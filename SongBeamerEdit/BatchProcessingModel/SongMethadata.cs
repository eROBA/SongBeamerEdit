using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SongBeamerEdit.BatchProcessingModel
{
    /// <summary>
    /// Analysiert die Methadaten im Songbeamerfile
    /// </summary>
    /// <param name="fullPath"></param>
    public class SongMethadata
    {
        public SongMethadata(string fullPath)
        {
            string songText = File.ReadAllText(fullPath, Encoding.Default);
            Match   
            match       = Regex.Match(songText, @"#Title=(.*?)\r\n");
            MTitle      = match.Groups[1].Value;
            match       = Regex.Match(songText, @"#OTitle=(.*?)\r\n");
            MOTitle     = match.Groups[1].Value;
            match       = Regex.Match(songText, @"#TitleLang2=(.*?)\r\n");
            MTitleLang2 = match.Groups[1].Value;
            match       = Regex.Match(songText, @"#TitleLang3=(.*?)\r\n");
            MTitleLang3 = match.Groups[1].Value;
            match       = Regex.Match(songText, @"#TitleLang4=(.*?)\r\n");
            MTitleLang4 = match.Groups[1].Value;
            match       = Regex.Match(songText, @"#Songbook=(.*?)\r\n");
            MSongbook   = match.Groups[1].Value;
        }
        #region Eigenschaften
        /// <summary>
        /// #Titel aus den Methadaten
        /// </summary>
        public string MTitle { get; set; }
        /// <summary>
        /// #OTitel aus den Methadaten
        /// </summary>
        public string MOTitle { get; set; }
        /// <summary>
        /// #TitelLang2 aus den Methadaten
        /// </summary>
        public string MTitleLang2 { get; set; }
        /// <summary>
        /// #TitelLang3 aus den Methadaten
        /// </summary>
        public string MTitleLang3 { get; set; }
        /// <summary>
        /// #TitelLang4 aus den Methadaten
        /// </summary>
        public string MTitleLang4 { get; set; }
        /// <summary>
        /// #Songbook aus den Methadaten
        /// </summary>
        public string MSongbook { get; set; }
        #endregion
    }
}
