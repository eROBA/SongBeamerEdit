using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SongBeamerEdit.BatchProcessingModel
{
    public class Doubles
    {
        #region Felder
        private static List<SongFilename> songFilenameCollection = new List<SongFilename>();
        private static List<SongFilenameDoubles> songFilenameDoublesCollection = new List<SongFilenameDoubles>();
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Auslesen aller mit in "path" befindlichen Songbeamerdateinamen und Ermitteln der daraus erkennbaren Informationen
        /// mit Übertragung in eine Liste
        /// </summary>
        /// <param name="path"></param>
        public Doubles(string path)
        {
            songFilenameCollection.AddRange(from string song in Directory.EnumerateFiles(path, "*.sng")  select new SongFilename(song));
            foreach (var song in songFilenameCollection)
            {
                var doub = new SongFilenameDoubles { SongReference = song };
                doub.SongDouble.AddRange(songFilenameCollection.Where(songList2 => song.Equals(songList2)).Select(songList2 => songList2));
                if (doub.SongDouble.Count > 0) songFilenameDoublesCollection.Add(doub);
            }
        }
        #endregion

        #region Eigenschaften
        /// <summary>
        /// Liste mit Infos zu Songbeamerfiles
        /// </summary>
        public List<SongFilename> SongFilenameCollection
        {
            get { return songFilenameCollection; }
            set { songFilenameCollection = value; }
        }
        
        /// <summary>
        /// Liste mit Gruppierung von Songs die mehrfach vorkommen
        /// </summary>
        public List<SongFilenameDoubles> SongFilenameDoublesCollection
        {
            get { return songFilenameDoublesCollection; }
            set { songFilenameDoublesCollection = value; }
        }
        #endregion
    }
}
