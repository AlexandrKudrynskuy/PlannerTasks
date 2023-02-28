using Bll;
using BLL.Service;
using Domain.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlannerTasks
{
    /// <summary>
    /// Interaction logic for SupAdminWind.xaml
    /// </summary>
    public partial class SupAdminWind : Window
    {
        private readonly UserService userService;
        public User User { get; set; }
        public SupAdminWind(UserService _userService)
        {
            InitializeComponent();
            userService = _userService;
            User = new User();

        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateUserGrid.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Collapsed;
            UpdateButton.Visibility = Visibility.Collapsed;
            RemoveButton.Visibility = Visibility.Collapsed;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
       
        }

        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveComboBox.ItemsSource = await userService.GetAllAsync();
            RemoveStackPanel.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Collapsed;
            UpdateButton.Visibility = Visibility.Collapsed;
            RemoveButton.Visibility = Visibility.Collapsed;
        }

        private async void RemoveChoiseButton_Click(object sender, RoutedEventArgs e)
        {

            if (RemoveComboBox.SelectedItem is User user)
            {
                if (user.TypeUser == TypeUser.SuperAdmin)
                {
                    MessageBox.Show("Суперадміна неможливо видалити");
                }
                else
                {
                    var res = await userService.DeleteAsync(user.Id);
                    if (res.IsEror == false)
                    {
                        MessageBox.Show($"{user.Name} видалено");
                    }
                    else
                    {
                        MessageBox.Show(res.Message);
                    }
                    RemoveStackPanel.Visibility = Visibility.Collapsed;
                    AddButton.Visibility = Visibility.Visible;
                    UpdateButton.Visibility = Visibility.Visible;
                    RemoveButton.Visibility = Visibility.Visible;
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void AddChoiseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdminChekBox.IsChecked == true)
            {
                User.TypeUser = TypeUser.Admin;
            }
            else
            {
                User.TypeUser = TypeUser.Worker;
            }
            var res = await userService.CreateAsync(User);
            if (res.IsEror == false)
            {
                MessageBox.Show($"User {User.Login} Add");
                User = new User();
                foreach (var child in AddStackPanel.Children)
                {
                    if (child is TextBox textBox)
                    {
                        textBox.Text = string.Empty;
                    }
                }
                PSWDPasswordBox.Password = string.Empty;
             
                RemoveStackPanel.Visibility = Visibility.Collapsed;
                AddOrUpdateUserGrid.Visibility = Visibility.Collapsed;
                AddButton.Visibility = Visibility.Visible;
                UpdateButton.Visibility = Visibility.Visible;
                RemoveButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show($"{res.Message}");
            }
        }

        private async void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var res = await userService.GetFromCondition(x => x.Login == textBox.Text);
                if (res.Count == 0)
                {
                    if (textBox.Text.IsCorectLogin())
                    {
                        User.Login = textBox.Text;
                        LoginPopup.IsOpen = false;
                    }
                    else
                    {
                        LoginPopup.Placement = PlacementMode.Bottom;
                        LoginPopup.PlacementTarget = (UIElement)e.Source;
                        LoginPopup.IsOpen = true;
                    }
                }
                else 
                {
                    MessageBox.Show($"User {textBox.Text} існує");
                }
            }
            IsAllTrue();
        }

        private void PSWDPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {

            if (PSWDPasswordBox.Password.IsCorectPassword())
            {
                User.Password = PSWDPasswordBox.Password;
                PswdPopup.IsOpen = false;
            }
            else
            {
                PswdPopup.Placement = PlacementMode.Bottom;
                PswdPopup.PlacementTarget = (UIElement)e.Source;
                PswdPopup.IsOpen = true;
            }
            IsAllTrue();

        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.IsCorectName())
            {
                User.Name = NameTextBox.Text;
                NamePopup.IsOpen = false;
            }
            else
            {
                NamePopup.Placement = PlacementMode.Bottom;
                NamePopup.PlacementTarget = (UIElement)e.Source;
                NamePopup.IsOpen = true;
            }
            IsAllTrue();
        }

        private void SurNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SurNameTextBox.Text.IsCorectName())
            {
                User.SurName = SurNameTextBox.Text;
                NamePopup.IsOpen = false;
            }
            else
            {
                NamePopup.Placement = PlacementMode.Bottom;
                NamePopup.PlacementTarget = (UIElement)e.Source;
                NamePopup.IsOpen = true;
            }
            IsAllTrue();
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTextBox.Text!=string.Empty)
            {
                User.Email = EmailTextBox.Text;
                EmptyPopup.IsOpen = false;
            }
            else
            {
                EmptyPopup.Placement = PlacementMode.Bottom;
                EmptyPopup.PlacementTarget = (UIElement)e.Source;
                EmptyPopup.IsOpen = true;
            }
            IsAllTrue();
        }

        private void PhoneNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PhoneNameTextBox.Text, out int number))
            {

                User.Phone = PhoneNameTextBox.Text;
                NumberPopup.IsOpen = false;
            }
            else
            {
                NumberPopup.Placement = PlacementMode.Bottom;
                NumberPopup.PlacementTarget = (UIElement)e.Source;
                NumberPopup.IsOpen = true;
            }
                IsAllTrue();
            
        }

    

        private void CanselButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveStackPanel.Visibility = Visibility.Collapsed;
            AddOrUpdateUserGrid.Visibility = Visibility.Collapsed;
            AddButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            RemoveButton.Visibility = Visibility.Visible;
        }

        private void IsAllTrue()
        {
            if (User.SurName != null &&
               User.Name != null &&
               User.Login != null &&
               User.Email != null &&
               User.Phone != null &&               
               User.Photo != null)
            {
                AddChoiseBtn.Visibility = Visibility.Visible;

            }
        }

        private void ChoiseFotoButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Photo|*jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                string temp = Guid.NewGuid() + ".jpg";
                PhotoNameTextBox.Text = temp;
                File.Copy(openFileDialog.FileName, Helper.PhotoPathUser + "\\" + temp);
                User.Photo = Helper.PhotoPathUser + "\\" + temp;
            }
            IsAllTrue();
        }
    }
}
