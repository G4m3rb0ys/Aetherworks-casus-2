using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public class NotificationsViewModel
    {
        public async void ShowNotification()
        {
            AskPushPermission();
            var request = new NotificationRequest
            {
                NotificationId = 1337,
                Title = "New Activity!",
                Subtitle = "Bonjours",
                Description = "This is a description",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5)
                }
            };

            await LocalNotificationCenter.Current.Show(request);
        }
        public async void AskPushPermission()
        {
            if (!await LocalNotificationCenter.Current.AreNotificationsEnabled())
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }
        }
    }
}
