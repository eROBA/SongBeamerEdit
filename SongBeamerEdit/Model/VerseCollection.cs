using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SongBeamerEdit.Model
{
    public class VerseCollection
    {
        public VerseCollection() { }
        public VerseCollection(string songText, int languageCount)
        {
            MaxVerseLinesCount = 0;
            var rgx = new Regex(@"[-]{3}\s*(.*?)[$]{2}\s*(.*?)(\r\n)?(?=---|$)", RegexOptions.Singleline);    //Filtert Verse aus
            MatchCollection versesText = rgx.Matches(songText);
            foreach (Match verseText in versesText)
            {
                Verses.Add(new LineCollection(verseText.Groups[2].Value, verseText.Groups[1].Value, languageCount));
                if (versesText.Count > MaxVerseLinesCount) MaxVerseLinesCount  = versesText.Count;
            }
        }

        public override string ToString()
        {
            string mySong = string.Empty;
            foreach (var vers in Verses)
            {
                mySong += "---\r\n";
                if (!string.IsNullOrEmpty(vers.CallSign))
                {
                    mySong += vers.CallSign + "\r\n";
                }
                foreach (var line in vers.Lines)
                {
                    if (!line.IsImplicit)
                    {
                        mySong += "##" + (int)line.BitwiseLanguageNr + " ";
                    }
                    mySong += line.LineText + "\r\n";
                }
            }
            return mySong;
        }
        public List<LineCollection> Verses { get; set; } = new List<LineCollection>();
        /// <summary>
        /// Höchste Anzahl von Verszeilen innerhalb eines Songs
        /// </summary>
        public int MaxVerseLinesCount { get; set; }

    }
}
