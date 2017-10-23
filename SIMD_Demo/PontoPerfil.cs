using System;
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
    }
}