using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongBeamerEdit.Converting;
using SongBeamerEdit.Model;
using System.Diagnostics;

namespace SongBeamerEditUnitTests
{
    [TestClass]
    public class UnitTestsSong
    {
        [TestMethod]
        public void LineTestExplicit()
        {
            //Arrange
            string vers = "##2 Das ist eine Zeile mit explizierter Sprachkennzeichnung in der zweiten Sprache";
            //Act
            Line Testline =  new Line(vers);
            Debug.Print("Die Sprache hat die Nummer: {0}.", Testline.LanguageNr);
            Debug.Print("Der Text der Zeile lautet: {0}", Testline.LineText);
            //Assert
            Assert.IsTrue(Testline.LanguageNr == 2 | Testline.LineText == "Das ist eine Zeile mit explizierter Sprachkennzeichnung in der zweiten Sprache");
        }
        [TestMethod]
        public void LineTestImplicit()
        {
            //Arrange
            string vers = "Das ist eine Zeile mit impliziter Sprachkennzeichnung d.h. erste Sprache";
            //Act
            Line Testline = new Line(vers);
            Debug.Print("Die Sprache hat die Nummer: {0}.", Testline.LanguageNr);
            Debug.Print("Der Text der Zeile lautet: {0}", Testline.LineText);
            //Assert
            Assert.IsTrue(Testline.LanguageNr == 1 | Testline.LineText == "Das ist eine Zeile mit impliziter Sprachkennzeichnung d.h. erste Sprache");
        }
        [TestMethod]
        public void LineTestEmpty()
        {
            //Arrange
            string vers = "";
            //Act
            Line Testline = new Line(vers);
            Debug.Print("Die Sprache hat die Nummer: {0}.", Testline.LanguageNr);
            Debug.Print("Der Text der Zeile lautet: {0}", Testline.LineText);
            //Assert
            Assert.IsTrue(Testline.LanguageNr == 1 | Testline.LineText == "");
        }
        [TestMethod]
        public void LanguageFilter()
        {
            //Arrange
            string Testsong4 = "#LangCount=4\r\n#Title=Abba Vater\r\n#TitleLang2=Abba Vater\r\n#OTitle=Abba Father\r\n#Author=Cindy Rethmeier\r\n#(c)=Vineyard Publishing, USA\r\n#NatCopyright=Projektion J Musikverlag, Asslar\r\n#Melody=Cindy Rethmeier\r\n#Bible=Römer 8,14-16\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Songbook=Du bist Herr IV / 001\r\n---\r\nAbba Vater, liebender Vater,\r\nAbba Father, our loving Father,\r\nanbetend kommen wir.\r\nwe've come to worship you.\r\n---\r\nSagen: wir lieben dich,\r\nTo say we love you,\r\nerheben uns’re Hände,\r\nto lift our hands up to you,\r\n---\r\nAnbetend kommen wir.\r\nwe've come to worship you.\r\n---\r\nWir sind deine Kinder,\r\nWe are your children,\r\ndurch deinen Geist geboren.\r\nborn of your spirit,\r\n---\r\nberufen durch dich,\r\ncalled by your name,\r\nerwählt durch deine Hand.\r\nchosen by your hand.\r\n---\r\nWir gehören dir.\r\nWe belong to you.\r\n\r\n\r\n";
            //Act
            Song Testsong = new Song(Testsong4);
            Testsong.GenerateSelectedVerseList(2);
            Debug.Print("Die Sprache hat die Nummer: {0}.", Testsong.AnzahlZeilenProSeite);
            Debug.Print("Der Text der Zeile lautet: {0}", Testsong.SelectedVerseList);
            //Assert
            //Assert.IsTrue(Testsong.LanguageNr == 1 | Testsong.LineText == "");
        }
    }
}
