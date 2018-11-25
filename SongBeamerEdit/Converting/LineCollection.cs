using System.Collections.Generic;
using System.Text.RegularExpressions;
using SongBeamerEdit.Model;

namespace SongBeamerEdit.Converting
{
    public class LineCollection
    {
        public LineCollection(string vers, string callSign)
        {
            short languageNr = 0;
            CallSign = callSign;
            string[] versLines = Regex.Split(vers, @"\r\n");
            foreach (string versLine in versLines)
            {
                languageNr += 1;
                Line line = new Line(versLine);
                if (line.IsImplicit)
                {
                line.LanguageNr = languageNr;
                }
                Lines.Add(line);
                if (languageNr == Song.LanguageCount) languageNr = 0;                
            }
        }
        public List<Line> Lines { get; set; } = new List<Line>();
        public string CallSign { get; set; }    //Verskennzeichen wie Vers, Chourus, Refrain ...
    }
}
