using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongBeamerEdit.BatchProcessingModel;
using System.Diagnostics;
using System.IO;

namespace SongBeamerEditTests.GetDoubles
{
    [TestClass]
    public class MetadataDoublesTests
    {
        [TestMethod]
        public void GetMethaDataInfosTest()
        {
            //string path = @"C:\Users\rolf\Documents\SongBeamer";
            string path = @"Z:\NORA\SongBeamer";
            var sngFiles = Directory.EnumerateFiles(path, "*.sng");
            foreach (var song in sngFiles)
            {
                var MethaSong = new SongMethadata(song);
                Debug.WriteLine(song);
                Debug.WriteLineIf(!string.IsNullOrEmpty(MethaSong.MTitle),      "   #Title          : " + MethaSong.MTitle);
                Debug.WriteLineIf(!string.IsNullOrEmpty(MethaSong.MOTitle),     "   #OTitel         : " + MethaSong.MOTitle);
                Debug.WriteLineIf(!string.IsNullOrEmpty(MethaSong.MTitleLang2), "   #TitelLang2     : " + MethaSong.MTitleLang2);
                Debug.WriteLineIf(!string.IsNullOrEmpty(MethaSong.MTitleLang3), "   #TitelLang3     : " + MethaSong.MTitleLang3);
                Debug.WriteLineIf(!string.IsNullOrEmpty(MethaSong.MTitleLang4), "   #TitelLang4     : " + MethaSong.MTitleLang4);
                Debug.WriteLineIf(!string.IsNullOrEmpty(MethaSong.MSongbook),   "   #Songbook       : " + MethaSong.MSongbook);
            }
        }
        [TestMethod]
        public void SongbookAnalyseTest()
        {
            //Arrange
            string[] songbookdumies = { "FJ1 88", "Feiert Jesus I / 88", "DbH5 198", "Du bist Herr 5 / 198", "LuH 6", "Lehre uns Herr / 6", "Song 198", "Ich will dir danken /074", "Freude", "Songs Lothar Kosse / 001", "In Love with Jesus I / 002", "Wiedenester / 002", "Du bist Herr Kids I / 003", "RR Liederbuch" };
            string erg = string.Empty;
            //Act
            foreach (string songbookdummy in songbookdumies)
            {
                Songbook test = new Songbook(songbookdummy);
                Debug.WriteLineIf(!string.IsNullOrEmpty(test.SbTitle),      "#STitle          : " + test.SbTitle);
                Debug.WriteLineIf(!string.IsNullOrEmpty(test.SbShortTitel), "#SShortTitle     : " + test.SbShortTitel);
                Debug.WriteLineIf(!string.IsNullOrEmpty(test.SbBookNr),     "#SNR             : " + test.SbBookNr);
                Debug.WriteLineIf(!string.IsNullOrEmpty(test.SbSongNr),     "#SSongNr         : " + test.SbSongNr);
                Debug.WriteLineIf(!string.IsNullOrEmpty(test.SbTitle), string.Empty);
                erg += test.SbTitle + test.SbShortTitel + test.SbBookNr + test.SbSongNr;
            }
            //Assert
            Assert.IsTrue(erg == "Feiert JesusFJ188Feiert JesusFJ188Du bist HerrDbH5198Du bist Herr 5198Lehre uns Herr LuH6Lehre uns Herr LuH6Song198Ich will dir danken 074FreudeSongs Lothar Kosse 001In Love with Jesus1002Wiedenester 002Du bist Herr Kids1003RR Liederbuch");
        }
        [TestMethod]
        public void RomanToIntTest()
        {
            //Arrange
            string[] songtitles = { "FJ1 88", "DbH5 198", "LuH 6", "SONG 198", "Feiert Jesus XIX / 88", "Du bist Herr 5 / 198", "Lehre uns Herr / 6", "Song / 198", "Ich will dir danken / 074", "Freude", "Songs Lothar Kosse / 001", "Ich will dir danken / 001", "In Love with Jesus I / 002", "Wiedenester / 002", "Du bist Herr Kids I / 003", "RR Liederbuch / 004" };
            string[] titlesShort ={ "FJ1 88", "DbH5 198", "LuH 6", "SONG 198", "", "", "", "", "", "", "", "" };
            string erg = string.Empty;

            //string[] romeToInt = new string[songtitles.Length];
            //Act
            foreach (var title in songtitles)
            {
                Songbook test = new Songbook(title);
                Debug.WriteLine(string.Format("#Songbook            : {0}", title));
                Debug.WriteLine(string.Format("#Songbook Titel      : {0}", test.SbTitle));
                Debug.WriteLine(string.Format("#Songbook Kurzform   : {0}", test.SbShortTitel));
                Debug.WriteLine(string.Format("#Songbook Nummer     : {0}", test.SbBookNr));
                Debug.WriteLine(string.Format("#Songbook Songnummer : {0}\n", test.SbSongNr));  
                erg += test.SbTitle + test.SbShortTitel + test.SbBookNr + test.SbSongNr;
            }
            //Assert
            Assert.IsTrue(erg == "Feiert JesusFJ188Du bist HerrDbH5198Lehre uns Herr LuH6SONG198Feiert JesusFJ1988Du bist Herr 5198Lehre uns Herr LuH6Song 198Ich will dir danken 074FreudeSongs Lothar Kosse 001Ich will dir danken 001In Love with Jesus1002Wiedenester 002Du bist Herr Kids1003RR Liederbuch 004");
        }
    }
}
