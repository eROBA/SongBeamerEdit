using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SongBeamerEdit.BatchProcessingModel
{
    public class Songbook
    {
        #region Konstruktoren
        public Songbook(string songbook)
        {
            var dict = new Dictionary<string, string> { { "FJ", "Feiert Jesus" }, { "DbH", "Du bist Herr" }, { "LuH", "Lehre uns Herr " }};
            //Aus Kurzformangaben Langform erstellen
            foreach (var titel in dict)
            {
                string regex = "(" + titel.Key + @")([0-9]*)\s*(.*?)$";
                var rgx1 = new Regex(regex, RegexOptions.Multiline);
                if (rgx1.IsMatch(songbook))
                {
                    Match match = rgx1.Match(songbook);
                    STitel = titel.Value; //+ " " + match.Groups[2].Value;
                    SShortTitel = match.Groups[1].Value;
                    SBookNr = match.Groups[2].Value.TrimStart();
                    SSongNr = match.Groups[3].Value;
                }
            }
            if (string.IsNullOrEmpty(STitel))
            {
                //Auswertung Langform mit römischen Zahlen bei Songbuchnummer
                var rgx2 = new Regex(@"([A-ZÄÖÜa-zßäüö ]*)([^IVX][IVX][IVX]*)\s*/\s*([0-9]*)", RegexOptions.Multiline);
                if (rgx2.IsMatch(songbook))
                {
                    Match match = rgx2.Match(songbook);
                    STitel = match.Groups[1].Value;
                    if (dict.ContainsValue(match.Groups[1].Value))
                    {
                        SShortTitel = dict.Single(kvp => kvp.Value == match.Groups[1].Value).Key;
                    }
                    SBookNr = RomanToInt(match.Groups[2].Value.TrimStart());
                    SSongNr = match.Groups[3].Value;
                }
                else
                {
                    //Auswertung Langform mit Dezimalzahlen bei Songbuchnummer
                    var rgx3 = new Regex(@"^((?:(?:[A-ZÄÖÜa-zäöüß]+)\s*)*)([0-9]*)\s*/\s*([0-9]+)", RegexOptions.Multiline);
                    if (rgx3.IsMatch(songbook))
                    {
                        Match match = rgx3.Match(songbook);
                        STitel = match.Groups[1].Value;
                        if (dict.ContainsValue(match.Groups[1].Value))
                        {
                            SShortTitel = dict.Single(kvp => kvp.Value == match.Groups[1].Value).Key;
                        }
                        int i;
                        if (int.TryParse(match.Groups[2].Value, out i))
                        {
                            SBookNr = match.Groups[2].Value.Trim();
                        }
                        SSongNr = match.Groups[3].Value;
                    }
                    else
                    {
                        //Auswertung nur Songbook Titel und evtl. Songbook Nummer
                        var rgx4 = new Regex(@"^((?:(?:[A-ZÄÖÜa-zäöüß]+)\s*)*)([0-9]*)", RegexOptions.Multiline);
                        if (rgx4.IsMatch(songbook))
                        {
                            Match match = rgx4.Match(songbook);
                            STitel = match.Groups[1].Value;
                            int i;
                            if (int.TryParse(match.Groups[2].Value, out i))
                            {
                                SSongNr = match.Groups[2].Value.Trim();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Methoden
        string RomanToInt(string roman)
        {
            var dict = new Dictionary<char, int> {{'I', 1}, {'V', 5}, { 'X',10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 }};
            Stack<char> st = new Stack<char>();
            foreach (char ch in roman.ToCharArray()) st.Push(ch);
            int result = 0;
            while (st.Count > 0)
            {
                var c1 = st.Pop();              //Entfernt das oberste Objekt aus Stack und gibt es zurück an c1
                var ch1 = dict[c1];             //Übergibt Value der dem Key zugeornedt ist

                if (st.Count > 0)               //Schleife über Stackeinträge bis noch einer vorhanden ist
                {
                    var c2 = st.Peek();         //Gibt das oberste Objekt von Stack zurück, ohne es zu entfernen.
                    var ch2 = dict[c2];         //Übergibt Value der dem Key zugeornedt ist
                    if (ch2 < ch1)              //Wenn der Value vom zweiten Key kleiner ist als vom ersten ...
                    {
                        result += (ch1 - ch2);  //dann ist das Ergebnis der erste Value minus dem zweiten Value
                        st.Pop();               //Entfernt das oberste Objekt aus Stack und gibt es nicht zurück
                    }
                    else                       //Wenn der Value vom zweiten Key größer ist als vom ersten ...
                    {
                        result += ch1;         //dann ist das Ergebnis der erste Value plus dem zweiten Value 
                    }
                }
                else
                {
                    result += ch1;            //Der letzte Stackeintrag wird zum Ergebnis addiert
                }
            }
            return result.ToString();
        }
        #endregion
        #region Eigenschaften
        public string STitel        { get; set; }
        public string SShortTitel   { get; set; }
        public string SBookNr       { get; set; }
        public string SSongNr       { get; set; }
        #endregion
    }
}
