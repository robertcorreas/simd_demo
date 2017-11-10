using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MathNet.Numerics;
using Prism.Commands;
using Prism.Mvvm;
using SIMD_Demo.Domínio;
using SIMD_Demo.Eventos;
using SIMD_Demo.Providers;
using SIMD_Demo.Repositories;

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

        public ICommand OnInserirDados { get; }
        public ICommand OnRemoverDados { get; }
        public ICommand OnTransformarDados { get; }

        private void TransformarDados()
        {
            double topo, @base;
            if (double.TryParse(Topo, out topo) && double.TryParse(Base, out @base))
            {
                var perfis = PerfilRepository.ObterTodos().ToList();

                if (PerfilRepository.Quantidade() != 2) return;

                var perfil1 = perfis[0];
                var perfil2 = perfis[1];

                var trechoPerfil1 = perfil1.Pontos.Where(x => x.Profundidade >= topo && x.Profundidade <= @base).ToList();
                var trechoPerfil2 = perfil2.Pontos.Where(x => x.Profundidade >= topo && x.Profundidade <= @base).ToList();

                foreach (var ponto in trechoPerfil1)
                    perfil1.TransformarPonto(ponto);

                foreach (var ponto in trechoPerfil2)
                    perfil1.TransformarPonto(ponto);

                PerfilRepository.Atualizar(perfil1, true);
                PerfilRepository.Atualizar(perfil2, true);

                EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Publish(new ExibirDadosEvent());
            }
        }

        private void RemoverDados()
        {
            double topo, @base;

            if (double.TryParse(Topo, out topo) && double.TryParse(Base, out @base))
            {
                var perfis = PerfilRepository.ObterTodos().ToList();

                if (perfis.Count != 2) return;

                var perfil1 = perfis[0];
                var perfil2 = perfis[1];

                PerfilRepository.RemoverTrecho(perfil1, topo, @base);
                PerfilRepository.RemoverTrecho(perfil2, topo, @base);

                NotificarGráficos();
            }
        }

        private void NotificarGráficos()
        {
            EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Publish(new ExibirDadosEvent());
        }

        private void InserirDados()
        {
            double topo, @base, intervalo;

            if (double.TryParse(Topo, out topo) && double.TryParse(Base, out @base) &&
                double.TryParse(Intervalo, out intervalo))
            {
                var perfis = PerfilRepository.ObterTodos().ToList();

                if (perfis.Count != 2) return;

                var perfil1 = perfis[0];
                var perfil2 = perfis[1];

                InserirDadosPerfil(perfil1, topo, @base, intervalo);
                InserirDadosPerfil(perfil2, topo, @base, intervalo);
                NotificarGráficos();
            }
        }

        private static void InserirDadosPerfil(Perfil perfil, double topo, double @base, double intervalo)
        {
            var valores = perfil.Pontos.Select(x => x.Valor).ToList();
            var profundidades = perfil.Pontos.Select(x => x.Profundidade).ToList();

            var interpolador = Interpolate.Linear(profundidades, valores);

            var r = new Random();
            var pontos = new List<PontoPerfil>();
            for (var i = topo; i < @base; i += intervalo)
            {
                var decisão = r.Next(0, 100);

                double valor;
                if (decisão % 2 == 0)
                    valor = interpolador.Interpolate(i) + r.NextDouble();
                else
                    valor = interpolador.Interpolate(i) - r.NextDouble();

                var ponto = perfil.AdicionarPontoPerfil(i, valor);
                pontos.Add(ponto);
            }

            perfil.Ordenar();
            PerfilRepository.Atualizar(perfil, false);
            PerfilRepository.InserirPontosPerfil(perfil, pontos);
        }
    }
}