using System.Windows;
using SIMD_Demo.Repositories.DBProvider;

namespace SIMD_Demo
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DatabaseProvider.DatabaseConfig();
        }
    }
}