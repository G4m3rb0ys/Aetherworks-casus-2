using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.Models
{
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("IsAdmin")]
        public bool IsAdmin { get; set; }
        [Column("Username"), Unique]
        public string? Username { get; set; }
        [Column("CapitalizedUsername"), Unique]
        public string? CapitalizedUsername { get; set; }
        [Column("Email"), Unique]
        public string? Email { get; set; }
        [Column("CapitalizedEmail"), Unique]
        public string? CapitalizedEmail { get; set; }
        [Column("Name")]
        public string? Name { get; set; }
        [Column("PhoneNumber"), Unique]
        public string? PhoneNumber { get; set; }
        [Column("Password")]
        public string? Password { get; set; }
        [Ignore]
        public List<Participation>? Participations { get; set; }
        [Ignore]
        public List<SuggestionLiked>? SuggestionLikeds { get; set; }
        [Ignore]
        public List<Suggestion>? Suggestions { get; set; }
        [Ignore]
        public List<Penalty>? Penalties { get; set; } 
    }
}
