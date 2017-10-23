using System;
using System.Windows.Documents;
using SimpleInMemoryDatabase.Lib.Api;

namespace SIMD_Demo
{
    public class PontoPerfil : Entity
    {
        public Perfil Perfil { get; private set; }


        public PontoPerfil(double profundidade, double valor, Perfil perfil)
        {
            Profundidade = profundidade;
            Valor = valor;
            Perfil = perfil;
        }

        public double Profundidade { get; private set; }
        public double Valor { get; private set; }

        public void AlterarValor(double novoValor)
        {
            Valor = novoValor;
        }

        public override string ToString()
        {
            return $"Prof: {Profundidade} | Valor: {Valor.ToString("F4")}";
        }
    }
}