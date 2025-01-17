using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.Models
{
    public class Suggestion
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Title")]
        public string? Title { get; set; }
        [Column("Description")]
        public string? Description { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }
        [Ignore]
        public User? User { get; set; }
        [Ignore]
        public List<SuggestionLiked>? SuggestionLikeds { get; set; }
    }
}
