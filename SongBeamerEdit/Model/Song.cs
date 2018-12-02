using System.Linq;
using System.Text.RegularExpressions;

namespace SongBeamerEdit.Model
{
    public class Song
    {
        #region Felder
        private string _myText;                                 //Arbeitsvariable für den Songtext
        private readonly ushort[] BinaryLanguages = {1, 3, 7, 15};
        #endregion

        #region Konstruktoren
        public Song(string songText)
        {
            SongOrigText = songText;
            SongAnalyse(songText);
            GenerateVerseList();
        }
        #endregion

        #region Methoden
        public void SongAnalyse(string songText)   //Aufruf immer wenn neuer Songtext geladen wurde oder Änderugen am Songtext gemacht wurden
        {
            _myText = songText;                                                                                         //Übertragung vom übergebenen Text in das Feld der Arbeitsvariable
            Vorspann = Regex.Match(_myText, @"(#.*?\r\n)+", RegexOptions.Singleline).ToString();                        //Ermittelt den Vorspann
            LanguageCount = ushort.TryParse(Regex.Match(Vorspann, "(?<=LangCount=)[0-9]+").Value, out ushort J) ? J : 1;
            LanguageCount = (LanguageCount > 4) ? 4 : LanguageCount;
            SelectedLanguages = (Language)BinaryLanguages[LanguageCount-1];
            _myText = _myText.Substring(Vorspann.Length);                                                               //Nimmt den Vorspann aus der Arbeitsvariable
            _myText = Regex.Replace(_myText, @"(\r\n){2,}", "\r\n", RegexOptions.Multiline);                            //Nimmt mehrfache Zeilenumbrüche heraus
            _myText = Regex.Replace(_myText, @"[ ]{2,}", " ", RegexOptions.Singleline);                                 //Nimmt mehrfache Spaces heraus
            _myText = Regex.Replace(_myText, @"\r\n($)", "$1", RegexOptions.Singleline);                                //Nimmt am Ende vom Lied Zeilenumbrüche heraus
            _myText = Regex.Replace(_myText, @"(-+\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$2$3");    //Wenn vor "Vers", "Bridge", "Refrain"... nur "-" oder "--" steht, wird "---" eingesetzt
            _myText = Regex.Replace(_myText, @"(?<!---\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$&");  //Wenn vor "Vers" oder "Bridge" oder "Refrain" "---" fehlt, werden diese eingesetzt
            _myText = Regex.Replace(_myText, @"\r\n--\r\n", "\r\n", RegexOptions.Multiline);                            //Nimmt vorhandene Seitenrennungen "--" heraus
            _myText = Regex.Replace(_myText, @"(-{3})\r\n(((Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)[ 0-9]*)?\r\n)?(.*?\r\n)", "$1 $3$$$ $5", RegexOptions.Multiline);     //Zieht die Kennzeichnungen in die erste Verszeile
        }

        public void GenerateVerseList()
        {
            VerseList = new VerseCollection(_myText);
        }
        public void GenerateSelectedVerseList(short lgNr)
        {
            var SelectedVerseList = from v in VerseList.Verses
                                    from vv in v.Lines
                                    where vv.LanguageNr == lgNr
                                    select vv;
            var SelectedVerList2 = VerseList.Verses.SelectMany(q => q.Lines).Where(a => a.LanguageNr == lgNr);
            var SelectedVerseList4 = from v in VerseList.Verses
                                     from vv in v.Lines
                                     where vv.LanguageNr == lgNr
                                     select vv;
            var SelectedVerseList5 = from v in VerseList.Verses
                                     from vv in v.Lines
                                     where vv.LanguageNr == lgNr
                                     select v;

            var SelectedVerList6 = new VerseCollection();
            VerseList.Verses.ForEach(p => p.Lines.Where(c => c.LanguageNr == lgNr));
        }
        #endregion

        #region Eigenschaften
        public string SongOrigText { get; set; }
        public static Language SelectedLanguages { get; set; }
        public static int LanguageCount { get; set; }
        public string Vorspann { get; set; }
        public int AnzahlZeilenProSeite { get; set; }
        public VerseCollection VerseList { get; set; } = new VerseCollection();
        public VerseCollection SelectedVerseList { get; set; } = new VerseCollection();

        #endregion
    }
}
