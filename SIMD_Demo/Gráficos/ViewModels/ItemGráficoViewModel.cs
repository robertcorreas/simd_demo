using System.Collections.ObjectModel;
using Prism.Mvvm;
using SIMD_Demo.Repositories.Domínio;

namespace SIMD_Demo.Gráficos.ViewModels
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