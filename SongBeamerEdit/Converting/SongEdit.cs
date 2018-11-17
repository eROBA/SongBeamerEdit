using System;
using System.Text.RegularExpressions;

namespace SongBeamerEdit.Converting
{
    public class SongEdit
    {
        #region Felder
        private string vorspann = string.Empty;         //Text vor dem eigendlichen Liedtext
        private string myText = string.Empty;           //Arbeitsvariable für den Songtext
        MatchEvaluatorManager myMatchEvaluatorManager = new MatchEvaluatorManager();                                //Erstellt ein Objekt in welchem die Matchevaluatoren aufgerufen werden
        #endregion
        #region Konstruktoren
        public SongEdit() { }
        public SongEdit(string songText)
        {
            ConvertToVMFormat(songText);
        }
        #endregion
        #region Hilfmethoden
        public void ConvertToVMFormat(string text)   //Aufruf immer wenn neuer Songtext geladen wurde oder Änderugen am Songtext gemacht wurden
        {
            #region Songanalyse, Bereinigungen und Abtrennung Vorspann
            myText = text;                                                                                          //Übertragung vom übergebenen Text in das Feld der Arbeitsvariable
            vorspann = Regex.Match(myText, @"(#.*?\r\n)+", RegexOptions.Singleline).ToString();                     //Ermittelt den Vorspann
            AnzahlSprachen = (Int32.TryParse((Regex.Match(vorspann, "(?<=LangCount=)[0-9]+").Value), out int J) ? J : 1);
            myText = myText.Substring(vorspann.Length);                                                             //Nimmt den Vorspann aus der Arbeitsvariable
            myText = Regex.Replace(myText, @"(\r\n){2,}", "\r\n", RegexOptions.Multiline);                          //Nimmt mehrfache Zeilenumbrüche heraus
            myText = Regex.Replace(myText, @"[ ]{2,}", " ", RegexOptions.Singleline);                               //Nimmt mehrfache Spaces heraus
            myText = Regex.Replace(myText, @"\r\n($)", "$1", RegexOptions.Singleline);                              //Nimmt am Ende vom Lied Zeilenumbrüche heraus
            myText = Regex.Replace(myText, @"(-+\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$2$3");    //Wenn vor "Vers", "Bridge", "Refrain"... nur "-" oder "--" steht, wird "---" eingesetzt
            myText = Regex.Replace(myText, @"(?<!---\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$&");  //Wenn vor "Vers" oder "Bridge" oder "Refrain" "---" fehlt, werden diese eingesetzt
            myText = Regex.Replace(myText, @"\r\n--\r\n", "\r\n", RegexOptions.Multiline);                          //Nimmt vorhandene Seitenrennungen "--" heraus
            myText = Regex.Replace(myText, @"(-{3})\r\n(((Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)[ 0-9]*)?\r\n)?(.*?\r\n)", "$1 $3$$$ $5", RegexOptions.Multiline);     //Zieht die Kennzeichnungen in die erste Verszeile
            #endregion
        }

        public string SeitenaufteilungSong(int anzahlSprachen)
        {
            myMatchEvaluatorManager.MaxZeilenzahl = AnzahlZeilenProSeite;
            myMatchEvaluatorManager.AnzahlSprachen = anzahlSprachen;
            Regex rgx = new Regex(@"([^$]+[$]{2})(.*?)(?=---|$)", RegexOptions.Singleline);    //Filtert Verse aus
            MatchEvaluator myEvaluator = myMatchEvaluatorManager.MatchEvaluator;
            string text = rgx.Replace(myText, myEvaluator);
            text = Regex.Replace(text, "\r\n--(\r\n---)", "$1");    //Wenn unmittelbar auf einen Seitenumbruch ein Versumbruch folgt, wird der Seitenumbruch gelöscht
            text = VerskennungInEigeneZeilenSchreiben(text);
            text = vorspann + text;
            return text;
        }

        public string SeitenaufteilungPage(int anzahlSprachen)
        {
            return "SeitenaufteilungPage()";
        }

        /// <summary>
        /// Nimmt die Verskennzeichnung aus dem Text und macht eine eigene Zeile daraus.
        /// </summary>
        /// <param name="songText"></param>
        /// <returns></returns>
        private string VerskennungInEigeneZeilenSchreiben(string songText)
        {
            songText = Regex.Replace(songText, @"(-{3})\s*(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)?(\s*[0-9]*)[$]{2}\s*(.*)", "$1\r\n$2$3\r\n$4", RegexOptions.Multiline);
            songText = Regex.Replace(songText, @"(\r\n)+", "\r\n");
            return songText;
        }
        #endregion
        #region Eigenschaften
        public string FileSongText { get; set; }
        public string Ergebnis { get; set; }
        public int AnzahlSprachen { get; set; }
        public int AnzahlZeilenProSeite { get; set; }
        #endregion
    }
}
