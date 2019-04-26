using System.Collections.Generic;
using System.IO;

namespace SongBeamerEdit.BatchProcessingModel
{
    public class Doubles
    {
        #region Konstruktoren
        /// <summary>
        /// Auslesen aller mit in "path" befindlichen Songbeamerdateien und Ermitteln der daraus erkennbaren Informationen
        /// mit Übertragung in eine Liste
        /// </summary>
        /// <param name="path"></param>
        public Doubles(string path)
        {
            var sngFiles = Directory.EnumerateFiles(path, "*.sng");
            foreach (var song in sngFiles) SongFilenameCollection.Add(new SongFilename(song));
            foreach (var songList1 in SongFilenameCollection)
            {
                SongFilenameDoubles test = new SongFilenameDoubles {SongReference = songList1};
                foreach (SongFilename songList2 in SongFilenameCollection)
                {
                    bool a, b, c, d, e;
                    a = songList1.ID    != songList2.ID;
                    b = songList1.Title == songList2.Title;
                    c = songList1.Title == songList2.TitleAlternativ;
                    d = songList1.Title == songList2.TitleSecondLang;
                    e = songList1.Title == songList2.TitelAlternativSecondLang;
                    if (a && (b | c | d | e)) test.SongDouble.Add(songList2);
                }
                if (test.SongDouble.Count > 0) SongFilenameDoublesCollection.Add(test);
            }
        }
        #endregion
               
        #region Eigenschaften
        /// <summary>
        /// Liste mit Infos zu Songbeamerfiles
        /// </summary>
        public List<SongFilename> SongFilenameCollection { get; set; } = new List<SongFilename>();
        
        /// <summary>
        /// Liste mit Gruppierung von Songs die mehrfach vorkommen
        /// </summary>
        public List<SongFilenameDoubles> SongFilenameDoublesCollection { get; set; } = new List<SongFilenameDoubles>();
        #endregion
    }
}
