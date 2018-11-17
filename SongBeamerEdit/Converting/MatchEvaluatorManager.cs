using System.Text.RegularExpressions;

namespace SongBeamerEdit.Converting
{
    public class MatchEvaluatorManager
    {
        public string MatchEvaluator(Match vers)
        {
            string Seite = vers.Groups[2].Value;
            Seite = Regex.Replace(Seite, "^(.*?\r\n){" + MaxZeilenzahl * AnzahlSprachen + "}", "$&--\r\n", RegexOptions.Multiline);
            return vers.Groups[1].Value + Seite;
        }
        public int MaxZeilenzahl { get; set; }
        public int AnzahlSprachen { get; set; }
    }
}
