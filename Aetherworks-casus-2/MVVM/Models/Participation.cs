using Aetherworks_casus_2.MVVM.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.Models
{
    public class Participation
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }
        [Ignore]
        public User? User { get; set; }
        [Column("ActivityId")]
        public int ActivityId { get; set; }
        [Ignore]
        public VictuzActivity? Activity { get; set; }
        [Column("Attend")]
        public bool Attend { get; set; }

        public void CreateNotifications()
        {
            new NotificationsViewModel().CreateScheduledActivityReminder(Activity);
        }
    }
}
