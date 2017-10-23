using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInMemoryDatabase.Lib.Api;

namespace SIMD_Demo
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
