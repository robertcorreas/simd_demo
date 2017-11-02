using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInMemoryDatabase.Lib.Api;

namespace SIMD_Demo
{
    public class Perfil : Entity
    {
        private List<PontoPerfil> _pontos;

        public Perfil(string nome)
        {
            Nome = nome;
            _pontos = new List<PontoPerfil>();
        }

        public string Nome { get; }

        public int QtdPontos => _pontos.Count;
        public IReadOnlyList<PontoPerfil> Pontos => _pontos;

        public PontoPerfil AdicionarPontoPerfil(double profundidade, double valor)
        {
            var pontoPerfil = new PontoPerfil(profundidade, valor, this);
            _pontos.Add(pontoPerfil);
            return pontoPerfil;
        }

        public void TransformarDados()
        {
            for (var i = 0; i < _pontos.Count; i++)
            {
                var r = new Random();
                var fator1 = r.Next(2, 6);
                var fator2 = r.Next(4, 12);

                _pontos[i].AlterarValor(_pontos[i].Valor / fator2 - _pontos[i].Valor * fator1);
            }
        }

        public void TransformarPonto(PontoPerfil ponto)
        {
            var r = new Random();
            var novoValor = 0.0;
            if (Convert.ToInt32(ponto.Valor) % 2 == 0)
                novoValor = ponto.Valor + r.NextDouble();
            else
                novoValor = ponto.Valor - r.NextDouble();

            ponto.AlterarValor(novoValor);
        }

        public void RemoverPonto(PontoPerfil pontoPerfil)
        {
            _pontos.Remove(pontoPerfil);
        }

        public void Ordenar()
        {
            _pontos = _pontos.OrderBy(x => x.Profundidade).ToList();
        }
    }
}