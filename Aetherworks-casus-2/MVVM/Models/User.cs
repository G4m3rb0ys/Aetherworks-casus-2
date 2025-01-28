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
        [Column("Name")]
        public string? Name { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
        [Column("Role")]
        public string? Role { get; set; }
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
