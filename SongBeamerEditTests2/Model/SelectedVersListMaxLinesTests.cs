using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongBeamerEdit.FlagsValueConverter;
using SongBeamerEditTests.Model;
using System.Diagnostics;

namespace SongBeamerEdit.Model
{
    [TestClass()]
    public class SelectedVersListMaxLines
    {
        [TestMethod()]
        public void GenerateMaxLineVersList()
        {
            //Arrange
            TestsongDummyCreator TestsongDummy = new TestsongDummyCreator();
            int maxZeilen = 2;
            string TestsongPattern = TestsongDummy.GenerateTestSong(3, 6, 1, maxZeilen);    //Verse|Verszeilen|Sprachen|Max Zeilenzahl
            Song Testsong = new Song(TestsongPattern);
            Testsong.ArrangeLangVerseList(Language.Lang0);
            Testsong.ArrangeMaxLineAndLangVersList(maxZeilen);
            string test = Testsong.SelectedVerseListMaxLines.ToString();
            Debug.Print(Testsong.Vorspann + test);
            //Assert
            Assert.IsTrue(Testsong.Vorspann + test == TestsongPattern);
        }
        [TestMethod()]
        public void GenerateMaxLineVersList2()
        {
            //Arrange
            TestsongDummyCreator TestsongDummy = new TestsongDummyCreator();
            int maxZeilen = 2;
            string TestsongPattern = TestsongDummy.GenerateTestSong(2, 5, 1, maxZeilen);    //Verse|Verszeilen|Sprachen|Max Zeilenzahl
            Song Testsong = new Song(TestsongPattern);
            Testsong.ArrangeLangVerseList(Language.Lang0);
            Testsong.ArrangeMaxLineAndLangVersList(maxZeilen);
            string test = Testsong.SelectedVerseListMaxLines.ToString();
            Debug.Print(Testsong.Vorspann + test);
            //Assert
            Assert.IsTrue(Testsong.Vorspann + test == TestsongPattern);
        }
        [TestMethod()]
        public void GenerateMaxLineVersList3()
        {
            //Arrange
            TestsongDummyCreator TestsongDummy = new TestsongDummyCreator();
            int maxZeilen = 2;
            string TestsongPattern = TestsongDummy.GenerateTestSong(2, 3, 2, maxZeilen);    //Verse|Verszeilen|Sprachen|Max Zeilenzahl
            Song Testsong = new Song(TestsongPattern);
            Testsong.ArrangeLangVerseList(Language.Lang1);
            Testsong.ArrangeMaxLineAndLangVersList(maxZeilen);
            string test = Testsong.SelectedVerseListMaxLines.ToString();
            Debug.Print(Testsong.Vorspann + test);
            //Assert
            Assert.IsTrue(test == "---\r\nVers1\r\nVers1 Zeile2 SpracheNr2\r\n---\r\nVers2\r\nVers2 Zeile2 SpracheNr2\r\n");
        }

    }
}