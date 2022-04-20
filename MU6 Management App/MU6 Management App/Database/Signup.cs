using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MU6_Management_App.Database
{
    internal class Signup
    {
        static public async Task<bool> Validate(string email, string password)
        {
            if (!String.IsNullOrEmpty(email))
            {
                if (!String.IsNullOrEmpty(password))
                {
                    try
                    {

                        var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Config.GetAPIKey()));
                        var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                        await App.Current.MainPage.DisplayAlert("Alert", "User " + email + " Created!", "OK");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        FirebaseAuthException error = ex as FirebaseAuthException;
                        await App.Current.MainPage.DisplayAlert("Alert", error.Reason.ToString(), "Ok");
                        return false;
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Password field cannot be empty!", "Ok");
                    return false;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Username/Email field cannot be empty!", "Ok");
                return false;
            }
        }
    }
}
