using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using SIMD_Demo.Domínio;
using SIMD_Demo.Eventos;
using SIMD_Demo.Providers;
using SIMD_Demo.Repositories;

namespace SIMD_Demo.Operações
{
    public class OperaçõesViewModel : Prism.Mvvm.BindableBase
    {
        private readonly string _caminhoArquivo = System.AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\dadosEntrada.dat";

        public OperaçõesViewModel()
        {
            OnCarregarDadosIniciais = new DelegateCommand(CarregarDadosIniciais);
            OnRemoverTodosDados = new DelegateCommand(RemoverTodosDados);
            OnAplicarTransformaçãoAosDados = new DelegateCommand(AplicarTransformaçãoAosDados);
        }

        private void AplicarTransformaçãoAosDados()
        {
            var perfis = PerfilRepository.ObterTodos().ToList();

            if (perfis.Count == 2)
            {
                var perfil1 = perfis[0];
                var perfil2 = perfis[1];

                perfil1.TransformarDados();
                perfil2.TransformarDados();

                PerfilRepository.Atualizar(new List<Perfil>() { perfil1, perfil2 }, atualizarPontos: true);

                NotificarGráficos();
            }
        }

        private void NotificarGráficos()
        {
            EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Publish(new ExibirDadosEvent());
        }

        private void RemoverTodosDados()
        {
            PerfilRepository.RemoverTodos();
            NotificarGráficos();
        }

        private void CarregarDadosIniciais()
        {
            if (PerfilRepository.Quantidade() != 0) return;

            var leitor = new LeitorDadosEntrada(_caminhoArquivo);
            var perfis = leitor.ObterDados();

            PerfilRepository.Inserir(perfis);

            NotificarGráficos();
        }



        public ICommand OnCarregarDadosIniciais { get; private set; }
        public ICommand OnRemoverTodosDados { get; private set; }
        public ICommand OnAplicarTransformaçãoAosDados { get; private set; }

    }
}
