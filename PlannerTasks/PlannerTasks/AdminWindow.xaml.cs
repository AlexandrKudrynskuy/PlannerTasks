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
using System.Windows.Shapes;
using BLL.Service;
using Domain.Model;

namespace PlannerTasks
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly MyTaskService myTaskService;
        private readonly PriorityService  priorityService;
        private readonly StatusService statusService;
        private readonly TypeTaskService typeTaskService;
        public ObservableCollection<string> Field {  get; set; }


        public AdminWindow(MyTaskService _myTaskService, StatusService _statusService, TypeTaskService _typeTaskService, PriorityService _priorityService )
        {
            InitializeComponent();
            myTaskService= _myTaskService;
            statusService= _statusService;
            typeTaskService= _typeTaskService;
            priorityService= _priorityService;
            Field = AddField();



        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChoiseFieldComboBox.ItemsSource = Field;
            ChoiseValueComboBox.Visibility = Visibility.Collapsed;


        }

        private void AddRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton != null && ChoiseFieldComboBox!=null)
                {
                    ChoiseValueComboBox.Visibility = Visibility.Collapsed;
                    valueLabel.Visibility = Visibility.Collapsed;
                    OkButton.Visibility = Visibility.Visible;
                    HeaderFieldTextBlock.Visibility = Visibility.Visible;
                    NameFieldTextBox.Visibility = Visibility.Visible;   

                }
            }
        }

        private ObservableCollection<string> AddField()
        {
            var field = new ObservableCollection<string>();
            field.Add(nameof(Status));
            field.Add(nameof(TypeTask));
            field.Add(nameof(Priority));
            return field;
        }

        private async void ChoiseFieldComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedValue is string str)
                {
                    if (str == nameof(TypeTask))
                    {
                        ChoiseValueComboBox.ItemsSource = await typeTaskService.GetAllAsync();
                    }
                    if (str == nameof(Status))
                    {
                        ChoiseValueComboBox.ItemsSource = await statusService.GetAllAsync();

                    }
                    if (str == nameof(Priority))
                    {
                        ChoiseValueComboBox.ItemsSource = await priorityService.GetAllAsync();

                    }
                }
            }


        }

        private void UpdateRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                ChoiseValueComboBox.Visibility = Visibility.Visible;
                valueLabel.Visibility = Visibility.Visible;
                OkButton.Visibility = Visibility.Visible;
                HeaderFieldTextBlock.Visibility = Visibility.Visible;
                NameFieldTextBox.Visibility = Visibility.Visible;

            }
        }

        private void RamoveRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                ChoiseValueComboBox.Visibility = Visibility.Visible;
                valueLabel.Visibility = Visibility.Visible;
                OkButton.Visibility = Visibility.Visible;
                HeaderFieldTextBlock.Visibility = Visibility.Collapsed;
                NameFieldTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (ChoiseFieldComboBox.SelectedValue is string str)
            {
                if (str == nameof(TypeTask))
                {
                    if (AddRadioBtn.IsChecked == true)
                    {
                        if (NameFieldTextBox.Text != string.Empty)
                        {
                            var res = await typeTaskService.CreateAsync(new TypeTask { Name = NameFieldTextBox.Text });
                            MessageBox.Show(res.Message);
                        }
                        else
                        {
                            MessageBox.Show("Поле не може бути пустим");
                        }
                    }

                    if (UpdateRadioBtn.IsChecked == true)
                    {
                        if (NameFieldTextBox.Text != string.Empty)
                        {
                            var item = ChoiseValueComboBox.SelectedValue as TypeTask; 
                            var res = await typeTaskService.UpdateAsync(item.Id,new TypeTask {Name = NameFieldTextBox.Text });
                            MessageBox.Show(res.Message);
                        }
                        else
                        {
                            MessageBox.Show("Поле не може бути пустим");
                        }
                    }

                    if(RamoveRadioBtn.IsChecked == true)
                    {
                        var item = ChoiseValueComboBox.SelectedValue as TypeTask;
                        var res = await typeTaskService.DeleteAsync(item.Id);
                        MessageBox.Show(res.Message);

                    }

                }
                if (str == nameof(Status))
                {
                    if (AddRadioBtn.IsChecked == true)
                    {
                        if (NameFieldTextBox.Text != string.Empty)
                        {
                            var res = await statusService.CreateAsync(new Status { Name = NameFieldTextBox.Text });
                            MessageBox.Show(res.Message);
                        }
                        else
                        {
                            MessageBox.Show("Поле не може бути пустим");
                        }
                    }
                    if (UpdateRadioBtn.IsChecked == true)
                    {
                        if (NameFieldTextBox.Text != string.Empty)
                        {
                            var item = ChoiseValueComboBox.SelectedValue as Status;
                            var res = await statusService.UpdateAsync(item.Id, new Status { Name = NameFieldTextBox.Text });
                            MessageBox.Show(res.Message);
                        }
                        else
                        {
                            MessageBox.Show("Поле не може бути пустим");
                        }
                    }
                    if (RamoveRadioBtn.IsChecked == true)
                    {
                        var item = ChoiseValueComboBox.SelectedValue as Status;
                        var res = await statusService.DeleteAsync(item.Id);
                        MessageBox.Show(res.Message);

                    }
                }
                if (str == nameof(Priority))
                {

                    if (AddRadioBtn.IsChecked == true)
                    {
                        if (NameFieldTextBox.Text != string.Empty)
                        {
                            var res = await priorityService.CreateAsync(new Priority { Name = NameFieldTextBox.Text });
                            MessageBox.Show(res.Message);
                        }
                        else
                        {
                            MessageBox.Show("Поле не може бути пустим");
                        }
                    }
                    if (UpdateRadioBtn.IsChecked == true)
                    {
                        if (NameFieldTextBox.Text != string.Empty)
                        {
                            var item = ChoiseValueComboBox.SelectedValue as Priority;
                            var res = await priorityService.UpdateAsync(item.Id, new Priority { Name = NameFieldTextBox.Text });
                            MessageBox.Show(res.Message);
                        }
                        else
                        {
                            MessageBox.Show("Поле не може бути пустим");
                        }
                    }
                    if (RamoveRadioBtn.IsChecked == true)
                    {
                        var item = ChoiseValueComboBox.SelectedValue as Priority;
                        var res = await priorityService.DeleteAsync(item.Id);
                        MessageBox.Show(res.Message);

                    }

                }
            }
            NameFieldTextBox.Text = string.Empty;

        }
   
    
    }
}
