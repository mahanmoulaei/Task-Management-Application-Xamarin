using Firebase.Database;
using System.Collections.ObjectModel;

namespace MU6_Management_App.Database
{
    internal class Config
    {
        private static string apiKey = "AIzaSyClYNFw2XYN3FVsT_WgMFsOn46L8RXSZio";
        private static FirebaseClient firebaseClient = new FirebaseClient("https://mu6-final-exam-b7def-default-rtdb.firebaseio.com/");
        public static ObservableCollection<Models.Task> allTasks = new ObservableCollection<Models.Task>();
        private static string tasksTableName = "Tasks";

        public static string GetAPIKey()
        {
            return apiKey;
        }

        public static FirebaseClient GetFirebaseClient()
        {
            return firebaseClient;
        }

        public static string GetTasksTableName()
        {
            return tasksTableName;
        }

    }
}
