using System.Text.RegularExpressions;

namespace SongBeamerEdit.BatchProcessingModel
{
    /// <summary>
    /// Ermittelt Informationen aus dem Filenamen einer Songbeamerdatei
    /// </summary>
    /// <param name="fullPath"></param>
    public class SongFilename
    {
        /// <summary>
        /// Analysiert den Filenamen
        /// </summary>
        /// <param name="fullPath"></param>
        public SongFilename(string fullPath)
        {
            ID = IDCounter++;
            OrigFilename = System.IO.Path.GetFileName(fullPath);
            string editFilename = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd();
            Path = System.IO.Path.GetDirectoryName(fullPath);
            //Nimmt ChurchSongID und evtl. einen Index gefolgt von " - " am Anfang vom Filenamen heraus und fügt die ermittelte Zahl als Source ein
            Regex praefixNo = new Regex(@"((^[0-9]+)(\s?[a-z])?)\s-\s(.*?$)", RegexOptions.Multiline);
            Match No = praefixNo.Match(editFilename);
            if (No.Value != string.Empty)
            {
                ChurchSongID = No.Groups[2].ToString() + No.Groups[3].ToString();
                editFilename = No.Groups[4].ToString();
            }
            //Ermittelt den Quellenangaben am Ende vom Filenamen
            Regex rgxSource = new Regex(@"(.*?)\s*-\s*([a-zäüößA-ZÄÜÖ0-9, ]+[0-9,])$", RegexOptions.Multiline);
            Match rgxMatch = rgxSource.Match(editFilename);
            if (rgxMatch.Value != string.Empty)
            {
                Songbook = rgxMatch.Groups[2].ToString().TrimEnd();
                editFilename = rgxMatch.Groups[1].ToString();
            }
            //Ermittelt den alternativen Titel wenn dieser zwischen " - " und dem Ende eingebettet ist, d.h. wenn am Ende keine Quellenangabe mehr folgt
            Regex altTitle2 = new Regex(@"(.*?)\s*(\((.*?)\)?)?\s*-\s*(.*?)\s*(\((.*?)\)?)?($| - )", RegexOptions.Multiline);
            Match altFn2 = altTitle2.Match(editFilename);
            if (altFn2.Value != string.Empty)
            {
                editFilename = altFn2.Groups[1].Value;
                TitleAlternativ = altFn2.Groups[3].Value;
                TitleSecondLang = altFn2.Groups[4].Value;
                TitelAlternativSecondLang = altFn2.Groups[6].Value;
            }
            //Ermittelt in Klammern angegebenen Alternativtitel wenn nur eine Sprache im Dateinamen enthalten ist
            Regex altTitle3 = new Regex(@"(.*?) [(](.*?)[)]$", RegexOptions.Multiline);
            Match altFn3 = altTitle3.Match(editFilename);
            if (altFn3.Value != string.Empty)
            {
                TitleAlternativ = altFn3.Groups[2].ToString();
                editFilename = altFn3.Groups[1].ToString();
            }
            Title = editFilename;
        }
        /// <summary>
        /// Filename (z.B. 'til the day I die.sng)
        /// </summary>
        public string OrigFilename { get; set; }

        /// <summary>
        /// Pfad (z.B. C:\Users\rolf\Documents\SongBeamer)
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Titel (z.B. Agnus Dei (Halleluja))
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Alternativer Titel (z.B. statt Agnus Dei -> Halleluja)
        /// </summary>
        public string TitleAlternativ { get; set; }

        /// <summary>
        /// Quellenangabe (z.B. FJ2 12)
        /// </summary>
        public string Songbook { get; set; }

        /// <summary>
        /// Titel einer weiteren Sprache z.B. der Name Jesus in Dateiname "011 - Groß und wunderbar - der Name Jesus.sng"
        /// </summary>
        public string TitleSecondLang { get; set; }

        /// <summary>
        /// Alternativer Titel einer weiteren Sprache z.B. Show your power in "Du bist der Herr und Du hast die Macht - He is the Lord and he reigns on high (Show your power) - FJ2 121.sng"
        /// </summary>
        public string TitelAlternativSecondLang { get; set; }

        /// <summary>
        /// ID einer Gemeinde internen Songnummerierung z.B. 000 in "000 - Test3Verse5Zeilen1Sprache.sng"
        /// </summary>
        public string ChurchSongID { get; set; }

        /// <summary>
        /// ID des Songfile
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Zähler über alle verarbeideten Songfiles
        /// </summary>
        public static int IDCounter { get; set; }
    }
}
