using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Infragistics;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using Prism.Commands;
using Prism.Mvvm;
using SIMD_Demo.Eventos;

namespace SIMD_Demo.OperaçõesPorTrecho
{
    public class OperaçõesPorTrechoViewModel : BindableBase
    {
        private string _base;
        private string _intervalo;
        private string _topo;

        public OperaçõesPorTrechoViewModel()
        {
            OnInserirDados = new DelegateCommand(InserirDados);
            OnRemoverDados = new DelegateCommand(RemoverDados);
            OnTransformarDados = new DelegateCommand(TransformarDados);
        }

        private void TransformarDados()
        {
            double topo, @base, intervalo;
            if (double.TryParse(Topo, out topo) && double.TryParse(Base, out @base) && double.TryParse(Intervalo, out intervalo))
            {
                var perfis = DatabaseProvider.Db.GetAll<Perfil>().ToList();

                if (perfis.Count != 2) return;

                var perfil1 = perfis[0];
                var perfil2 = perfis[1];

                var trecho = perfil1.Pontos.Where(x => x.Profundidade >= topo && x.Profundidade <= @base);


                foreach (var pontoPerfil in trecho.ToList())
                {
                    perfil1.TransformarPonto(pontoPerfil);
                    DatabaseProvider.Db.Update(pontoPerfil);
                }

                EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Publish(new ExibirDadosEvent());
            }
        }

        private void RemoverDados()
        {
            double topo, @base, intervalo;

            if (double.TryParse(Topo, out topo) && double.TryParse(Base, out @base) && double.TryParse(Intervalo, out intervalo))
            {
                var perfis = DatabaseProvider.Db.GetAll<Perfil>().ToList();

                if (perfis.Count != 2) return;

                var perfil1 = perfis[0];
                var perfil2 = perfis[1];

                var trecho = perfil1.Pontos.Where(x => x.Profundidade >= topo && x.Profundidade <= @base);

                foreach (var pontoPerfil in trecho.ToList())
                {
                    perfil1.RemoverPonto(pontoPerfil);
                    DatabaseProvider.Db.Delete(pontoPerfil);
                }

                EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Publish(new ExibirDadosEvent());
            }

        }

        private void InserirDados()
        {
            double topo, @base, intervalo;

            if (double.TryParse(Topo, out topo) && double.TryParse(Base, out @base) && double.TryParse(Intervalo, out intervalo))
            {
                var perfis = DatabaseProvider.Db.GetAll<Perfil>().ToList();

                if (perfis.Count != 2) return;

                var perfil1 = perfis[0];
                var perfil2 = perfis[1];

                InserirDados(perfil1, topo, @base, intervalo);
                InserirDados(perfil2, topo, @base, intervalo);
                EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Publish(new ExibirDadosEvent());
            }

        }

        private static void InserirDados(Perfil perfil1, double topo, double @base, double intervalo)
        {
            var valores = perfil1.Pontos.Select(x => x.Valor).ToList();
            var profundidades = perfil1.Pontos.Select(x => x.Profundidade).ToList();

            var interpolador = Interpolate.Linear(profundidades, valores);

            var r = new Random();

            for (double i = topo; i < @base; i += intervalo)
            {
                var decisão = r.Next(0, 100);

                double valor;
                if (decisão % 2 == 0)
                {
                    valor = interpolador.Interpolate(i) + r.NextDouble();
                }
                else
                {
                    valor = interpolador.Interpolate(i) - r.NextDouble();
                }

                // TODO não pode ser shallow copy. Adições em listas alteram o objeto original
                if (perfil1.Pontos.Any(x => x.Profundidade == i))
                {
                    continue;
                }

                perfil1.AdicionarPontoPerfil(i, valor);

                var ponto = perfil1.Pontos.Single(x => x.Profundidade == i);

                DatabaseProvider.Db.Insert(ponto);
            }

            perfil1.Ordenar();
            DatabaseProvider.Db.Update(perfil1);
        }

        public string Topo
        {
            get => _topo;
            set
            {
                _topo = value;
                RaisePropertyChanged(nameof(Topo));
            }
        }

        public string Base
        {
            get => _base;
            set
            {
                _base = value;
                RaisePropertyChanged(nameof(Base));
            }
        }

        public string Intervalo
        {
            get => _intervalo;
            set
            {
                _intervalo = value;
                RaisePropertyChanged(nameof(Intervalo));
            }
        }

        public ICommand OnInserirDados { get; private set; }
        public ICommand OnRemoverDados { get; private set; }
        public ICommand OnTransformarDados { get; private set; }
    }
}