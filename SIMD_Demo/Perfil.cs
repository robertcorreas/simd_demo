using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInMemoryDatabase.Lib.Api;

namespace SIMD_Demo
{
    public class Perfil : Entity
    {
        public string Nome { get; private set; }

        private List<PontoPerfil> _pontos;

        public int QtdPontos => _pontos.Count;
        public IReadOnlyList<PontoPerfil> Pontos => _pontos;

        public Perfil(string nome)
        {
            Nome = nome;
            _pontos = new List<PontoPerfil>();
        }

        public void AdicionarPontoPerfil(double profundidade, double valor)
        {
            _pontos.Add(new PontoPerfil(profundidade, valor, this));
        }
    }
}
