using System.Text.RegularExpressions;

namespace SongBeamerEdit.Converting
{
    public class Line
    {
        public Line(string lineText)
        {
            var match = Regex.Match(lineText, @"(##)?([1-4]\s*)?([^\n]*)");
            LanguageNr = short.TryParse(match.Groups[2].Value, out short J) ? J : 0;
            if (LanguageNr == 0)
            {
                IsImplicit = true;
                LanguageNr = 1;
            }
            LineText = match.Groups[3].Value;
        }
        public bool IsImplicit { get; set; }
        public int LanguageNr { get; set; }
        public string LineText { get; set; }
    }
}
