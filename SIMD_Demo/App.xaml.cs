using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SimpleInMemoryDatabase.Lib.Api;

namespace SIMD_Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var database = DatabaseCreator.Create();

            database.CreateTable<Perfil>();
            database.CreateTable<PontoPerfil>();

            database.CreateOneToMany<Perfil, PontoPerfil>(p => p.Perfil.Id, cascateDeletion: true);

            DatabaseProvider.Db = database;
        }
    }
}
