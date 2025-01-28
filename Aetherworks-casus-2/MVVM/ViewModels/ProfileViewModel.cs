using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.Data;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public class ProfileViewModel
    {
        public User? LoggedInUser { get; set; } = SessionService.LoggedInUser;
        public string? Username { get; set; } = SessionService.LoggedInUser.Username;
        public string? Email { get; set; } = SessionService.LoggedInUser.Email;
        public string? Name { get; set; } = SessionService.LoggedInUser.Name;
        public string? PhoneNumber { get; set; } = SessionService.LoggedInUser.PhoneNumber;

        public void LogOut()
        {
            SessionService.LogOut();
        }
    }
}
