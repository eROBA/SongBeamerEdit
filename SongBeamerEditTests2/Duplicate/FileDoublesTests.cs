using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using SongBeamerEdit.BatchProcessingModel;
using System;

namespace SongBeamerEditTests.GetDoubles
{
    [TestClass]
    public class FileDoublesTests
    {
        [TestMethod]
        public void GetFilenameInfosTest()
        {
            string path = @"C:\Users\rolf\Documents\SongBeamer";
            var sngFiles = Directory.EnumerateFiles(path, "*.sng");
            foreach (var song in sngFiles)
            {
                SongFilename SongName = new SongFilename(song);
                Debug.WriteLine(SongName.OrigFilename);
                Debug.WriteLine                                                            ("   Title             : " + SongName.Title);
                Debug.WriteLineIf(!string.IsNullOrEmpty(SongName.TitleAlternativ),          "   Titel alternativ  : " + SongName.TitleAlternativ);
                Debug.WriteLineIf(!string.IsNullOrEmpty(SongName.TitleSecondLang),          "   Sprache alternativ: " + SongName.TitleSecondLang);
                Debug.WriteLineIf(!string.IsNullOrEmpty(SongName.TitelAlternativSecondLang),"   Sprache alternativ: " + SongName.TitelAlternativSecondLang);
                Debug.WriteLineIf(!string.IsNullOrEmpty(SongName.Songbook),                 "   Source            : " + SongName.Songbook);
                Debug.WriteLine("");
            }
        }
        [TestMethod]
        public void GetFilenameDoublesTest()
        {
            string path = @"C:\Users\rolf\Documents\SongBeamer";
            var test = new Doubles(path);
            foreach (var d in test.SongFilenameDoublesCollection)
            {
                Debug.WriteLine(string.Format("---Referenzfile-ID {0}----------------------",d.SongReference.ID));
                Debug.WriteLine(d.SongReference.OrigFilename);
                Debug.WriteLineIf(!string.IsNullOrEmpty(d.SongReference.Title),                     "   Titel               : " + d.SongReference.Title);
                Debug.WriteLineIf(!string.IsNullOrEmpty(d.SongReference.TitleAlternativ),           "   Titel alternativ    : " + d.SongReference.TitleAlternativ);
                Debug.WriteLineIf(!string.IsNullOrEmpty(d.SongReference.TitleSecondLang),           "   Sprache 2           : " + d.SongReference.TitleSecondLang);
                Debug.WriteLineIf(!string.IsNullOrEmpty(d.SongReference.TitelAlternativSecondLang), "   Sprache 2 alternativ: " + d.SongReference.TitelAlternativSecondLang);
                Debug.WriteLineIf(!string.IsNullOrEmpty(d.SongReference.Songbook),                  "   Source              : " + d.SongReference.Songbook);
                foreach (var dd in d.SongDouble)
                {
                    Debug.WriteLine(string.Format("---Duplikat-ID {0}--------------------------", dd.ID.ToString()));
                    Debug.WriteLine(dd.OrigFilename);
                    Debug.WriteLineIf(!string.IsNullOrEmpty(dd.Title),                      "   Titel               : " + dd.Title);
                    Debug.WriteLineIf(!string.IsNullOrEmpty(dd.TitleAlternativ),            "   Titel alternativ    : " + dd.TitleAlternativ);
                    Debug.WriteLineIf(!string.IsNullOrEmpty(dd.TitleSecondLang),            "   Sprache 2           : " + dd.TitleSecondLang);
                    Debug.WriteLineIf(!string.IsNullOrEmpty(dd.TitelAlternativSecondLang),  "   Sprache 2 alternativ: " + dd.TitelAlternativSecondLang);
                    Debug.WriteLineIf(!string.IsNullOrEmpty(dd.Songbook),                   "   Source              : " + dd.Songbook);
                }
                Debug.WriteLine("");
            }
        }

    }

}
