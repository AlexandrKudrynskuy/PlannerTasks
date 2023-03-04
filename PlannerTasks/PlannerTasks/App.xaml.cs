using BLL.Service;
using DLL.Context;
using DLL.Repository;
using Domain.Model;
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
            services.AddTransient<SupAdminWind>();
            services.AddTransient<AdminWindow>();
            services.AddTransient<WorkerWind>();

            services.AddTransient<StatusRepository>();
            services.AddTransient<StatusService>();
            services.AddTransient<TypeTaskRepository>();
            services.AddTransient<TypeTaskService>();
            services.AddTransient<PriorityRepository>();
            services.AddTransient<PriorityService>();
            services.AddTransient<MyTaskRepository>();
            services.AddTransient<MyTaskService>();

        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var wind = provider.GetService<MainWindow>();
            //var wind = provider.GetService<WorkerWind>();
            wind.Show();
        }
    }
}
