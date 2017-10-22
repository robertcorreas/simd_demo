using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMD_Demo
{
    public class Perfil
    {
        public string Nome { get; private set; }

        private List<PontoPerfil> _pontos;

        public int QtdPontos => _pontos.Count;

        public Perfil(string nome)
        {
            Nome = nome;
            _pontos = new List<PontoPerfil>();
        }

        public void AdicionarPontoPerfil(double profundidade, double valor)
        {
            _pontos.Add(new PontoPerfil(profundidade, valor));
        }
    }
}
