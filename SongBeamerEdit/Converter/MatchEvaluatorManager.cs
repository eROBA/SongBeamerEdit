using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SongBeamerEdit
{
    public class MatchEvaluatorManager
    {
        public string MatchEvaluator(Match m)
        {
            string Seite = m.Groups[EditGroupNr].Value;
            Seite = Regex.Replace(Seite, "^(.*?\r\n){" + MaxZeilenzahl * AnzahlSprachen + "}", "$&--\r\n", RegexOptions.Multiline);
            return m.Groups[1].Value + Seite;
        }
        public int MaxZeilenzahl { get; set; }
        public int AnzahlSprachen { get; set; }
        public int EditGroupNr { get; set; }
    }
}
