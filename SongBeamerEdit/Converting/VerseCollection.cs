using SongBeamerEdit.Converting;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SongBeamerEdit.Model
{
    public class VerseCollection
    {
        public VerseCollection() { }
        public VerseCollection(string songText)
        {
            var rgx = new Regex(@"[-]{3}\s*(.*?)[$]{2}\s*(.*?)(\r\n)?(?=---|$)", RegexOptions.Singleline);    //Filtert Verse aus
            MatchCollection versesText = rgx.Matches(songText);
            foreach (Match verseText in versesText)
            {
                Verses.Add(new LineCollection(verseText.Groups[2].Value, verseText.Groups[1].Value));
            }
        }
        public override string ToString()
        {
            string mySong = string.Empty;
            foreach (var vers in Verses)
            {
                mySong += "---\r\n";
                if (vers.CallSign != string.Empty) mySong += vers.CallSign + "\r\n";
                foreach (var line in vers.Lines)
                {
                    if (!line.IsImplicit)
                    {
                        mySong += "##" + line.LanguageNr + " ";
                    }
                    mySong += line.LineText + "\r\n";
                }
            }
            return mySong;
        }
        public List<LineCollection> Verses { get; set; } = new List<LineCollection>();
    }
}
