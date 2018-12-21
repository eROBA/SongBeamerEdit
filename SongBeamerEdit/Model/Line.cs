using System.Text.RegularExpressions;
using SongBeamerEdit.FlagsValueConverter;

namespace SongBeamerEdit.Model
{
    public class Line
    {
        public Line() { }
        public Line(string lineText)
        {
            var match = Regex.Match(lineText, @"(##)?([1-4]\s*)?([^\n]*)");
            BitwiseLanguageNr = ushort.TryParse(match.Groups[2].Value, out ushort L) ? (Language)L : Language.None;
            IsImplicit = (BitwiseLanguageNr != Language.None) ? false : true;
            LineText = match.Groups[3].Value;
        }
        public bool IsImplicit { get; set; }
        public Language BitwiseLanguageNr { get; set; }
        public string LineText { get; set; }
    }
}
