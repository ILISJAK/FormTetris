using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FormTetris
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
