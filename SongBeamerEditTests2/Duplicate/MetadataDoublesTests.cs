using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SongBeamerEdit.BatchProcessingModel;
using System.Diagnostics;

namespace SongBeamerEditTests.GetDoubles
{
    [TestClass]
    public class MetadataDoublesTests
    {
        [TestMethod]
        public void GetMethaDataInfosTest()
        {
            string path = @"C:\Users\rolf\Documents\SongBeamer";
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
            foreach (string songbookdummy in songbookdumies)
            {
                Songbook test = new Songbook(songbookdummy);
                Debug.WriteLineIf(!string.IsNullOrEmpty(test.STitel),       "   #STitle          : " + test.STitel);
                Debug.WriteLineIf(!string.IsNullOrEmpty(test.SShortTitel),  "   #SShortTitle     : " + test.SShortTitel);
                Debug.WriteLineIf(!string.IsNullOrEmpty(test.SBookNr),      "   #SNR             : " + test.SBookNr);
                Debug.WriteLineIf(!string.IsNullOrEmpty(test.SSongNr),      "   #SSongNr         : " + test.SSongNr);
            }
            //Act


            //Assert
        }
        [TestMethod]
        public void RomanToIntTest()
        {
            //Arrange
            Songbook test = new Songbook("Feiert Jesus XIX / 88");
            //Act
            Debug.WriteLine(string.Format("Römische Zahl {0} ist dezimal {1}",test.SBookNr ,test.SBookNr ));
            //Assert
        }

    }
}
