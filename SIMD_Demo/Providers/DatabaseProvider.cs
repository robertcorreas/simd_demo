using System;
using SimpleInMemoryDatabase.Lib.Api;

namespace SIMD_Demo.Providers
{
    public static class DatabaseProvider
    {
        private static IDatabase _db;

        public static IDatabase Db
        {
            get => _db;
            set
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
    }
}
