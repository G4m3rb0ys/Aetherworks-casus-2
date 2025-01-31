using Aetherworks_casus_2.MVVM.Models;
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
        public async void CreateScheduledActivityReminder(VictuzActivity activity)
        {
            AskPushPermission();
            var request = new NotificationRequest
            {
                NotificationId = Int32.Parse("60" + activity.Id.ToString()),
                Title = "Reminder",
                Subtitle = "Activity start",
                Description = $"{activity.Name} starts in one hour!",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = activity.ActivityDate.AddMinutes(-60)
                }
            };
            await LocalNotificationCenter.Current.Show(request);

            request = new NotificationRequest
            {
                NotificationId = Int32.Parse("15" + activity.Id.ToString()),
                Title = "Reminder",
                Subtitle = "Activity Start",
                Description = $"{activity.Name} starts in 15 minutes!",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = activity.ActivityDate.AddMinutes(-15)
                }
            };
            await LocalNotificationCenter.Current.Show(request);

        }
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
            
            request = new NotificationRequest
            {
                NotificationId = 13378,
                Title = "New Activity!",
                Subtitle = "Bonjours",
                Description = "This is a description",
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(7)
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
