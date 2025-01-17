using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.Models
{
    public class Penalty
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("UserId")]
        public int UserID { get; set; }
        [Ignore]
        public User? User { get; set; }
        [Column("EndDate")]
        public DateTime EndDate { get; set; }
    }
}
