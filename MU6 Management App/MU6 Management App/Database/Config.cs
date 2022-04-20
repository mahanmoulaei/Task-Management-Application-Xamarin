using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace MU6_Management_App.Database
{
    internal class Config
    {
        private static string apiKey = "AIzaSyClYNFw2XYN3FVsT_WgMFsOn46L8RXSZio";
        private FirebaseClient firebaseClient = new FirebaseClient("https://mu6-final-exam-b7def-default-rtdb.firebaseio.com");

        public static string GetAPIKey()
        {
            return apiKey;
        }

        public FirebaseClient GetFirebaseClient()
        {
            return firebaseClient;
        }

    }
}
