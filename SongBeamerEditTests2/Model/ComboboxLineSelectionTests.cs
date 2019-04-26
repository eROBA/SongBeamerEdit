using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongBeamerEdit.ViewModel;
using System.Diagnostics;
using SongBeamerEdit.Properties;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace SongBeamerEditTests.Model
{
    [TestClass()]
    public class CobmoboxLineSelectionTests
    {
        [TestMethod()]
        //Variante 1 -> es gibt mehr Sprachen als maximal erlaubte Zeilenzahlen
        public void PageLinesTest1()
        {
            //Arrange
            SongViewModel TestSVM = new SongViewModel();
            int VorgabeMaxZeilenanzahl = 2;
            int AnzahlGewaelterSprachen = 3;
            int MaxAnzahlZeilenImVers = 8;
            //Act
            var Test = TestSVM.PageLines(VorgabeMaxZeilenanzahl, AnzahlGewaelterSprachen, MaxAnzahlZeilenImVers);
            foreach (var item in Test) Debug.Print(item.ToString());
            //Assert
            Assert.IsTrue(Test.Count == 3 & Test[0] == 3 & Test[1] == 6 & Test[2] == 8);
        }
        [TestMethod()]
        //Variante 2 -> es gibt gleich viele Sprachen wie maximal erlaubte Zeilenzahlen
        public void PageLinesTest2()
        {
            //Arrange
            SongViewModel TestSVM = new SongViewModel();
            int VorgabeMaxZeilenanzahl = 2;
            int AnzahlGewaelterSprachen = 2;
            int MaxAnzahlZeilenImVers = 8;
            //Act
            var Test = TestSVM.PageLines(VorgabeMaxZeilenanzahl, AnzahlGewaelterSprachen, MaxAnzahlZeilenImVers);
            foreach (var item in Test) Debug.Print(item.ToString());
            //Assert
            Assert.IsTrue(Test.Count == 4 & Test[0] == 2 & Test[1] == 4 & Test[2] == 6 & Test[3] == 8);
        }
        [TestMethod()]
        //Variante 3 -> es gibt weniger Sprachen wie maximal erlaubte Zeilenzahlen
        public void PageLinesTest3()
        {
            //Arrange
            SongViewModel TestSVM = new SongViewModel();
            int VorgabeMaxZeilenanzahl = 4;
            int AnzahlGewaelterSprachen = 3;
            int MaxAnzahlZeilenImVers = 10;
            //Act
            var Test = TestSVM.PageLines(VorgabeMaxZeilenanzahl, AnzahlGewaelterSprachen, MaxAnzahlZeilenImVers);
            foreach (var item in Test) Debug.Print(item.ToString());
            //Assert
            Assert.IsTrue(Test.Count == 4 & Test[0] == 3 & Test[1] == 6 & Test[2] == 9 & Test[3] == 10);
        }
        [TestMethod()]
        //Variante 4 -> es gibt weniger Sprachen wie maximal erlaubte Zeilenzahlen
        public void PageLinesTest4()
        {
            //Arrange
            SongViewModel TestSVM = new SongViewModel();
            int VorgabeMaxZeilenanzahl = 2;
            int AnzahlGewaelterSprachen = 1;
            int MaxAnzahlZeilenImVers = 5;
            //Act
            var Test = TestSVM.PageLines(VorgabeMaxZeilenanzahl, AnzahlGewaelterSprachen, MaxAnzahlZeilenImVers);
            foreach (var item in Test) Debug.Print(item.ToString());
            //Assert
            Assert.IsTrue(Test.Count == 5 & Test[0] == 1 & Test[1] == 2 & Test[2] == 3 & Test[3] == 4 & Test[4] == 5);
        }
        [TestMethod()]
        //Variante 5 -> es gibt weniger Sprachen wie maximal erlaubte Zeilenzahlen
        public void PageLinesTest5()
        {
            //Arrange
            SongViewModel TestSVM = new SongViewModel();
            int VorgabeMaxZeilenanzahl = 2;
            int AnzahlGewaelterSprachen = 3;
            int MaxAnzahlZeilenImVers = 6;
            //Act
            var Test = TestSVM.PageLines(VorgabeMaxZeilenanzahl, AnzahlGewaelterSprachen, MaxAnzahlZeilenImVers);
            foreach (var item in Test) Debug.Print(item.ToString());
            //Assert
            Assert.IsTrue(Test.Count == 2 & Test[0] == 3 & Test[1] == 6);
        }
        //[TestMethod()]
        //Ermittlung vorkommender Methadaten aus allen vorh. Songs
        public void GetSongMethaData()
        {
            List<string> methas = new List<string> { };
            string path = @"C:\Users\rolf\Documents\SongBeamer";
            var sngFiles = Directory.EnumerateFiles(path, "*.sng");
            foreach (var song in sngFiles)
            {
                string songText= File.ReadAllText(song, Encoding.Default);
                string vorspann = Regex.Match(songText, @"(#.*?\r\n)+", RegexOptions.Singleline).Value;
                MatchCollection matches = Regex.Matches(vorspann, @"^#(.*?)=", RegexOptions.Multiline);
                foreach (Match match in matches)
                {
                    if (methas.IndexOf(match.Groups[1].Value) < 0) methas.Add(match.Groups[1].Value);
                }
            }
            methas.Sort();
            foreach (var item in methas)
            {
                Debug.WriteLine("public string " + item + " { get; set; }");
            }
            //#Chords
            //#TitleFormat
            //(c)
            //AddCopyrightInfo
            //Author
            //BackgroundImage
            //BGProfile
            //Bible
            //Categories
            //CCLI
            //Chords
            //ChurchSongID
            //Comment
            //Comments
            //Editor
            //Font
            //FontLang2
            //FontSize
            //Format
            //Format_PrePage
            //Key
            //Keywords
            //Lang
            //LangCount
            //LineFormat
            //Melody
            //NatCopyright
            //OTitle
            //OutlineColor
            //OutlinedFont
            //QuickFind
            //Rights
            //Songbook
            //Speed
            //TextAlign
            //Title
            //TitleAlign
            //TitleFontSize
            //TitleFormat
            //TitleLang2
            //TitleLang3
            //TitleLang4
            //Translation
            //Transpose
            //TransposeAccidental
            //VerseOrder
            //Version
        }
        //[TestMethod]
        //Ermittlung vorkommender Methadaten aus allen vorh. Songs
        public void GetSongMethaLines()
        {
            string path = @"C:\Users\rolf\Documents\SongBeamer";
            var sngFiles = Directory.EnumerateFiles(path, "*.sng");
            foreach (var song in sngFiles)
            {
                string songText = File.ReadAllText(song, Encoding.Default);
                string vorspann = Regex.Match(songText, @"(#.*?\r\n)+", RegexOptions.Singleline).Value;
                Debug.Write(System.IO.Path.GetFileName(song) + "\n" + vorspann + "\n");
            }
        }

    }
}