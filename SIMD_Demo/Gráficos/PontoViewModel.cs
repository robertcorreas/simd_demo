using Prism.Mvvm;

namespace SIMD_Demo.Gráficos
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