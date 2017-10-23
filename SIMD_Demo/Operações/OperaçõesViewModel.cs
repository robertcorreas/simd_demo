using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using SIMD_Demo.Eventos;

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
            throw new NotImplementedException();
        }

        private void RemoverTodosDados()
        {
            var perfis = DatabaseProvider.Db.GetAll<Perfil>();

            foreach (var perfil in perfis)
            {
                DatabaseProvider.Db.Delete(perfil);
            }

            EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Publish(new ExibirDadosEvent());
        }

        private void CarregarDadosIniciais()
        {
            var db = DatabaseProvider.Db;
            if (db.Count<Perfil>() != 0) return;

            var leitor = new LeitorDadosEntrada(_caminhoArquivo);
            var perfis = leitor.ObterDados();

            foreach (var perfil in perfis)
            {
                db.Insert(perfil);

                foreach (var pontos in perfil.Pontos)
                {
                    db.Insert(pontos);
                }
            }

            EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Publish(new ExibirDadosEvent());
        }


        public ICommand OnCarregarDadosIniciais { get; private set; }
        public ICommand OnRemoverTodosDados { get; private set; }
        public ICommand OnAplicarTransformaçãoAosDados { get; private set; }

    }
}
