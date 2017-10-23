using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using SIMD_Demo.Eventos;

namespace SIMD_Demo.Gráficos
{
    public class GráficosViewModel : BindableBase
    {
        private ItemGráficoViewModel _item1;
        private ItemGráficoViewModel _item2;

        public ItemGráficoViewModel Item1
        {
            get => _item1;
            private set { _item1 = value; RaisePropertyChanged(nameof(Item1));}
        }

        public ItemGráficoViewModel Item2
        {
            get => _item2;
            private set { _item2 = value; RaisePropertyChanged(nameof(Item2)); }
        }

        public GráficosViewModel()
        {
            Item1 = new ItemGráficoViewModel();
            Item2 = new ItemGráficoViewModel();

            EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Subscribe((e) =>
            {
                var perfis = DatabaseProvider.Db.GetAll<Perfil>().ToList();

                if (perfis.Count == 2)
                {
                    Item1.CarregarDados(perfis[0]);
                    Item2.CarregarDados(perfis[1]);
                }
                else
                {
                    Item1.Pontos.Clear();
                    Item2.Pontos.Clear();
                }
            });
        }
    }

    public class ItemGráficoViewModel : BindableBase
    {
        public void CarregarDados(Perfil perfil)
        {
            Pontos.Clear();

            foreach (var pontoPerfil in perfil.Pontos)
            {
                Pontos.Add(new PontoViewModel(pontoPerfil));
            }
        }

        public ItemGráficoViewModel()
        {
            Pontos = new ObservableCollection<PontoViewModel>();
        }

        public ObservableCollection<PontoViewModel> Pontos { get; private set; }
    }

    public class PontoViewModel : BindableBase
    {
        public double Profundidade { get; private set; }
        public double Valor { get; private set; }

        public PontoViewModel(PontoPerfil pontoPerfil)
        {
            Profundidade = pontoPerfil.Profundidade;
            Valor = pontoPerfil.Valor;
        }
    }
}
