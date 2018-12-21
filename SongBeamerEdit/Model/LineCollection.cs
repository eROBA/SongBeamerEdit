using System.Collections.Generic;
using System.Text.RegularExpressions;
using SongBeamerEdit.Model;
using SongBeamerEdit.FlagsValueConverter;
using System;

namespace SongBeamerEdit.Model
{
    public class LineCollection
    {
        public LineCollection() { }
        public LineCollection(string vers, string callSign)
        {
            CallSign = callSign;
            string[] versLines = Regex.Split(vers, @"\r\n");            //Teilt einen Vers in Zeilen auf
            int lngNr = 0;
            foreach (string versLine in versLines)              
            {
                Line line = new Line(versLine);
                if (line.BitwiseLanguageNr == Language.None)            //Wenn der Vers keine Explizite Versangabe wie z.B. ##2 hat
                {
                    if (lngNr == Song.LanguageCount) lngNr = 0;         //Schleife über Anzahl der Sprachen
                    line.BitwiseLanguageNr = (Language)(int)Math.Pow(2.0, lngNr);
                    lngNr++;
                }
                Lines.Add(line);
            }
        }
        public List<Line> Lines { get; set; } = new List<Line>();
        public string CallSign { get; set; }    //Verskennzeichen wie Vers, Chourus, Refrain ...
    }
}
