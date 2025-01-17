using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.Models
{
    public class VictuzLocation
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string? Name { get; set; }
        [Column("MaxCapacity")]
        public int MaxCapacity { get; set; }
    }
}
