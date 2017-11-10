using SimpleInMemoryDatabase.Lib.Api;

namespace SIMD_Demo.Domínio
{
    public class PontoPerfil : Entity
    {
        public PontoPerfil(double profundidade, double valor, Perfil perfil)
        {
            Profundidade = profundidade;
            Valor = valor;
            Perfil = perfil;
        }

        public Perfil Perfil { get; }

        public double Profundidade { get; }
        public double Valor { get; private set; }

        public void AlterarValor(double novoValor)
        {
            Valor = novoValor;
        }

        public override string ToString()
        {
            return $"Prof: {Profundidade} | Valor: {Valor:F4}";
        }
    }
}