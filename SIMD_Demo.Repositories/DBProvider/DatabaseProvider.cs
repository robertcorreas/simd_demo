using System;
using SimpleInMemoryDatabase.Lib.Api;
using SIMD_Demo.Repositories.Domínio;

namespace SIMD_Demo.Repositories.DBProvider
{
    public static class DatabaseProvider
    {
        private static IDatabase _db;

        internal static IDatabase Db
        {
            get => _db;
            private set
            {
                if (_db == null)
                {
                    _db = value;
                }
                else
                {
                    throw new ArgumentException("Database já setado");
                }
            }
        }

        public static void DatabaseConfig()
        {
            var database = DatabaseCreator.Create();

            database.CreateTable<Perfil>();
            database.CreateTable<PontoPerfil>();

            database.CreateOneToMany<Perfil, PontoPerfil>(p => p.Perfil.Id, cascateDeletion: true);

            Db = database;
        }
    }
}
