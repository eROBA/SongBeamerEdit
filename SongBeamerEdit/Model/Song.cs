using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SongBeamerEdit.FlagsValueConverter;

namespace SongBeamerEdit.Model
{
    public class Song
    {
        #region Felder
        private string _myText;                                         //Arbeitsvariable für den Songtext
        #endregion

        #region Konstruktoren
        public Song() { }
        public Song(string songText)
        {
            OrigLoadedSongText = songText;
            SongAnalyse(songText);
            VerseList = new VerseCollection(_myText);
        }
        #endregion

        #region Methoden
        public void SongAnalyse(string songText)   //Aufruf immer wenn neuer Songtext geladen wurde oder Änderugen am Songtext gemacht wurden
        {
            _myText = songText;                                                                                         //Übertragung vom übergebenen Text in das Feld der Arbeitsvariable
            Vorspann = Regex.Match(_myText, @"(#.*?\r\n)+", RegexOptions.Singleline).ToString();                        //Ermittelt den Vorspann
            LanguageCount = int.TryParse(Regex.Match(Vorspann, "(?<=LangCount=)[0-9]+").Value, out int J) ? J : 1;
            LanguageCount = (LanguageCount > 4) ? 4 : LanguageCount;
            SelectedLanguages = (Language)(int)Math.Pow(2.0, LanguageCount)-1;
            _myText = _myText.Substring(Vorspann.Length);                                                               //Nimmt den Vorspann aus der Arbeitsvariable
            _myText = Regex.Replace(_myText, @"(\r\n){2,}", "\r\n", RegexOptions.Multiline);                            //Nimmt mehrfache Zeilenumbrüche heraus
            _myText = Regex.Replace(_myText, @"[ ]{2,}", " ", RegexOptions.Singleline);                                 //Nimmt mehrfache Spaces heraus
            _myText = Regex.Replace(_myText, @"\r\n($)", "$1", RegexOptions.Singleline);                                //Nimmt am Ende vom Lied Zeilenumbrüche heraus
            _myText = Regex.Replace(_myText, @"(-+\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$2$3");    //Wenn vor "Vers", "Bridge", "Refrain"... nur "-" oder "--" steht, wird "---" eingesetzt
            _myText = Regex.Replace(_myText, @"(?<!---\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$&");  //Wenn vor "Vers" oder "Bridge" oder "Refrain" "---" fehlt, werden diese eingesetzt
            _myText = Regex.Replace(_myText, @"\r\n--\r\n", "\r\n", RegexOptions.Multiline);                            //Nimmt vorhandene Seitenrennungen "--" heraus
            _myText = Regex.Replace(_myText, @"(-{3})\r\n(((Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)[ 0-9]*)?\r\n)?(.*?\r\n)", "$1 $3$$$ $5", RegexOptions.Multiline);     //Zieht die Kennzeichnungen in die erste Verszeile
        }

        public void GenerateSelectedVerseList(Language lang)
        {
            SelectedVerseList.Verses = 
                (from verse in VerseList.Verses
                select new LineCollection()
                {
                    CallSign = verse.CallSign,
                    Lines = verse.Lines.Where(line => (lang & line.BitwiseLanguageNr) == line.BitwiseLanguageNr).ToList()
                }).ToList();
        }

        public void GenerateMaxLineVersList(int maxLines)
        {
            foreach (var verLineCollection in SelectedVerseList.Verses)
            {
                var maxLineCollection = new LineCollection();
                int count = 0;
                for (int i = 0; i < verLineCollection.Lines.Count; i++)
                {
                    maxLineCollection.Lines.Add(verLineCollection.Lines[i]);
                    count++;
                    if (count == maxLines )
                    {
                        if (verLineCollection.Lines.Count != i+1) maxLineCollection.Lines.Add(new Line{LineText = "--", IsImplicit = true});
                        count = 0;
                    }
                }
                maxLineCollection.CallSign = verLineCollection.CallSign;
                SelectedVerseListMaxLines.Verses.Add(maxLineCollection);
            }
        }

        #endregion

        #region Eigenschaften
        public static Language SelectedLanguages { get; set; }
        public static int LanguageCount { get; private set; }
        public string OrigLoadedSongText { get; set; }
        public string Vorspann { get; set; }
        public int AnzahlZeilenProSeite { get; set; }
        public VerseCollection VerseList { get; set; } = new VerseCollection();
        public VerseCollection SelectedVerseList { get; set; } = new VerseCollection();
        public VerseCollection SelectedVerseListMaxLines { get; set; } = new VerseCollection();
        #endregion
    }
}
