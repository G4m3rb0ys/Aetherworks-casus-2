using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aetherworks_casus_2.MVVM.Models
{
    public class SuggestionLiked
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("SuggestionId")]
        public int SuggestionId { get; set; }
        [Ignore]
        public Suggestion? Suggestion { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }
        [Ignore]
        public User? User { get; set; }
    }
}
