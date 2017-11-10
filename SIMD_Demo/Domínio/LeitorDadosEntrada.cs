using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SIMD_Demo.Domínio
{
    public class LeitorDadosEntrada
    {
        public string CaminhoArquivo { get; private set; }


        public LeitorDadosEntrada(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                throw new ArgumentException("Caminho inválido");
            }

            CaminhoArquivo = caminhoArquivo;
        }

        public List<Perfil> ObterDados()
        {
            using(var stream = File.OpenRead(CaminhoArquivo))
            using (TextReader reader = new StreamReader(stream))
            {
                //Skip general info
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                var header = reader.ReadLine()?.Trim();

                var splitHeader = header?.Split('\t');

                var nomePerfilA = splitHeader[1];
                var nomePerfilB = splitHeader[2];

                var perfilA = new Perfil(nomePerfilA);
                var perfilB = new Perfil(nomePerfilB);

                var line = reader.ReadLine();
                while (line != null)
                {
                    var dados = line.Trim().Split('\t');

                    if(dados.Length != 3) break;

                    var profundidade = double.Parse(dados[0], CultureInfo.InvariantCulture);
                    var dadoPerfilA = double.Parse(dados[1], CultureInfo.InvariantCulture);
                    var dadoPerfilB = double.Parse(dados[2], CultureInfo.InvariantCulture);

                    perfilA.AdicionarPontoPerfil(profundidade, dadoPerfilA);
                    perfilB.AdicionarPontoPerfil(profundidade, dadoPerfilB);

                    line = reader.ReadLine();
                }

                return new List<Perfil>() { perfilA, perfilB };
            }
        }
    }
}
