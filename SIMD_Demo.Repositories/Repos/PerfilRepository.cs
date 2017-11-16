using System.Collections.Generic;
using System.Linq;
using SIMD_Demo.Repositories.DBProvider;
using SIMD_Demo.Repositories.Domínio;

namespace SIMD_Demo.Repositories.Repos
{
    public static class PerfilRepository
    {
        public static void Inserir(Perfil perfil)
        {
            DatabaseProvider.Db.Insert(perfil);
            DatabaseProvider.Db.Insert<PontoPerfil>(perfil.Pontos);
        }

        public static void Inserir(IEnumerable<Perfil> perfis)
        {
            foreach (var perfil in perfis)
                Inserir(perfil);
        }

        public static IEnumerable<Perfil> ObterTodos()
        {
            return DatabaseProvider.Db.GetAll<Perfil>();
        }

        public static void Remover(Perfil perfil)
        {
            DatabaseProvider.Db.Delete(perfil);
        }

        public static long Quantidade()
        {
            return DatabaseProvider.Db.Count<Perfil>();
        }

        public static void Atualizar(Perfil perfil, bool atualizarPontos)
        {
            DatabaseProvider.Db.Update(perfil);

            if (atualizarPontos)
                AtualizarPontos(perfil);
        }

        public static void Atualizar(IEnumerable<Perfil> perfis, bool atualizarPontos)
        {
            foreach (var perfil in perfis)
            {
                DatabaseProvider.Db.Update(perfil);

                if (atualizarPontos)
                    AtualizarPontos(perfil);
            }
        }

        private static void AtualizarPontos(Perfil perfil)
        {
            DatabaseProvider.Db.Update<PontoPerfil>(perfil.Pontos);
        }

        public static void RemoverTodos()
        {
            DatabaseProvider.Db.DeleteAll<Perfil>();
        }

        public static void RemoverTrecho(Perfil perfil, double topo, double @base)
        {
            var trecho = perfil.Pontos.Where(x => x.Profundidade >= topo && x.Profundidade <= @base).ToList();

            foreach (var pontoPerfil in trecho)
                perfil.RemoverPonto(pontoPerfil);

            DatabaseProvider.Db.Update(perfil);
            DatabaseProvider.Db.Delete(trecho, t => t.Perfil.Id == perfil.Id);
        }

        public static void InserirPontosPerfil(Perfil perfil, IEnumerable<PontoPerfil> pontos)
        {
            DatabaseProvider.Db.Update(perfil);
            DatabaseProvider.Db.Insert(pontos);
        }

        public static void InserirPontoPerfil(Perfil perfil, PontoPerfil ponto)
        {
            DatabaseProvider.Db.Update(perfil);
            DatabaseProvider.Db.Insert(ponto);
        }
    }
}