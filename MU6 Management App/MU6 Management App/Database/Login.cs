using Firebase.Auth;
using System;
using System.Threading.Tasks;

namespace MU6_Management_App.Database
{
    internal class Login
    {
        static public async Task<bool> Validate(string email, string password)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Config.GetAPIKey()));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (Exception ex)
            {
                FirebaseAuthException error = ex as FirebaseAuthException;
                await App.Current.MainPage.DisplayAlert("Alert", error.Reason.ToString(), "OK");
                return false;
            }
        }
    }
}
