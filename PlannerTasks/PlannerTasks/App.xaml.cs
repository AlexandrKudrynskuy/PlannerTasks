using BLL.Service;
using DLL.Context;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PlannerTasks
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider provider;
        public App()
        {
            var service = new ServiceCollection();
            ConfigureService(service);
            provider = service.BuildServiceProvider();
        }
        private void ConfigureService(ServiceCollection services)
        {
            services.AddDbContext<PlannerTasksContext>(x => x.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TaskPlanner;Integrated Security=True;"));
            services.AddTransient<UserService>();
            services.AddTransient<UserRepository>();
            services.AddTransient<MainWindow>();


        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var wind = provider.GetService<MainWindow>();
            wind.Show();
        }
    }
}
