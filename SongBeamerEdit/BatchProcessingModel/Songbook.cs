using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SongBeamerEdit.BatchProcessingModel
{
    public class Songbook
    {
        #region Konstruktoren
        public Songbook(string songbook)
        {
            var rgx1 = new Regex(@"(FJ)\s*([0-9]*)\s*(.*?)", RegexOptions.Multiline);
            if (rgx1.IsMatch(songbook))
            {
                Match match = rgx1.Match(songbook);
                STitel = "Feiert Jesus " + match.Groups[2].Value;
                SShortTitel = "FJ" + match.Groups[2].Value + " " + match.Groups[3].Value;
                SBookNr = match.Groups[2].Value;
                SSongNr = match.Groups[3].Value;
            }

            var rgx2 = new Regex(@"([A-ZÄÖÜa-zßäüö ]*)([^IVX][IVX][IVX]*)\s*/\s*([0-9]*)", RegexOptions.Multiline);
            if (rgx2.IsMatch(songbook))
            {
                Match match = rgx2.Match(songbook);
                STitel = match.Groups[1].Value;
                SBookNr = match.Groups[2].Value;
                SSongNr = match.Groups[3].Value;
            }

            var rgx3 = new Regex(@"(Feiert Jesus)\s*([IVXLCDM]*)\s*(.*?)", RegexOptions.Multiline);
            if (rgx3.IsMatch(songbook))
            {
                MatchEvaluator matchEvaluator = new MatchEvaluator(RomanToInt);
                rgx3.Replace(songbook, matchEvaluator);
            }


        }
        #endregion
        #region Methoden
        string RomanToInt(Match match)
        {
            var dict = new Dictionary<char, int> {{'I', 1}, {'V', 5}, { 'X',10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 }};
            Stack<char> st = new Stack<char>();
            foreach (char ch in match.Groups[2].ToString().ToCharArray()) st.Push(ch);
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
            STitel = match.Groups[1].Value + " " + match.Groups[2].Value;
            SShortTitel = "FJ" + result.ToString() + " " + match.Groups[3].Value;
            SBookNr = match.Groups[2].Value;
            SSongNr = match.Groups[3].Value;
            return string.Empty;
        }
        #endregion
        #region Eigenschaften
        public string STitel { get; set; }
        public string SShortTitel { get; set; }
        public string SBookNr { get; set; }
        public string SSongNr { get; set; }
        #endregion
    }
}
