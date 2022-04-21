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
    public partial class NewTaskPage : ContentPage
    {
        List<string> pickerSource = new List<string>() { "Low", "Medium", "High", "Critical" };

        public NewTaskPage()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            pickerTaskPriority.ItemsSource = pickerSource;
            pickerTaskPriority.SelectedIndex = 0;
        }

        private async void btnAddTask_Clicked(object sender, EventArgs e)
        {
            if (await Database.Realtime.AddTask(txtTaskDescription.Text, pickerTaskPriority.SelectedItem.ToString()))
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
            txtTaskDescription.Text = "";
            pickerTaskPriority.SelectedIndex = 0;
            await Navigation.PopAsync();
        }
    }
}