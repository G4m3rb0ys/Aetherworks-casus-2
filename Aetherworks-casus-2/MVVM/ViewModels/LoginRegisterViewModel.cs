using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Models;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public class LoginRegisterViewModel
    {
        public LocalDbService _db = new LocalDbService();
        public async Task<User?> GetUserFromDB(string EmailOrUsername)
        {
            return await _db.GetUser(EmailOrUsername);
        }
        public void LogIn(User loggedInUser)
        {
            SessionService.LogIn(loggedInUser);
        }
        public void RegisterNewUser()
        {

        }
    }
}
