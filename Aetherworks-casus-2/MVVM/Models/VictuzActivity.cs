using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.Models
{
    public class VictuzActivity
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Category")]
        public ActivityCategory Category { get; set; }
        [Column("Name")]
        public string? Name { get; set; }
        [Column("Description")]
        public string? Description { get; set; }
        [Column("Picture")]
        public string? Picture { get; set; }
        [Column("LocationId")]
        public int LocationId { get; set; }
        [Ignore]
        public VictuzLocation? Location { get; set; }
        [Column("ActivityDate")]
        public DateTime ActivityDate { get; set; }
        [Column("HostId")]
        public int HostId { get; set; }
        [Ignore]
        public User? Host { get; set; }
        [Column("Price")]
        public decimal Price { get; set; }
        [Column("MemberPrice")]
        public decimal MemberPrice { get; set; }
        [Column("ParticipationLimit")]
        public int ParticipationLimit { get; set; }
        [Ignore]
        public List<Participation>? Participations { get; set; }
    }
}
