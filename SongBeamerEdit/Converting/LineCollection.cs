using System.Collections.Generic;
using System.Text.RegularExpressions;
using SongBeamerEdit.Model;
using SongBeamerEdit.FlagsValueConverter;


namespace SongBeamerEdit.Converting
{
    public class LineCollection
    {
        public LineCollection() { }
        public LineCollection(string vers, string callSign)
        {
            CallSign = callSign;
            string[] versLines = Regex.Split(vers, @"\r\n");    //Teilt einen Vers in Zeilen auf
            Language[] LanguagesArray = {Language.Lang0, Language.Lang1, Language.Lang2, Language.Lang3};
            int lngNr = 0;
            //Fügt den Verszeilen die Sprachnummer hinzu
            foreach (string versLine in versLines)              
            {
                Line line = new Line(versLine);
                if (line.BitwiseLanguageNr == Language.None)              //Wenn der Vers keine Explizite Versangabe wie z.B. ##2 hat
                {
                    if (lngNr == Song.LanguageCount) lngNr = 0;         //Schleife über Anzahl der Sprachen
                    line.LanguageNr = lngNr + 1;
                    line.BitwiseLanguageNr = LanguagesArray[lngNr];
                    lngNr++;                                            //
                }
                Lines.Add(line);
            }
        }
        public List<Line> Lines { get; set; } = new List<Line>();
        public string CallSign { get; set; }    //Verskennzeichen wie Vers, Chourus, Refrain ...
    }
}
