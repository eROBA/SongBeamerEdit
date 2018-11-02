using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SongBeamerEdit.Converting
{
    public class SongEdit
    {
        #region Felder
        string vorspann = string.Empty;         //Text vor dem eigendlichen Liedtext
        string myText = string.Empty;           //Arbeitsvariable für den Songtext
        MatchEvaluatorManager myMatchEvaluatorManager = new MatchEvaluatorManager();                                //Erstellt ein Objekt in welchem die Matchevaluatoren aufgerufen werden
        #endregion
        #region Konstruktoren
        public SongEdit() { }
        public SongEdit(string text)
        {
            ConvertToVMFormat(text);
        }
        #endregion
        #region Hilfmethoden
        public string ConvertToVMFormat(string text)
        {
            #region Songanalyse, Bereinigungen und Abtrennung Vorspann
            myText = text;                                                                                          //Übertragung vom übergebenen Text in das Feld der Arbeitsvariable
            vorspann = Regex.Match(myText, @"(#.*?\r\n)+", RegexOptions.Singleline).ToString();                     //Ermittelt den Vorspann
            //AnzahlSprachen = Convert.ToInt32(Regex.Match(vorspann, "(?<=LangCount=)[0-9]+").Value);                 //Ermittelt aus der Angabe im Vorspann die Anzahl der Sprachen
            AnzahlSprachen = (Int32.TryParse((Regex.Match(vorspann, "(?<=LangCount=)[0-9]+").Value), out int J) ? J : 1);
            myText = myText.Substring(vorspann.Length);                                                             //Nimmt den Vorspann aus der Arbeitsvariable
            myText = Regex.Replace(myText, @"(\r\n){2,}", "\r\n", RegexOptions.Multiline);                          //Nimmt mehrfache Zeilenumbrüche heraus
            myText = Regex.Replace(myText, @"[ ]{2,}", " ", RegexOptions.Singleline);                               //Nimmt mehrfache Spaces heraus
            myText = Regex.Replace(myText, @"\r\n($)", "$1", RegexOptions.Singleline);                              //Nimmt am Ende vom Lied Zeilenumbrüche heraus
            myText = Regex.Replace(myText, @"(-+\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$2$3");    //Wenn vor "Vers", "Bridge", "Refrain"... nur "-" oder "--" steht, wird "---" eingesetzt
            myText = Regex.Replace(myText, @"(?<!---\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$&");  //Wenn vor "Vers" oder "Bridge" oder "Refrain" "---" fehlt, werden diese eingesetzt
            myText = Regex.Replace(myText, @"\r\n--\r\n", "\r\n", RegexOptions.Multiline);                          //Nimmt vorhandene Seitenrennungen "--" heraus
            myText = Regex.Replace(myText, @"(-{3})\r\n(((Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)[ 0-9]*)?\r\n)?(.*?\r\n)", "$1 $3$$$ $5", RegexOptions.Multiline);     //Zieht die Kennzeichnungen in die erste Verszeile
            AnzahlSichererVerswechsel = Regex.Matches(myText, @"(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)[ 0-9]*[$]{2}", RegexOptions.Multiline).Count;                  //Ermittelt Anzahl "Vers" oder "Bridge" oder "Refrain" Kennzeichnungen
            AnzahlUnsichererVerswechsel = Regex.Matches(myText, @"^---\s*[$]{2}", RegexOptions.Multiline).Count;    //Ermittelt Anzahl "---"Kennzeichnungen
            AnzahlVerszahlen = Regex.Matches(myText, @"---\s*Vers[ 0-9]+\s*[$]{2}", RegexOptions.Multiline).Count;  //Ermittelt Anzahl "Vers" Kennzeichnungen mit Versnummern
            AnzahlVerseOhneVerszahlen = Regex.Matches(myText, @"---\s*Vers\s*[$]{2}").Count;                        //Ermittelt Anzahl "Vers" Kennzeichnungen ohne Versnummern
            myMatchEvaluatorManager.MaxZeilenzahl = MaxAnzahlZeilenProSeite;
            myMatchEvaluatorManager.AnzahlSprachen = AnzahlSprachen;
            #endregion

            #region Songtextbearbeitung mit Variantenunterscheidung
            bool Variante1 = AnzahlVerseOhneVerszahlen == 0 && AnzahlUnsichererVerswechsel == 0;    //Wenn keine "Vers" - Kennzeichnung vorhanden ist --> gibt es nur einen Vers
            bool Variante2 = AnzahlVerseOhneVerszahlen == 1;                                        //Wenn nur eine "Vers" - Kennzeichnung ohne Versnummer vorhanden ist --> gibt es nur einen Vers
            bool Variante3 = AnzahlSichererVerswechsel == 0 && AnzahlUnsichererVerswechsel >= 1;    //Wenn nur mehrere "---" - Kennzeichnung vorhanden sind
            Variante3 = true;
            if (Variante1)
            {
                Variante3 = true;
            }
            if (Variante2)
            {
                Variante3 = true;
            }
            if (Variante3)
            {
                Regex rgx3 = new Regex(@"([^$]+[$]{2})(.*?)(?=---|$)", RegexOptions.Singleline);    //Ermittelt den Verstext
                myMatchEvaluatorManager.EditGroupNr = 2;                                            //Übergibt die Regex-Gruppennummer in der der Verstext zu finden ist
                Ergebnis = vorspann + MehrereVerseOhneVers(rgx3);                                   //Führt den Vorspann und das Ergebnis der Konvertierung zusammen und legt diese in der Eigenschaft Ergebis ab
            }
            return Ergebnis;
            #endregion
        }
        private string MehrereVerseOhneVers(Regex rgx)
        {
            MatchEvaluator myEvaluator = myMatchEvaluatorManager.MatchEvaluator;
            string text = rgx.Replace(myText, myEvaluator);
            text = Regex.Replace(text, "\r\n--(\r\n---)", "$1");
            text = VerskennungInEigeneZeilenSchreiben(text);
            return text;
        }
        private string VerskennungInEigeneZeilenSchreiben(string myText)
        {
            myText = Regex.Replace(myText, @"(-{3})\s*(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)?(\s*[0-9]*)[$]{2}\s*(.*)", "$1\r\n$2$3\r\n$4", RegexOptions.Multiline);
            myText = Regex.Replace(myText, @"(\r\n)+", "\r\n");
            return myText;
        }

        #endregion
        #region Eigenschaften
        public string Ergebnis { get; set; }
        public int AnzahlSprachen { get; set; } = 1;
        public int MaxAnzahlZeilenProSeite { get; set; } = 2;
        public int AnzahlSichererVerswechsel { get; set; }
        public int AnzahlUnsichererVerswechsel { get; set; }
        public int AnzahlVerszahlen { get; set; }
        public int AnzahlVerseOhneVerszahlen { get; set; }
        #endregion
    }
}
