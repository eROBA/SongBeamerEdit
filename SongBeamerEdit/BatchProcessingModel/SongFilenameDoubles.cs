using System.Collections.Generic;

namespace SongBeamerEdit.BatchProcessingModel
{
    /// <summary>
    /// Gruppierung von vermutlich indentischen Songs
    /// </summary>
    public class SongFilenameDoubles
    {
        /// <summary>
        /// Song als Vergleichsbasis
        /// </summary>
        public SongFilename SongReference { get; set; }
        /// <summary>
        /// Liste vermutlich identischer Songs von SongReference
        /// </summary>
        public List<SongFilename> SongDouble { get; set; } = new List<SongFilename>();
    }
}
