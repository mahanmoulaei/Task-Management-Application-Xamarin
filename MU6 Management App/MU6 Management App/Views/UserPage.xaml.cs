using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MU6_Management_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        int globalSelectedItemIndex = -1;
        public UserPage()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            Initialize();
        }

        private void Initialize()
        {
            listViewTasks.ItemsSource = null;
            listViewTasks.ItemsSource = Database.Realtime.GetAllTasks();
            listViewTasks.SelectedItem = null;
            SetButtonsEnablement(false);
        }

        private void SetButtonsEnablement(bool state)
        {
            btnEditTask.IsEnabled = btnDeleteTask.IsEnabled = state;
        }

        private void listViewTasks_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItemIndex >= 0)
            {
                globalSelectedItemIndex = e.SelectedItemIndex;
                SetButtonsEnablement(true);
            }
            else
            {
                SetButtonsEnablement(false);
                //globalSelectedItemIndex = -1;
            }
        }

        private async void btnAddTask_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewTaskPage());
        }

        private async void btnEditTask_Clicked(object sender, EventArgs e)
        {
            Models.Task task = (Models.Task)listViewTasks.SelectedItem;
            var taskDetail = new EditTaskPage(task);
            taskDetail.BindingContext = task;
            SetButtonsEnablement(false);
            await Navigation.PushAsync(taskDetail);

        }

        private async void btnDeleteTask_Clicked(object sender, EventArgs e)
        {
            Models.Task task = (Models.Task)listViewTasks.SelectedItem;
            bool respond = await DisplayAlert("Alert", "Are you sure you want to delete the Task " + task.ID + " ?", "Yes", "No");
            if (respond)
            {
                if (await Database.Realtime.DeleteTask(task.ID))
                {
                    SetButtonsEnablement(false);
                }
            }      
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}