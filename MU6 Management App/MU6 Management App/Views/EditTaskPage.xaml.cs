using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MU6_Management_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTaskPage : ContentPage
    {
        private List<string> pickerSource = new List<string>() { "Low", "Medium", "High", "Critical" };
        Models.Task globakTask;
        public EditTaskPage(Models.Task task)
        {
            InitializeComponent();
            globakTask = task;
            Initialize();
        }

        private void Initialize()
        {
            pickerTaskPriority.ItemsSource = pickerSource;
            pickerTaskPriority.SelectedIndex = 0;
            pickerTaskPriority.SelectedIndex = pickerSource.FindIndex(item => item == globakTask.Priority);
        }


        private async void btnEditTask_Clicked(object sender, EventArgs e)
        {
            if (await Database.Realtime.EditTask(Convert.ToInt32(txtTaskID.Text), txtTaskDescription.Text, pickerTaskPriority.SelectedItem.ToString()))
            {
                GoBackToPreviousPage();
            }
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            GoBackToPreviousPage();
        }

        private async void GoBackToPreviousPage()
        {
            txtTaskID.Text = txtTaskKey.Text = txtTaskDescription.Text = "";
            await Navigation.PopAsync();
        }
    }
}