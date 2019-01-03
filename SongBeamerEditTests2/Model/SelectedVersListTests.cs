using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SongBeamerEdit.Model;
using SongBeamerEdit.FlagsValueConverter;
using SongBeamerEditTests.Model;

namespace SongBeamerEditUnitTests.Model
{
    [TestClass()]
    public class SelectedVersListTests
    {
        [TestMethod()]
        public void GenerateVerseListWithAllAvailableLanguges()
        {
            //Arrange
            string TestsongPattern = "#LangCount=2\r\n#Title=Titel\r\n#TitleLang2=Titel zweite Sprache\r\n#OTitle=Originaltitel\r\n#Author=Autor\\r\\n#(c)=Copyright\\\r\n#NatCopyright=Nationale Rechte\\\r\n#Melody=Melodie\\\r\n#Bible=Römer 8,14-16\\\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\\\r\n#Songbook=Du bist Herr IV/001\r\n---\r\nVers 1\r\nVers1 Zeile1 Sprache1\r\nVers1 Zeile2 Sprache2\r\nVers1 Zeile3 Sprache1\r\nVers1 Zeile4 Sprache2\r\n---\r\nVers 2\r\nVers2 Zeile1 Sprache1\r\nVers2 Zeile2 Sprache2\r\nVers2 Zeile3 Sprache1\r\nVers2 Zeile4 Sprache2\r\n---\r\nRefrain 1\r\nRefrain1 Zeile1 Sprache1\r\nRefrain1 Zeile2 Sprache2\r\n---\r\nVers 3\r\nVers3 Zeile1 Sprache1\r\nVers3 Zeile2 Sprache2\r\nVers3 Zeile3 Sprache1\r\nVers3 Zeile4 Sprache2\r\n---\r\nVers 4\r\nVers4 Zeile1 Sprache1\r\nVers4 Zeile2 Sprache2\r\nVers4 Zeile3 Sprache1\r\nVers4 Zeile4 Sprache2\r\n---\r\nRefrain 2\r\nRefrain2 Zeile1 Sprache1\r\nRefrain2 Zeile2 Sprache2";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", TestsongPattern);
            Song Testsong = new Song(TestsongPattern);
            //Testsong.SongAnalyse(TestsongPattern);
            Testsong.ArrangeLangVerseList(Language.Lang0 | Language.Lang1);
            string Test = Testsong.InitalVerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", Testsong.Vorspann, Testsong.InitalVerseList.ToString());
            //Assert
            Assert.IsTrue(Test == "---\r\nVers 1\r\nVers1 Zeile1 Sprache1\r\nVers1 Zeile2 Sprache2\r\nVers1 Zeile3 Sprache1\r\nVers1 Zeile4 Sprache2\r\n---\r\nVers 2\r\nVers2 Zeile1 Sprache1\r\nVers2 Zeile2 Sprache2\r\nVers2 Zeile3 Sprache1\r\nVers2 Zeile4 Sprache2\r\n---\r\nRefrain 1\r\nRefrain1 Zeile1 Sprache1\r\nRefrain1 Zeile2 Sprache2\r\n---\r\nVers 3\r\nVers3 Zeile1 Sprache1\r\nVers3 Zeile2 Sprache2\r\nVers3 Zeile3 Sprache1\r\nVers3 Zeile4 Sprache2\r\n---\r\nVers 4\r\nVers4 Zeile1 Sprache1\r\nVers4 Zeile2 Sprache2\r\nVers4 Zeile3 Sprache1\r\nVers4 Zeile4 Sprache2\r\n---\r\nRefrain 2\r\nRefrain2 Zeile1 Sprache1\r\nRefrain2 Zeile2 Sprache2\r\n");
        }
        [TestMethod()]
        public void GenerateVerseListWithMoreThanAvailableLanguges()
        {
            //Arrange
            string TestsongPattern = "#LangCount=2\r\n#Title=Titel\r\n#TitleLang2=Titel zweite Sprache\r\n#OTitle=Originaltitel\r\n#Author=Autor\\r\\n#(c)=Copyright\\\r\n#NatCopyright=Nationale Rechte\\\r\n#Melody=Melodie\\\r\n#Bible=Römer 8,14-16\\\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\\\r\n#Songbook=Du bist Herr IV/001\r\n---\r\nVers 1\r\nVers1 Zeile1 Sprache1\r\nVers1 Zeile2 Sprache2\r\nVers1 Zeile3 Sprache1\r\nVers1 Zeile4 Sprache2\r\n---\r\nVers 2\r\nVers2 Zeile1 Sprache1\r\nVers2 Zeile2 Sprache2\r\nVers2 Zeile3 Sprache1\r\nVers2 Zeile4 Sprache2\r\n---\r\nRefrain 1\r\nRefrain1 Zeile1 Sprache1\r\nRefrain1 Zeile2 Sprache2\r\n---\r\nVers 3\r\nVers3 Zeile1 Sprache1\r\nVers3 Zeile2 Sprache2\r\nVers3 Zeile3 Sprache1\r\nVers3 Zeile4 Sprache2\r\n---\r\nVers 4\r\nVers4 Zeile1 Sprache1\r\nVers4 Zeile2 Sprache2\r\nVers4 Zeile3 Sprache1\r\nVers4 Zeile4 Sprache2\r\n---\r\nRefrain 2\r\nRefrain2 Zeile1 Sprache1\r\nRefrain2 Zeile2 Sprache2";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", TestsongPattern);
            Song Testsong = new Song(TestsongPattern);
            //Testsong.SongAnalyse(TestsongPattern);
            Testsong.ArrangeLangVerseList(Language.Lang0 | Language.Lang1 | Language.Lang2 | Language.Lang3);
            string Test = Testsong.InitalVerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", Testsong.Vorspann, Testsong.InitalVerseList.ToString());
            //Assert
            Assert.IsTrue(Test == "---\r\nVers 1\r\nVers1 Zeile1 Sprache1\r\nVers1 Zeile2 Sprache2\r\nVers1 Zeile3 Sprache1\r\nVers1 Zeile4 Sprache2\r\n---\r\nVers 2\r\nVers2 Zeile1 Sprache1\r\nVers2 Zeile2 Sprache2\r\nVers2 Zeile3 Sprache1\r\nVers2 Zeile4 Sprache2\r\n---\r\nRefrain 1\r\nRefrain1 Zeile1 Sprache1\r\nRefrain1 Zeile2 Sprache2\r\n---\r\nVers 3\r\nVers3 Zeile1 Sprache1\r\nVers3 Zeile2 Sprache2\r\nVers3 Zeile3 Sprache1\r\nVers3 Zeile4 Sprache2\r\n---\r\nVers 4\r\nVers4 Zeile1 Sprache1\r\nVers4 Zeile2 Sprache2\r\nVers4 Zeile3 Sprache1\r\nVers4 Zeile4 Sprache2\r\n---\r\nRefrain 2\r\nRefrain2 Zeile1 Sprache1\r\nRefrain2 Zeile2 Sprache2\r\n");
        }
        [TestMethod]
        public void GenerateVerseListWithDifferentLanguages()
        {
            //Arrange
            string TestsongPattern = "#LangCount=2\r\n#Title=Abba Vater\r\n#TitleLang2=Abba Vater\r\n#OTitle=Abba Father\r\n#Author=Cindy Rethmeier\r\n#(c)=Vineyard Publishing, USA\r\n#NatCopyright=Projektion J Musikverlag, Asslar\r\n#Melody=Cindy Rethmeier\r\n#Bible=Römer 8,14-16\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Songbook=Du bist Herr IV / 001\r\n---\r\nAbba Vater, liebender Vater,\r\nAbba Father, our loving Father,\r\nanbetend kommen wir.\r\nwe've come to worship you.\r\n---\r\nSagen: wir lieben dich,\r\nTo say we love you,\r\nerheben uns’re Hände,\r\nto lift our hands up to you,\r\n---\r\nAnbetend kommen wir.\r\nwe've come to worship you.\r\n---\r\nWir sind deine Kinder,\r\nWe are your children,\r\ndurch deinen Geist geboren.\r\nborn of your spirit,\r\n---\r\nberufen durch dich,\r\ncalled by your name,\r\nerwählt durch deine Hand.\r\nchosen by your hand.\r\n---\r\nWir gehören dir.\r\nWe belong to you.\r\n\r\n\r\n";
            //Act
            Song TestsongLang0 = new Song(TestsongPattern);
            Song TestsongLang1 = new Song(TestsongPattern);
            Song TestsongLang0And1 = new Song(TestsongPattern);
            TestsongLang0.ArrangeLangVerseList(Language.Lang0);
            TestsongLang1.ArrangeLangVerseList(Language.Lang1);
            TestsongLang0And1.ArrangeLangVerseList(Language.Lang0 | Language.Lang1);
            Debug.Print("******Die erste Sprache:\r\n{0}", TestsongLang0.SelectedVerseList);
            Debug.Print("******Die zweite Sprache:\r\n{0}", TestsongLang1.SelectedVerseList);
            Debug.Print("******Beide Sprachen:\r\n{0}", TestsongLang0And1.SelectedVerseList);
            //Assert
            string text0 = TestsongLang0.SelectedVerseList.ToString();
            string text1 = TestsongLang1.SelectedVerseList.ToString();
            string text01 = TestsongLang0And1.SelectedVerseList.ToString();
            Assert.IsTrue(TestsongLang0.SelectedVerseList.ToString() == "---\r\nAbba Vater, liebender Vater,\r\nanbetend kommen wir.\r\n---\r\nSagen: wir lieben dich,\r\nerheben uns’re Hände,\r\n---\r\nAnbetend kommen wir.\r\n---\r\nWir sind deine Kinder,\r\ndurch deinen Geist geboren.\r\n---\r\nberufen durch dich,\r\nerwählt durch deine Hand.\r\n---\r\nWir gehören dir.\r\n");
            Assert.IsTrue(TestsongLang1.SelectedVerseList.ToString() == "---\r\nAbba Father, our loving Father,\r\nwe've come to worship you.\r\n---\r\nTo say we love you,\r\nto lift our hands up to you,\r\n---\r\nwe've come to worship you.\r\n---\r\nWe are your children,\r\nborn of your spirit,\r\n---\r\ncalled by your name,\r\nchosen by your hand.\r\n---\r\nWe belong to you.\r\n");
            Assert.IsTrue(TestsongLang0And1.SelectedVerseList.ToString() == "---\r\nAbba Vater, liebender Vater,\r\nAbba Father, our loving Father,\r\nanbetend kommen wir.\r\nwe've come to worship you.\r\n---\r\nSagen: wir lieben dich,\r\nTo say we love you,\r\nerheben uns’re Hände,\r\nto lift our hands up to you,\r\n---\r\nAnbetend kommen wir.\r\nwe've come to worship you.\r\n---\r\nWir sind deine Kinder,\r\nWe are your children,\r\ndurch deinen Geist geboren.\r\nborn of your spirit,\r\n---\r\nberufen durch dich,\r\ncalled by your name,\r\nerwählt durch deine Hand.\r\nchosen by your hand.\r\n---\r\nWir gehören dir.\r\nWe belong to you.\r\n");
        }
        [TestMethod()]
        public void GenerateSelectedVerseListWithAutomticallyText()
        {
            //Arrange
            TestsongDummyCreator TestsongDummy = new TestsongDummyCreator();
            string TestsongPattern = TestsongDummy.GenerateTestSong(3, 6, 3, 6);    //Verse|Verszeilen|Sprachen|Max Zeilenzahl
            //Act
            Song Testsong = new Song(TestsongPattern);
            //Testsong.SongAnalyse(TestsongPattern);
            Testsong.ArrangeLangVerseList(Language.Lang0 | Language.Lang1 | Language.Lang2);
            //Assert
            string Test = Testsong.Vorspann + Testsong.SelectedVerseList.ToString();
            string Ergebnis = TestsongDummy.GenerateTestSong(3, 6, 3, 6);
            Debug.Write("Der Text der durch Testcode erstellt wurde \r\n" + TestsongDummy.GenerateTestSong(3, 6, 3, 6));
            Debug.Write("Der Text der durch SongBeamerEdit erzeugt wurde \r\n" + Test);
            Assert.IsTrue(Test == Ergebnis);
        }
    }
}
