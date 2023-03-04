using System;
using System.Collections.Generic;
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
    /// Interaction logic for WorkerWind.xaml
    /// </summary>
    public partial class WorkerWind : Window
    {
        private readonly MyTaskService myTaskService;
        private readonly PriorityService priorityService;
        private readonly StatusService statusService;
        private readonly TypeTaskService typeTaskService;
        private readonly UserService userService;
        public MyTask MyTask { get; set; }
        public WorkerWind(MyTaskService _myTaskService, UserService _userService, StatusService _statusService, TypeTaskService _typeTaskService, PriorityService _priorityService)
        {
            InitializeComponent();
            userService = _userService;
            myTaskService = _myTaskService;
            statusService = _statusService;
            typeTaskService = _typeTaskService;
            priorityService = _priorityService;
            MyTask = new MyTask();
            MyTask.WorkerId = CurrentUser.User.Id;

        }

        private async void ShowAllBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskListBox.ItemsSource = await myTaskService.GetFromConditionAsync(x=>x.WorkerId==CurrentUser.User.Id);
        }

        private async void TypeTaskComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                    if (comboBox.SelectedItem is TypeTask field)
                    {
                        if (field != null)
                        {
                            MyTask.TypeTaskId = field.Id;
                        }
                }
                var res = await myTaskService.FilterAsync(MyTask);
                TaskListBox.ItemsSource = res;
            }
        }
        private async void statusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is Status field)
                {
                    MyTask.StatusId = field.Id;

                }
                var res = await myTaskService.FilterAsync(MyTask);
                TaskListBox.ItemsSource = res;
            }
        }

        private async void priorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is Priority field)
                {
                    MyTask.PriorityId = field.Id;

                }
                var res = await myTaskService.FilterAsync(MyTask);
                TaskListBox.ItemsSource = res;
            }
        }

        private async void autorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is User field)
                {
                    MyTask.AdminId = field.Id;

                }
                var res = await myTaskService.FilterAsync(MyTask);
                TaskListBox.ItemsSource = res;
            }

        }

        private async void DateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker datePicker)
            {
                MyTask.DateStart = datePicker.SelectedDate;
                var res = await myTaskService.FilterAsync(MyTask);
                TaskListBox.ItemsSource = res;
            }

        }

        private async void DateFinich_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker datePicker)
            {
                MyTask.DateFinich = datePicker.SelectedDate;
                var res = await myTaskService.FilterAsync(MyTask);
                TaskListBox.ItemsSource = res;
            }
        }
        private async void InitializeFilter()
        {
            TypeTaskComboBox.Items.Clear();
            statusComboBox.Items.Clear();
            priorityComboBox.Items.Clear();
            autorComboBox.Items.Clear();
        

            TypeTaskComboBox.ItemsSource = await typeTaskService.GetAllAsync();
            statusComboBox.ItemsSource = await statusService.GetAllAsync();
            priorityComboBox.ItemsSource = await priorityService.GetAllAsync();
            autorComboBox.ItemsSource = await userService.GetFromCondition(x => x.TypeUser == TypeUser.Admin);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            TaskListBox.Items.Clear();
            InitializeFilter();
        }

        private async void UpdateStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button buttton)
            {
                int id;
                int.TryParse(buttton.Tag.ToString(), out id);
                MyTask = (await myTaskService.GetFromConditionAsync(x => x.Id == id)).First();

                chaingeStatusPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                chaingeStatusPopup.PlacementTarget = (UIElement)e.Source;
                chaingeStatusPopup.IsOpen = true;
                ChaingeStatusComboBox.ItemsSource = await statusService.GetAllAsync();
                if (ChaingeStatusComboBox.ItemsSource is MyTask itemTask)
                {
                    MyTask.StatusId = itemTask.StatusId;
               
                } 
            }
        }

        private async void ChaingeButton_Click(object sender, RoutedEventArgs e)
        {
            chaingeStatusPopup.IsOpen = false;
            var res = await myTaskService.UpdateAsync(MyTask.Id, MyTask);

        }
    }


}
