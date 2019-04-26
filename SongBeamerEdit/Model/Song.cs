using System;
using System.Linq;
using System.Text.RegularExpressions;
using SongBeamerEdit.FlagsValueConverter;

namespace SongBeamerEdit.Model
{
    /// <summary>
    /// Klasse zur Verarbeitung von Text im Songbeamerformat
    /// </summary>
    public class Song
    {
        #region Konstruktoren
        public Song() { }
        public Song(string _mySongText)
        {
            SongAnalyse(_mySongText);                               //Führt Erkennung und Bereinigung mit regulären Ausdrücken aus
            ArrangeLangVerseList(BitMaskSelectedLang);              //Erstellt eine Versliste unter Berücksichtigung der vorhandenen Sprachen
            int displayLinesCount = Math.Max(Properties.Settings.Default.MaxDisplaySonglines, AvailableLangCount);  //Ermittelt Anzahl anzuzeigender Zeilen -> größter Wert aus default und Anzahl Sprachen
            ArrangeMaxLineAndLangVersList(displayLinesCount);       //Erstellt eine Versliste unter Berücksichtigung aller vorhandenen Sprachen und der Anzahl anzuzeigender Zeilen
            Text = Vorspann + SelectedVerseListMaxLines.ToString(); //Setzt die Texteingenschaft
        }
        #endregion
        /// <summary>
        /// Erstellt den Songtext wenn an der Sprachauswahl etwas geändert wurde
        /// </summary>
        /// <param name="languageBitMask"></param>
        public string GetSongText(Language languageBitMask)
        {
            ArrangeLangVerseList(languageBitMask);
            int lines = Math.Max(Properties.Settings.Default.MaxDisplaySonglines, SelectedLangCount);
            ArrangeMaxLineAndLangVersList(lines);
            Text = Vorspann + SelectedVerseListMaxLines.ToString();
            return Text;
        }

        /// <summary>
        /// Erstellt den Songtext wenn an der Auswahl der darzustellenden Zeilenanzahl etwas geändert wurde
        /// </summary>
        /// <param name="maxDisplaylines"></param>
        public string GetSongText(int maxDisplaylines)
        {
            ArrangeMaxLineAndLangVersList(maxDisplaylines);
            Text = Vorspann + SelectedVerseListMaxLines.ToString();
            return Text;
        }

        /// <summary>
        /// Initiale Texterkennung
        /// </summary>
        /// <param name="_myText"></param>
        private string SongAnalyse(string _myText)
        {
            Vorspann = Regex.Match(_myText, @"(#.*?\r\n)+", RegexOptions.Singleline).ToString();                        //Ermittelt den Vorspann
            AvailableLangCount = int.TryParse(Regex.Match(Vorspann, "(?<=LangCount=)[0-9]+").Value, out int J) ? J : 1;
            AvailableLangCount = (AvailableLangCount > 4) ? 4 : AvailableLangCount;
            SelectedLangCount = AvailableLangCount;
            BitMaskSelectedLang = (Language)(int)Math.Pow(2.0, AvailableLangCount) - 1;
            _myText = _myText.Substring(Vorspann.Length);                                                               //Nimmt den Vorspann aus der Arbeitsvariable
            _myText = Regex.Replace(_myText, @"(\r\n){2,}", "\r\n", RegexOptions.Multiline);                            //Nimmt mehrfache Zeilenumbrüche heraus
            _myText = Regex.Replace(_myText, @"[ ]{2,}", " ", RegexOptions.Singleline);                                 //Nimmt mehrfache Spaces heraus
            _myText = Regex.Replace(_myText, @"\r\n($)", "$1", RegexOptions.Singleline);                                //Nimmt am Ende vom Lied Zeilenumbrüche heraus
            _myText = Regex.Replace(_myText, @"(-+\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$2$3");    //Wenn vor "Vers", "Bridge", "Refrain"... nur "-" oder "--" steht, wird "---" eingesetzt
            _myText = Regex.Replace(_myText, @"(?<!---\r\n)(Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)([ 0-9]*\r\n)", "---\r\n$&");  //Wenn vor "Vers" oder "Bridge" oder "Refrain" "---" fehlt, werden diese eingesetzt
            _myText = Regex.Replace(_myText, @"\r\n--\r\n", "\r\n", RegexOptions.Multiline);                            //Nimmt vorhandene Seitenrennungen "--" heraus
            _myText = Regex.Replace(_myText, @"(-{3})\r\n(((Vers|Bridge|Pre-Refrain|Refrain|Pre-Chorus|Chorus|Coda)[ 0-9]*)?\r\n)?(.*?\r\n)", "$1 $3$$$ $5", RegexOptions.Multiline);     //Zieht die Kennzeichnungen in die erste Verszeile
            InitalVerseList = new VerseCollection(_myText, AvailableLangCount);
            return _myText;
        }

        #region Methoden
        /// <summary>
        /// Erstellt eine Versliste unter Vorgabe der gewünschten Sprache(n)
        /// </summary>
        /// <param name="lang"></param>
        public void ArrangeLangVerseList(Language lang)
        {
            SelectedLangCount = FlagCount(lang);
            BitMaskSelectedLang = lang;
            SelectedVerseList.Verses = (from verse in InitalVerseList.Verses
                                        select new LineCollection()
                                        {
                                            CallSign = verse.CallSign,
                                            Lines = verse.Lines.Where(line => (lang & line.BitwiseLanguageNr) == line.BitwiseLanguageNr).ToList()
                                        }).ToList();
        }

        /// <summary>
        /// Erstellt eine Versliste unter Vorgabe der max. anzuzeigenden Verzeilenanzahl.
        /// Grundlage ist die Liste der gewählten Sprache(n)
        /// </summary>
        /// <param name="maxLines"></param>
        public void ArrangeMaxLineAndLangVersList(int maxLines)
        {
            SelectedVerseListMaxLines.Verses.Clear();
            SelectedVerseListMaxLines.MaxVerseLinesCount = 0;
            foreach (var verLineCollection in SelectedVerseList.Verses)
            {
                var maxLineCollection = new LineCollection();
                int count = 0;
                if (verLineCollection.Lines.Count > SelectedVerseListMaxLines.MaxVerseLinesCount) SelectedVerseListMaxLines.MaxVerseLinesCount = verLineCollection.Lines.Count;

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
        /// <summary>
        /// Ermittelt aus Enum Bitmuster die Anzahl der gesetzten Bits
        /// </summary>
        /// <param name="languageMask"></param>
        /// <returns>Anzahl gesetzter Bits</returns>
        public int FlagCount(Language languageMask)
        {
            var hasFlagAttribute = languageMask.GetType().GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;
            if (!hasFlagAttribute) return 1;
            var count = 0;
            var value = Convert.ToInt16(languageMask);
            while (value != 0)
            {
                if ((value & 1) == 1) count++;
                value >>= 1;
            }
            return count;
        }
        #endregion

        #region Eigenschaften
        /// <summary>
        /// Bitmaske der ausgewählten Sprachen
        /// </summary>
        public Language BitMaskSelectedLang { get; set; }
        /// <summary>
        /// Anzahl der ausgewählten Sprachen
        /// </summary>
        public int SelectedLangCount { get; set; }
        /// <summary>
        /// Anzahl der verfügbaren Sprachen
        /// </summary>
        public int AvailableLangCount { get; private set; }
        /// <summary>
        /// Konfigurationszeilen vom Songbeamer-Datei
        /// </summary>
        public string Vorspann { get; set; }
        /// <summary>
        /// Liste aller Verse aus dem Originaltext
        /// </summary>
        public VerseCollection InitalVerseList { get; set; }
        /// <summary>
        /// Liste von Versen unter Berücksichtigung der gewählten Sprachen
        /// </summary>
        public VerseCollection SelectedVerseList { get; set; } = new VerseCollection();
        /// <summary>
        /// Liste von Versen unter Berücksichtigung der gewählten Sprachen und der gewählten Anzahl darzustellender Zeilen
        /// </summary>
        public VerseCollection SelectedVerseListMaxLines { get; set; } = new VerseCollection();
        /// <summary>
        /// Der für die View anzuzeigende Songtext
        /// </summary>
        public string Text { get; set; }
        #endregion
    }
}
