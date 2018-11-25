using System.Text.RegularExpressions;

namespace SongBeamerEdit.Model
{
    public class Song
    {
        #region Felder
        private string _myText;                                 //Arbeitsvariable für den Songtext
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
            LanguageCount = int.TryParse(Regex.Match(Vorspann, "(?<=LangCount=)[0-9]+").Value, out int J) ? J : 1;
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
        #endregion

        #region Eigenschaften
        public string SongOrigText { get; set; }
        public static int LanguageCount { get; set; }
        public string Vorspann { get; set; }
        public int AnzahlZeilenProSeite { get; set; }
        public VerseCollection VerseList { get; set; } = new VerseCollection();
        #endregion
    }
}
