using System.Linq;
using Prism.Mvvm;
using SIMD_Demo.Eventos;
using SIMD_Demo.Providers;
using SIMD_Demo.Repositories;
using SIMD_Demo.Repositories.Repos;

namespace SIMD_Demo.Gráficos.ViewModels
{
    public class GráficosViewModel : BindableBase
    {
        private ItemGráficoViewModel _item1;
        private ItemGráficoViewModel _item2;

        public GráficosViewModel()
        {
            Item1 = new ItemGráficoViewModel();
            Item2 = new ItemGráficoViewModel();

            EventAggregatorProvider.EventAggregator.GetEvent<ExibirDadosEvent>().Subscribe(e =>
            {
                var perfis = PerfilRepository.ObterTodos().ToList();

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

        public ItemGráficoViewModel Item1
        {
            get => _item1;
            private set
            {
                _item1 = value;
                RaisePropertyChanged(nameof(Item1));
            }
        }

        public ItemGráficoViewModel Item2
        {
            get => _item2;
            private set
            {
                _item2 = value;
                RaisePropertyChanged(nameof(Item2));
            }
        }
    }
}