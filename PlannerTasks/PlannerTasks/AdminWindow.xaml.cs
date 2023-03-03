using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private readonly PriorityService priorityService;
        private readonly StatusService statusService;
        private readonly TypeTaskService typeTaskService;
        private readonly UserService userService;
        public ObservableCollection<MyTask> MyTasks { get; set; }
        public MyTask MyTask { get; set; }
        public ObservableCollection<string> Field { get; set; }


        public AdminWindow(MyTaskService _myTaskService, UserService _userService, StatusService _statusService, TypeTaskService _typeTaskService, PriorityService _priorityService)
        {
            InitializeComponent();
            userService = _userService;
            myTaskService = _myTaskService;
            statusService = _statusService;
            typeTaskService = _typeTaskService;
            priorityService = _priorityService;
            Field = AddField();


        }

        private async void InitializeFilter()
        {
            TypeTaskComboBox.Items.Clear();
            statusComboBox.Items.Clear();
            priorityComboBox.Items.Clear();
            autorComboBox.Items.Clear();
            workerComboBox.Items.Clear();

            AddUpdateTypeCombox.Items.Clear();
            AddUpdateAutorCombox.Items.Clear();
            AddUpdatePrioritetCombox.Items.Clear();
            AddUpdateWorkerCombox.Items.Clear();
            AddUpdateStatusCombox.Items.Clear();

            AddUpdateTypeCombox.ItemsSource = await typeTaskService.GetAllAsync();
            AddUpdateAutorCombox.ItemsSource = await userService.GetFromCondition(x => x.TypeUser == TypeUser.Admin);
            AddUpdatePrioritetCombox.ItemsSource = await priorityService.GetAllAsync();
            AddUpdateWorkerCombox.ItemsSource = await userService.GetFromCondition(x => x.TypeUser == TypeUser.Worker);
            AddUpdateStatusCombox.ItemsSource = await statusService.GetAllAsync();

            TypeTaskComboBox.ItemsSource = await typeTaskService.GetAllAsync();
            statusComboBox.ItemsSource = await statusService.GetAllAsync();
            priorityComboBox.ItemsSource = await priorityService.GetAllAsync();
            autorComboBox.ItemsSource = await userService.GetFromCondition(x => x.TypeUser == TypeUser.Admin);
            workerComboBox.ItemsSource = await userService.GetFromCondition(x => x.TypeUser == TypeUser.Worker);

        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChoiseFieldComboBox.ItemsSource = Field;
            ChoiseValueComboBox.Visibility = Visibility.Collapsed;

            TaskListBox.Items.Clear();
            InitializeFilter();
            MyTask = new MyTask();
            //TaskListBox.ItemsSource = MyTasks;
        }
        #region Seting field
        private void AddRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton != null && ChoiseFieldComboBox != null)
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
            if (SetingTaskTabItem.IsSelected == true)
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
                            var res = await typeTaskService.UpdateAsync(item.Id, new TypeTask { Name = NameFieldTextBox.Text });
                            MessageBox.Show(res.Message);

                        }
                        else
                        {
                            MessageBox.Show("Поле не може бути пустим");
                        }
                    }

                    if (RamoveRadioBtn.IsChecked == true)
                    {
                        var item = ChoiseValueComboBox.SelectedValue as TypeTask;
                        var res = await typeTaskService.DeleteAsync(item.Id);
                        MessageBox.Show(res.Message);

                    }
                    ChoiseValueComboBox.ItemsSource = await typeTaskService.GetAllAsync();


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
                    ChoiseValueComboBox.ItemsSource = await statusService.GetAllAsync();

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
                    ChoiseValueComboBox.ItemsSource = await priorityService.GetAllAsync();


                }

            }
            NameFieldTextBox.Text = string.Empty;

        }


        #endregion


        private async void ShowAllBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskListBox.ItemsSource = await myTaskService.GetAllAsync();
            //statusComboBox.SelectedItem = null;
            //priorityComboBox.SelectedItem = null;
            //TypeTaskComboBox.SelectedItem = null;
        }

        private async void statusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is Status field)
                {
                    MyTask.StatusId = field.Id;

                }
                var res = await myTaskService.Filter(MyTask);
                TaskListBox.ItemsSource = res;
            }

        }
        private async void TypeTaskComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is TypeTask field)
                {
                    MyTask.TypeTaskId = field.Id;

                }
                var res = await myTaskService.Filter(MyTask);
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
                var res = await myTaskService.Filter(MyTask);
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
                var res = await myTaskService.Filter(MyTask);
                TaskListBox.ItemsSource = res;
            }
        }

        private async void workerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is User field)
                {
                    MyTask.WorkerId = field.Id;

                }
                var res = await myTaskService.Filter(MyTask);
                TaskListBox.ItemsSource = res;
            }
        }

        private void AddUpdateCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void AddUpdateNewTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            int timeSpan;

            if (AddUpdateTaskTextBox.Text != string.Empty && DescriptionUpdateTaskTextBox.Text != string.Empty && AddUpdateStartPicer != null && int.TryParse(AddUpdateTimeSpanTextBox.Text, out timeSpan))
            {
                MyTask.Name = AddUpdateTaskTextBox.Text;
                MyTask.Description = DescriptionUpdateTaskTextBox.Text;
                         
                if (AddUpdateTypeCombox.SelectedItem is TypeTask typeTask)
                {
                    MyTask.TypeTaskId = typeTask.Id;
                }

                if (AddUpdatePrioritetCombox.SelectedItem is Priority priority)
                {
                    MyTask.PriorityId = priority.Id;
                }

                if (AddUpdateStatusCombox.SelectedItem is Status status)
                {
                    MyTask.StatusId = status.Id;
                }

                if (AddUpdateAutorCombox.SelectedItem is User autor)
                {
                    MyTask.AdminId = autor.Id;
                }
                if (AddUpdateWorkerCombox.SelectedItem is User worker)
                {
                    MyTask.WorkerId = worker.Id;
                }
                MyTask.DateStart = AddUpdateStartPicer.SelectedDate;

                MyTask.TimeSpan = timeSpan;

                var result = await myTaskService.CreateAsync(MyTask);
             
                MessageBox.Show(result.Message);
             
                MyTask = new MyTask();
            }
            else
            {
                MessageBox.Show("заповніть всі поля");
            }
            AddUpdateStartPicer.SelectedDate = DateTime.Now;
            AddUpdateTaskTextBox.Text = string.Empty;
            DescriptionUpdateTaskTextBox.Text = string.Empty;
            AddUpdateTimeSpanTextBox.Text = string.Empty;
        }

        private async void RemovePopUpBtn_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                int id;
                int.TryParse(button.Tag.ToString(), out id);
                var res = await myTaskService.DeleteAsync(id);
                TaskListBox.ItemsSource = await myTaskService.GetAllAsync();
            }
        }

        private async void UpdatePopUpBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateNewTaskBtn.Visibility = Visibility.Collapsed;
            UpdateTaskBtn.Visibility = Visibility.Visible;
            if(sender is Button button) 
            {
                int id;
                int.TryParse(button.Tag.ToString(), out id);
                var res = await myTaskService.GetFromCondition(x=>x.Id == id);
                MyTask = res.First();
                AddUpdateTaskTextBox.Text = MyTask.Name;
                DescriptionUpdateTaskTextBox.Text= MyTask.Description;

                foreach (var it in AddUpdateStatusCombox.Items)
                {
                    if (it is Status status)
                    {
                        if (status.Id == MyTask.StatusId)
                        {
                            AddUpdateStatusCombox.SelectedItem = status;
                        }
                    }
                
                }

                foreach (var it in AddUpdatePrioritetCombox.Items)
                {
                    if (it is Priority priority)
                    {
                        if (priority.Id == MyTask.PriorityId)
                        {
                            AddUpdatePrioritetCombox.SelectedItem = priority;
                        }
                    }

                }

                foreach (var it in AddUpdateTypeCombox.Items)
                {
                    if (it is TypeTask typeTask)
                    {
                        if (typeTask.Id == MyTask.TypeTaskId)
                        {
                            AddUpdateTypeCombox.SelectedItem = typeTask;
                        }
                    }
                }

                foreach (var it in AddUpdateAutorCombox.Items)
                {
                    if (it is User autor)
                    {
                        if (autor.Id == MyTask.AdminId)
                        {
                            AddUpdateAutorCombox.SelectedItem = autor;
                        }
                    }
                }
                foreach (var it in AddUpdateWorkerCombox.Items)
                {
                    if (it is User worker)
                    {
                        if (worker.Id == MyTask.WorkerId)
                        {
                            AddUpdateWorkerCombox.SelectedItem = worker;
                        }
                    }
                }


                AddUpdateStartPicer.SelectedDate = MyTask.DateStart;
                AddUpdateTimeSpanTextBox.Text = MyTask.TimeSpan.ToString();
               

            }

        }

        private async void UpdateTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            var operationDetail = await myTaskService.UpdateAsync(MyTask.Id, MyTask);
            MessageBox.Show(operationDetail.Message);

            TaskListBox.ItemsSource = await myTaskService.GetAllAsync();
            
            AddUpdateNewTaskBtn.Visibility = Visibility.Visible;
            UpdateTaskBtn.Visibility = Visibility.Collapsed;
            AddUpdateStartPicer.SelectedDate = DateTime.Now;
            AddUpdateTaskTextBox.Text = string.Empty;
            DescriptionUpdateTaskTextBox.Text = string.Empty;
            AddUpdateTimeSpanTextBox.Text = string.Empty;
            MyTask = new MyTask();
        }
    }
}
