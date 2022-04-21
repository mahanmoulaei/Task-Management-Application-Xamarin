using Firebase.Database.Query;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MU6_Management_App.Database
{
    class Realtime
    {
        private static int serverID; //For showing the key(primary key) on the list and be able to track it as the question instruction asks us
        static public ObservableCollection<Models.Task> GetAllTasks()
        {
            serverID = 0;
            Config.allTasks.Clear();
            var collection = Config.GetFirebaseClient().Child(Config.GetTasksTableName()).AsObservable<Models.Task>().Subscribe(item =>
            {
                if (item.Object != null) //Validation
                {
                    Config.allTasks.Add(new Models.Task() { ID = item.Object.ID, Key = item.Key, Description = item.Object.Description, Priority = item.Object.Priority });
                    serverID++;
                }
            });
            return Config.allTasks;
        }

        static public async Task<bool> AddTask(string taskDescription, string taskPriority)
        {
            if (!String.IsNullOrEmpty(taskDescription))
            {
                if (!String.IsNullOrEmpty(taskPriority))
                {
                    try
                    {
                        await Config.GetFirebaseClient().Child(Config.GetTasksTableName()).PostAsync(new Models.Task() { ID = serverID + 1, Description = taskDescription, Priority = taskPriority });
                        await App.Current.MainPage.DisplayAlert("Alert", "Task created successfully!", "Ok");
                        return true;
                    }
                    catch
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Error at creating the task! Try again...", "Ok");
                        return false;
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Task Priority field cannot be empty!", "Ok");
                    return false;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Task Description field cannot be empty!", "Ok");
                return false;
            }
        }

        static public async Task<bool> DeleteTask(int taskID)
        {
            var taskToDelete = (await Config.GetFirebaseClient().Child(Config.GetTasksTableName()).OnceAsync<Models.Task>()).Where(item => item.Object.ID == taskID).FirstOrDefault();
            if (taskToDelete != null)
            {
                try
                {
                    await Config.GetFirebaseClient().Child(Config.GetTasksTableName()).Child(taskToDelete.Key).DeleteAsync();
                    GetAllTasks();
                    await App.Current.MainPage.DisplayAlert("Alert", "Task deleted successfully!", "Ok");
                    return true;
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Error happenned at deleting the task! Try again or contact the adminstrator for help...", "Ok");
                    return false;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Error happenned at deleting the task! No such task exists! Try again or contact the adminstrator for help...", "Ok");
                return false;
            }
        }

        static public async Task<bool> EditTask(int taskID, string taskDescription, string taskPriority)
        {
            var taskToUpdate = (await Config.GetFirebaseClient().Child(Config.GetTasksTableName()).OnceAsync<Models.Task>()).Where(item => item.Object.ID == taskID).FirstOrDefault();
            if (taskToUpdate != null)
            {
                if (!String.IsNullOrEmpty(taskDescription))
                {
                    if (!String.IsNullOrEmpty(taskPriority))
                    {
                        try
                        {
                            await Config.GetFirebaseClient().Child(Config.GetTasksTableName()).Child(taskToUpdate.Key).PutAsync(new Models.Task() { ID = taskID, Description = taskDescription, Priority = taskPriority });
                            GetAllTasks();
                            await App.Current.MainPage.DisplayAlert("Alert", "Task edited successfully!", "Ok");
                            return true;
                        }
                        catch
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Error happenned at editing the task! Try again or contact the adminstrator for help...", "Ok");
                            return false;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Task Priority field cannot be empty!", "Ok");
                        return false;
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Task Description field cannot be empty!", "Ok");
                    return false;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Error happenned at editing the task! No such task exists! Try again or contact the adminstrator for help...", "Ok");
                return false;
            }
        }
    } 
}
