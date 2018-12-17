using System.Text.RegularExpressions;
using SongBeamerEdit.FlagsValueConverter;

namespace SongBeamerEdit.Converting
{
    public class Line
    {
        public Line() { }
        public Line(string lineText)
        {
            var match = Regex.Match(lineText, @"(##)?([1-4]\s*)?([^\n]*)");
            LanguageNr = short.TryParse(match.Groups[2].Value, out short J) ? J : 0;    //Nach Umsetzung Bitweise Vergeleiche löschen
            BitwiseLanguageNr = ushort.TryParse(match.Groups[2].Value, out ushort L) ? (Language)L : Language.None;
            IsImplicit = (BitwiseLanguageNr != Language.None) ? false : true;
            LineText = match.Groups[3].Value;
        }
        public bool IsImplicit { get; set; }
        public int LanguageNr { get; set; } //Nach Umsetzung Bitweise Vergleiche löschen
        public Language BitwiseLanguageNr { get; set; }
        public string LineText { get; set; }
    }
}
