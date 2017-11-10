using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SIMD_Demo.Domínio;
using Xunit;

namespace SIMD_Demo.Tests
{
    public class LeitorDadosEntradaTests
    {
        private LeitorDadosEntrada _sut;

        private string _caminhoArquivo = System.AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\dadosEntrada.dat";

        public LeitorDadosEntradaTests()
        {
            _sut = new LeitorDadosEntrada(_caminhoArquivo);
        }

        [Fact]
        public void DeveObterCaminhoArquivo()
        {
            _sut.CaminhoArquivo.Should().Be(_caminhoArquivo);
        }

        [Fact]
        public void DeveLançarExceçãoParaCaminhoInválido()
        {
            Action ação = () =>
            {
                _sut = new LeitorDadosEntrada("");
            };

            ação.ShouldThrow<ArgumentException>().WithMessage("Caminho inválido");
        }

        [Fact]
        public void DeveObterDadosEntrada()
        {
            var perfis = _sut.ObterDados();
            perfis.Should().HaveCount(2);

            const int totalPontosEsperados = 14834;

            perfis[0].QtdPontos.Should().Be(totalPontosEsperados);
            perfis[1].QtdPontos.Should().Be(totalPontosEsperados);
        }
    }
}
