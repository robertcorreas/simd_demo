using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace SIMD_Demo.Gráficos
{
    public class ItemGráficoViewModel : BindableBase
    {
        public ItemGráficoViewModel()
        {
            Pontos = new ObservableCollection<PontoViewModel>();
        }

        public ObservableCollection<PontoViewModel> Pontos { get; }

        public void CarregarDados(Perfil perfil)
        {
            Pontos.Clear();

            foreach (var pontoPerfil in perfil.Pontos)
                Pontos.Add(new PontoViewModel(pontoPerfil));
        }
    }
}