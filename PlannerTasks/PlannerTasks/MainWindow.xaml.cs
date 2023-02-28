using BLL.Service;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlannerTasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserService userService;
        private ObservableCollection<User> users;
        public MainWindow(UserService _userService)
        {
            userService = _userService;
            InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedValue is User us)
            {
                var user = await userService.Login(us.Login, PasswordPasswordBox.Password);
                if (user == null)
                {
                    MessageBox.Show("Невірний пароль");
                }
                else
                {
                    CurrentUser.GetCurentUser();
                    CurrentUser.SetCurrentUser(user);
                    this.Close();
                }
            }
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UsersComboBox.ItemsSource = new ObservableCollection<User>(await userService.GetAllAsync());
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CurrentUser.User == null)
            {
                return;
            }
            if (CurrentUser.User.TypeUser == TypeUser.SuperAdmin)
            {
                var wind = App.provider.GetService<SupAdminWind>();
                wind.Show();
            }
            else
            {
                if (CurrentUser.User.TypeUser == TypeUser.Admin)
                {
                    var wind = App.provider.GetService<AdminWindow>();
                    wind.Show();
                }
                else
                {
                    var wind = App.provider.GetService<WorkerWind>();
                    wind.Show();
                }
            }
        }
    }
}
