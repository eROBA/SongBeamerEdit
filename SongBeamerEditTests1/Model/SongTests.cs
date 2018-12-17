using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongBeamerEdit.Converting;
using SongBeamerEdit.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SongBeamerEdit.FlagsValueConverter;

namespace SongBeamerEdit.Model.Tests
{
    [TestClass()]
    public class SongTests
    {
        public string GenerateTestSong(int anzahlVerse, int anzahlVerszeilen, int anzahlSprachen, int maxZeilenProPage)
        {
            if (maxZeilenProPage < anzahlSprachen) maxZeilenProPage = anzahlSprachen;
            string vorspann = "#LangCount=" + anzahlSprachen + "\r\n#Title=Titel\r\n#TitleLang2=Titel zweite Sprache\r\n#OTitle=Originaltitel\r\n#Author=Autor\r\n#(c)=Copyright\r\n#NatCopyright=Nationale Rechte\r\n#Melody=Melodie\r\n#Bible=Römer 8,14-16\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Songbook=Du bist Herr IV/001\r\n";
            string testSong = vorspann;
            for (int i = 1; i < anzahlVerse+1; i++)
            {
                testSong += "---" + "\r\n" +"Vers" + (i) + "\r\n";
                int spracheNr = 0;
                int iii = 0;
                for (int ii = 1; ii < anzahlVerszeilen+1; ii++)
                {
                    if (spracheNr < anzahlSprachen)
                    {
                        spracheNr++;
                    }
                    else
                    {
                        spracheNr = 1;
                    }
                    if (iii >= maxZeilenProPage)
                    {
                        testSong += "--" + "\r\n";
                        iii = 0;
                    }
                    iii++;
                    testSong += "Vers" + i + " Zeile" + ii + " SpracheNr" + spracheNr + "\r\n";
                }
            }
            return testSong;
        }
        [TestMethod()]
        public void GenerateExplicitLine()
        {
            //Arrange
            string LineText1 = "##2 Zeile1 Sprache2";
            //Act
            Line TestLine = new Line(LineText1);
            //Assert
            Assert.IsTrue(TestLine.BitwiseLanguageNr == Language.Lang1 && TestLine.LineText == "Zeile1 Sprache2");
        }
        [TestMethod()]
        public void GenerateImplicitLine()
        {
            //Arrange
            string LineText2 = "Zeile1 Sprache1";
            //Act
            Line TestLine = new Line(LineText2);
            //Assert
            Assert.IsTrue(TestLine.BitwiseLanguageNr == Language.None && TestLine.LineText == "Zeile1 Sprache1");
        }
        [TestMethod()]
        public void GenerateVerseListTest()
        {
            //Arrange
            string Testsong4 = "#LangCount=2\r\n#Title=Titel\r\n#TitleLang2=Titel zweite Sprache\r\n#OTitle=Originaltitel\r\n#Author=Autor\\r\\n#(c)=Copyright\\\r\nNatCopyright=Nationale Rechte\\\r\nMelody=Melodie\\\r\nBible=Römer 8,14-16\\\r\nEditor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\nTitleFormat=U\\\r\nSongbook=Du bist Herr IV/001\r\n---\r\nVers 1\r\nVers1 Zeile1 Sprache1\r\nVers1 Zeile2 Sprache2\r\nVers1 Zeile3 Sprache1\r\nVers1 Zeile4 Sprache2\r\n---\r\nVers 2\r\nVers2 Zeile1 Sprache1\r\nVers2 Zeile2 Sprache2\r\nVers2 Zeile3 Sprache1\r\nVers2 Zeile4 Sprache2\r\n---\r\nRefrain 1\r\nRefrain1 Zeile1 Sprache1\r\nRefrain1 Zeile2 Sprache2\r\n---\r\nVers 3\r\nVers3 Zeile1 Sprache1\r\nVers3 Zeile2 Sprache2\r\nVers3 Zeile3 Sprache1\r\nVers3 Zeile4 Sprache2\r\n---\r\nVers 4\r\nVers4 Zeile1 Sprache1\r\nVers4 Zeile2 Sprache2\r\nVers4 Zeile3 Sprache1\r\nVers4 Zeile4 Sprache2\r\n---\r\nRefrain 2\r\nRefrain2 Zeile1 Sprache1\r\nRefrain2 Zeile2 Sprache2\r\n";
            //Act
            Song Testsong = new Song(Testsong4);
            Testsong.SongAnalyse(Testsong4);
            Testsong.GenerateSelectedVerseList(Language.Lang0 | Language.Lang1 );
            string Test = Testsong.VerseList.ToString();
            //Assert
            Assert.IsTrue(Test == "---\r\nVers 1\r\nVers1 Zeile1 Sprache1\r\nVers1 Zeile2 Sprache2\r\nVers1 Zeile3 Sprache1\r\nVers1 Zeile4 Sprache2\r\n---\r\nVers 2\r\nVers2 Zeile1 Sprache1\r\nVers2 Zeile2 Sprache2\r\nVers2 Zeile3 Sprache1\r\nVers2 Zeile4 Sprache2\r\n---\r\nRefrain 1\r\nRefrain1 Zeile1 Sprache1\r\nRefrain1 Zeile2 Sprache2\r\n---\r\nVers 3\r\nVers3 Zeile1 Sprache1\r\nVers3 Zeile2 Sprache2\r\nVers3 Zeile3 Sprache1\r\nVers3 Zeile4 Sprache2\r\n---\r\nVers 4\r\nVers4 Zeile1 Sprache1\r\nVers4 Zeile2 Sprache2\r\nVers4 Zeile3 Sprache1\r\nVers4 Zeile4 Sprache2\r\n---\r\nRefrain 2\r\nRefrain2 Zeile1 Sprache1\r\nRefrain2 Zeile2 Sprache2\r\n");
        }
        [TestMethod()]
        public void GenerateVerseListTestExplicitLanguages()
        {
            //Arrange
            string Testsong4 = "#LangCount=2\r\n#Title=Titel\r\n#TitleLang2=Titel zweite Sprache\r\n#OTitle=Originaltitel\r\n#Author=Autor\\r\\n#(c)=Copyright\\\r\nNatCopyright=Nationale Rechte\\\r\nMelody=Melodie\\\r\nBible=Römer 8,14-16\\\r\nEditor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\nTitleFormat=U\\\r\nSongbook=Du bist Herr IV/001\r\n---\r\nVers 1\r\nVers1 Zeile1 Sprache1\r\n##1 Vers1 Zeile2 Sprache1\r\nVers1 Zeile3 Sprache2\r\nVers1 Zeile4 Sprache1\r\nVers1 Zeile5 Sprache2\r\n---\r\nVers 2\r\nVers2 Zeile1 Sprache1\r\nVers2 Zeile2 Sprache2\r\nVers2 Zeile3 Sprache1\r\nVers2 Zeile4 Sprache2\r\n---\r\nRefrain 1\r\nRefrain1 Zeile1 Sprache1\r\nRefrain1 Zeile2 Sprache2\r\n---\r\nVers 3\r\nVers3 Zeile1 Sprache1\r\nVers3 Zeile2 Sprache2\r\nVers3 Zeile3 Sprache1\r\nVers3 Zeile4 Sprache2\r\n---\r\nVers 4\r\nVers4 Zeile1 Sprache1\r\nVers4 Zeile2 Sprache2\r\nVers4 Zeile3 Sprache1\r\nVers4 Zeile4 Sprache2\r\n---\r\nRefrain 2\r\nRefrain2 Zeile1 Sprache1\r\nRefrain2 Zeile2 Sprache2\r\n";
            //Act
            Song Testsong = new Song();
            Testsong.SongAnalyse(Testsong4);
            Testsong.GenerateSelectedVerseList(Language.Lang0 | Language.Lang1 | Language.Lang2 | Language.Lang3);
            string Test = Testsong.VerseList.ToString();
            //Assert
            Assert.IsTrue(Test == "---\r\nVers 1\r\nVers1 Zeile1 Sprache1\r\n##1 Vers1 Zeile2 Sprache1\r\nVers1 Zeile3 Sprache2\r\nVers1 Zeile4 Sprache1\r\nVers1 Zeile5 Sprache2\r\n---\r\nVers 2\r\nVers2 Zeile1 Sprache1\r\nVers2 Zeile2 Sprache2\r\nVers2 Zeile3 Sprache1\r\nVers2 Zeile4 Sprache2\r\n---\r\nRefrain 1\r\nRefrain1 Zeile1 Sprache1\r\nRefrain1 Zeile2 Sprache2\r\n---\r\nVers 3\r\nVers3 Zeile1 Sprache1\r\nVers3 Zeile2 Sprache2\r\nVers3 Zeile3 Sprache1\r\nVers3 Zeile4 Sprache2\r\n---\r\nVers 4\r\nVers4 Zeile1 Sprache1\r\nVers4 Zeile2 Sprache2\r\nVers4 Zeile3 Sprache1\r\nVers4 Zeile4 Sprache2\r\n---\r\nRefrain 2\r\nRefrain2 Zeile1 Sprache1\r\nRefrain2 Zeile2 Sprache2\r\n");
        }
        [TestMethod()]
        public void GenerateSelectedVerseListTest()
        {
            //Arrange
            string Testsong5 = GenerateTestSong(3, 6, 3, 6);    //Verse|Verszeilen|Sprachen|Max Zeilenzahl
            //Act
            Song Testsong = new Song(Testsong5);
            Testsong.SongAnalyse(Testsong5);
            Testsong.GenerateSelectedVerseList(Language.Lang0 | Language.Lang1 | Language.Lang2);
            //Assert
            string Test = Testsong.Vorspann + Testsong.SelectedVerseList.ToString();
            Debug.Write("Der Text der durch SongBeamerEdit erzeugt wurde \r\n" + Test);
            Debug.Write("Der Text der durch Testcode erstellt wurde \r\n" + GenerateTestSong(3, 6, 3, 3));
            Assert.IsTrue(Test == GenerateTestSong(3, 6, 3, 3));
        }
    }
}