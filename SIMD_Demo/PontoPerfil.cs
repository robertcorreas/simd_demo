namespace SIMD_Demo
{
    public class PontoPerfil
    {
        public PontoPerfil(double profundidade, double valor)
        {
            Profundidade = profundidade;
            Valor = valor;
        }

        public double Profundidade { get; private set; }
        public double Valor { get; private set; }
    }
}