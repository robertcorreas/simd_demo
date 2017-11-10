using Prism.Mvvm;
using SIMD_Demo.Domínio;

namespace SIMD_Demo.Gráficos.ViewModels
{
    public class PontoViewModel : BindableBase
    {
        public PontoViewModel(PontoPerfil pontoPerfil)
        {
            Profundidade = pontoPerfil.Profundidade;
            Valor = pontoPerfil.Valor;
        }

        public double Profundidade { get; }
        public double Valor { get; }
    }
}