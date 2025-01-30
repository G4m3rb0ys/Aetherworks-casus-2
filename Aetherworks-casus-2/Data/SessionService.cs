using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetherworks_casus_2.MVVM.Models;

namespace Aetherworks_casus_2.Data
{
    public static class SessionService
    {
        private static User? _loggedInUser;
        public static User LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                _loggedInUser = value;
            }
        }

        public static bool IsUserLoggedIn => _loggedInUser != null;

        public static void LogOut()
        {
            LoggedInUser = null;
            SecureStorage.Default.Remove("rememberUserLogin");
        }
        public static void LogIn(User user)
        {
            LoggedInUser = user;
            SecureStorage.Default.SetAsync("rememberUserLogin", user.Id.ToString());
        }
    }
}
