using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SongBeamerEdit.Model;
using SongBeamerEdit.FlagsValueConverter;

namespace SongBeamerEditUnitTests
{
    [TestClass]
    public class VerseListTests
    {
        [TestMethod]
        public void NurEinVersMitVerskennzeichnung()
        {
            //Arrange
            string Testsong = "#LangCount=1\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nVers\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            string ErgebnisTestsong = "---\r\nVers\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", Testsong);
            Song testVMSongbeamerFormat = new Song(Testsong);
            string test = testVMSongbeamerFormat.VerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", testVMSongbeamerFormat.Vorspann, testVMSongbeamerFormat.VerseList.ToString());
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.VerseList.ToString() == ErgebnisTestsong);
        }
        [TestMethod]
        public void NurEinVersOhneVerskennzeichnung()
        {
            //Arrange
            string Testsong = "#LangCount=1\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            string ErgebnisTestsong = "---\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", Testsong);
            Song testVMSongbeamerFormat = new Song(Testsong);
            string test = testVMSongbeamerFormat.VerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", testVMSongbeamerFormat.Vorspann, testVMSongbeamerFormat.VerseList.ToString());
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.VerseList.ToString() == ErgebnisTestsong);
        }
        [TestMethod]
        public void NurEinChorusVerskennzeichnung()
        {
            //Arrange
            string Testsong = "#LangCount=1\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nChorus\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            string ErgebnisTestsong = "---\r\nChorus\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", Testsong);
            Song testVMSongbeamerFormat = new Song(Testsong);
            string test = testVMSongbeamerFormat.VerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", testVMSongbeamerFormat.Vorspann, testVMSongbeamerFormat.VerseList.ToString());
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.VerseList.ToString() == ErgebnisTestsong);
        }
        [TestMethod]
        public void NurEinVersMitVerskennzeichnungZweiSprachen()
        {
            //Arrange
            string Testsong = "#LangCount=2\r\n#Title=Abba Vater\r\n#TitleLang2=Abba Vater\r\n#OTitle=Abba Father\r\n#Author=Cindy Rethmeier\r\n#(c)=Vineyard Publishing, USA\r\n#NatCopyright=Projektion J Musikverlag, Asslar\r\n#Melody=Cindy Rethmeier\r\n#Bible=Römer 8,14-16\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Songbook=Du bist Herr IV / 001\r\n---\r\nVers\r\nAbba Vater, liebender Vater,\r\nAbba Father, our loving Father,\r\nanbetend kommen wir.\r\nwe've come to worship you.";
            string ErgebnisTestsong = "---\r\nVers\r\nAbba Vater, liebender Vater,\r\nAbba Father, our loving Father,\r\nanbetend kommen wir.\r\nwe've come to worship you.\r\n";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", Testsong);
            Song testVMSongbeamerFormat = new Song(Testsong);
            testVMSongbeamerFormat.GenerateSelectedVerseList(Language.Lang0 | Language.Lang1);
            string test = testVMSongbeamerFormat.SelectedVerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", testVMSongbeamerFormat.Vorspann, testVMSongbeamerFormat.VerseList.ToString());
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.SelectedVerseList.ToString() == ErgebnisTestsong);
        }
        [TestMethod]
        public void MehrereVerseOhneKennzeichnung()
        {
            //Arrange
            string Testsong = "#LangCount=1\r\n#Title=Schwabdidu\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nSchwappdidu\r\nGott kennt meinen Namen\r\nSchwappdidu\r\nEr wei� wer ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\nEr liebt mich,\r\nso wie ich bin\r\n---\r\nSchwappdidu\r\nGott kennt meinen Namen\r\nSchwappdidu\r\nEr wei� wer ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\nEr liebt mich,\r\nso wie ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\nSchwappdidu\r\nHalleluja Halleluja Amen";
            string ErgebnisTestsong = "#LangCount=1\r\n#Title=Schwabdidu\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nSchwappdidu\r\nGott kennt meinen Namen\r\nSchwappdidu\r\nEr wei� wer ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\nEr liebt mich,\r\nso wie ich bin\r\n---\r\nSchwappdidu\r\nGott kennt meinen Namen\r\nSchwappdidu\r\nEr wei� wer ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\nEr liebt mich,\r\nso wie ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\nSchwappdidu\r\nHalleluja Halleluja Amen\r\n";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", Testsong);
            Song testVMSongbeamerFormat = new Song(Testsong);
            string test = testVMSongbeamerFormat.Vorspann + testVMSongbeamerFormat.VerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", testVMSongbeamerFormat.Vorspann, testVMSongbeamerFormat.VerseList.ToString());
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Vorspann + testVMSongbeamerFormat.VerseList.ToString() == ErgebnisTestsong);
        }
        [TestMethod]
        public void MitVersenUndRefrain()
        {
            //Arrange
            string Testsong =         "#LangCount=1\r\n#Title=All heav�n declares - 4\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Songbook=Wiedenester / 004\r\n#Author=Noel Richard\r\n#(c)=(c) 1987 Kingsway�s Thankyou Music\r\n#Melody=Trica Richard\r\n#AddCopyrightInfo=F�r D,A,CH: Projektion J<br>Musikverlag, Asslar<br>www.inAktion.de\r\n---\r\nVers 1\r\nAll heav�n declares\r\nthe glory of the risen Lord.\r\nWho can compare\r\nwith the beauty of the Lord?\r\n---\r\nRefrain\r\nForever He (You) will be\r\nthe Lamb upon the throne.\r\nI gladly bow the knee\r\nand worship Him (You) alone.\r\n---\r\nVers 2\r\nI will proclaim\r\nthe glory of the risen Lord,\r\nwho once was slain\r\nto reconcile man of God.";
            string ErgebnisTestsong = "#LangCount=1\r\n#Title=All heav�n declares - 4\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Songbook=Wiedenester / 004\r\n#Author=Noel Richard\r\n#(c)=(c) 1987 Kingsway�s Thankyou Music\r\n#Melody=Trica Richard\r\n#AddCopyrightInfo=F�r D,A,CH: Projektion J<br>Musikverlag, Asslar<br>www.inAktion.de\r\n---\r\nVers 1\r\nAll heav�n declares\r\nthe glory of the risen Lord.\r\nWho can compare\r\nwith the beauty of the Lord?\r\n---\r\nRefrain\r\nForever He (You) will be\r\nthe Lamb upon the throne.\r\nI gladly bow the knee\r\nand worship Him (You) alone.\r\n---\r\nVers 2\r\nI will proclaim\r\nthe glory of the risen Lord,\r\nwho once was slain\r\nto reconcile man of God.\r\n";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", Testsong);
            Song testVMSongbeamerFormat = new Song(Testsong);
            string test = testVMSongbeamerFormat.Vorspann + testVMSongbeamerFormat.VerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", testVMSongbeamerFormat.Vorspann, testVMSongbeamerFormat.VerseList.ToString());
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Vorspann + testVMSongbeamerFormat.VerseList.ToString() == ErgebnisTestsong);
        }
        [TestMethod]
        public void MitVersenUndRefrains()
        {
            //Arrange
            string Testsong = "#LangCount=1\r\n#Title=Vater, wir kommen vor deinen Thron\r\n#Author=Albert Frey\r\n#(c)=H�nssler Verlag, Holzgerlingen\r\n#NatCopyright=2004\r\n#Melody=Albert Frey\r\n#Key=C\r\n#Songbook=Feiert Jesus III / 002\r\n#AddCopyrightInfo=Tempo 113\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nVers 1\r\n\r\nVater, wir kommen vor deinen Thron.\r\nDu bist voll Freude, erwartest uns schon.\r\nDu gibst uns Raum und du hast f�r uns Zeit,\r\n�ffnest uns dein Herz weit.\r\n---\r\nRefrain 1\r\nDu bist gro�, du gibst den Sternen ihren Lauf.\r\nDu bist gut und deine Liebe h�rt nie auf.\r\n---\r\nVers 2\r\nDu bist uns heilig, wir lieben dich, Herr.\r\nDein Einfluss wachse bei uns immer mehr.\r\nDein heiliger Name wird so in der Welt\r\nneu wiederhergestellt.\r\n---\r\nRefrain 2\r\nDu bist gro�, du gibst den Sternen ihren Lauf.\r\nDu bist gut und deine Liebe h�rt nie auf. (2 x)\r\nDu bist gro�, du bist gut.";
            string ErgebnisTestsong = "#LangCount=1\r\n#Title=Vater, wir kommen vor deinen Thron\r\n#Author=Albert Frey\r\n#(c)=H�nssler Verlag, Holzgerlingen\r\n#NatCopyright=2004\r\n#Melody=Albert Frey\r\n#Key=C\r\n#Songbook=Feiert Jesus III / 002\r\n#AddCopyrightInfo=Tempo 113\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nVers 1\r\nVater, wir kommen vor deinen Thron.\r\nDu bist voll Freude, erwartest uns schon.\r\nDu gibst uns Raum und du hast f�r uns Zeit,\r\n�ffnest uns dein Herz weit.\r\n---\r\nRefrain 1\r\nDu bist gro�, du gibst den Sternen ihren Lauf.\r\nDu bist gut und deine Liebe h�rt nie auf.\r\n---\r\nVers 2\r\nDu bist uns heilig, wir lieben dich, Herr.\r\nDein Einfluss wachse bei uns immer mehr.\r\nDein heiliger Name wird so in der Welt\r\nneu wiederhergestellt.\r\n---\r\nRefrain 2\r\nDu bist gro�, du gibst den Sternen ihren Lauf.\r\nDu bist gut und deine Liebe h�rt nie auf. (2 x)\r\nDu bist gro�, du bist gut.\r\n";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", Testsong);
            Song testVMSongbeamerFormat = new Song(Testsong);
            string test = testVMSongbeamerFormat.Vorspann + testVMSongbeamerFormat.VerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", testVMSongbeamerFormat.Vorspann, testVMSongbeamerFormat.VerseList.ToString());
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Vorspann + testVMSongbeamerFormat.VerseList.ToString() == ErgebnisTestsong);
        }
        [TestMethod]
        public void RefrainVersNrMehrmals()
        {
            //Arrange
            string Testsong = "#LangCount=1\r\n#Title=Lobe den Herrn! Sing Ihm dein Lied!\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Author=Werner Arthur Hoffmann\r\n#(c)=1992 Musikverlag Klaus Gerth, Asslar\r\n#Melody=Werner Arthur Hoffmann\r\n#Key=E\r\n#Bible=Psalm 103\r\n#Songbook=Feiert Jesus I / 54\r\n#Chords=MC41LDEsRQ0wLjUsMixFL0cjDTMuNSwzLEENMTguNSwzLEYjNy9BIw05LjUsNCxFL0I9DTE4LDQsQj0NMC41LDYsRQ0wLjUsNyxFL0cjDTAuNSw4LEENMC41LDksQj03DTkuNSw5LEUNNC41LDEyLEENMTQuNSwxMixCPQ0yMy41LDEyLEUNNC41LDEzLEENMTAuNSwxMyxCPTcNMTguNSwxMyxCPQ0yMywxMyxFDTQuNSwxNCxBDTE2LjUsMTQsQj0NMjYsMTQsRQ0tMSwxNSxBDTYuNSwxNSxFDTE5LjUsMTUsQj03DTI1LjUsMTUsRQ0wLjUsMTgsRQ0wLjUsMTksRS9HIw0zLjUsMjAsQQ0xOC41LDIwLEYjNy9BIw05LjUsMjEsRS9CPQ0xOCwyMSxCPQ0wLjUsMjMsRQ0wLjUsMjQsRS9HIw0wLjUsMjUsQQ0wLjUsMjYsQj03DTkuNSwyNixFDTQuNSwyOSxBDTE4LjUsMjksQj0NMjYuNSwyOSxFDTMuNSwzMCxBDTE3LDMwLEI9Nw0yOS41LDMwLEI9DTMyLDMwLEUNMy41LDMxLEENMTcuNSwzMSxCPQ0yNy41LDMxLEUNLTEsMzIsQQ02LjUsMzIsRQ0xOC41LDMyLEI9Nw0yNS41LDMyLEUNMC41LDM1LEUNMC41LDM2LEUvRyMNMy41LDM3LEENMTguNSwzNyxGIzcvQSMNOS41LDM4LEUvQj0NMTgsMzgsQj0NMC41LDQwLEUNMC41LDQxLEUvRyMNMC41LDQyLEENMC41LDQzLEI9Nw05LjUsNDMsRQ0zLjUsNDYsQQ0xNy41LDQ2LEI9DTI5LjUsNDYsRQ02LjUsNDcsQQ0xNS41LDQ3LEI9Nw0yNSw0NyxCPQ0yOSw0NyxFDTQuNSw0OCxBDTE2LjUsNDgsQj0NMjYuNSw0OCxFDS0xLDQ5LEENNC41LDQ5LEUNMTYuNSw0OSxCPTcNMjUuNSw0OSxFDTAuNSw1MixFDTAuNSw1MyxFL0cjDTMuNSw1NCxBDTE4LjUsNTQsRiM3L0EjDTkuNSw1NSxFL0I9DTE4LDU1LEI9DTAuNSw1NyxFDTAuNSw1OCxFL0cjDTAuNSw2MCxBDTAuNSw2MSxCPTcNOS41LDYxLEUN\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 1\r\nDer dir deine S�nde vergibt\r\nund heilt deine Gebrechen.\r\n--\r\nDer dich vom Verderben erl�st,\r\ndich kr�nt mit Barmherzigkeit.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 2\r\nDer dich fr�hlich singen l�sst;\r\ndu wirst wieder jung wie ein Adler.\r\n--\r\nEr zeigt dir den richtigen Weg\r\nund schafft dir Gerechtigkeit.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 3\r\nSo hoch, wie der Himmel sich hebt,\r\nl�sst Er Seine Gnade erstrahlen.\r\n--\r\nBarmherzig und gn�dig ist Er\r\nund ist wie ein Vater zu dir.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n";
            string ErgebnisTestsong = "#LangCount=1\r\n#Title=Lobe den Herrn! Sing Ihm dein Lied!\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Author=Werner Arthur Hoffmann\r\n#(c)=1992 Musikverlag Klaus Gerth, Asslar\r\n#Melody=Werner Arthur Hoffmann\r\n#Key=E\r\n#Bible=Psalm 103\r\n#Songbook=Feiert Jesus I / 54\r\n#Chords=MC41LDEsRQ0wLjUsMixFL0cjDTMuNSwzLEENMTguNSwzLEYjNy9BIw05LjUsNCxFL0I9DTE4LDQsQj0NMC41LDYsRQ0wLjUsNyxFL0cjDTAuNSw4LEENMC41LDksQj03DTkuNSw5LEUNNC41LDEyLEENMTQuNSwxMixCPQ0yMy41LDEyLEUNNC41LDEzLEENMTAuNSwxMyxCPTcNMTguNSwxMyxCPQ0yMywxMyxFDTQuNSwxNCxBDTE2LjUsMTQsQj0NMjYsMTQsRQ0tMSwxNSxBDTYuNSwxNSxFDTE5LjUsMTUsQj03DTI1LjUsMTUsRQ0wLjUsMTgsRQ0wLjUsMTksRS9HIw0zLjUsMjAsQQ0xOC41LDIwLEYjNy9BIw05LjUsMjEsRS9CPQ0xOCwyMSxCPQ0wLjUsMjMsRQ0wLjUsMjQsRS9HIw0wLjUsMjUsQQ0wLjUsMjYsQj03DTkuNSwyNixFDTQuNSwyOSxBDTE4LjUsMjksQj0NMjYuNSwyOSxFDTMuNSwzMCxBDTE3LDMwLEI9Nw0yOS41LDMwLEI9DTMyLDMwLEUNMy41LDMxLEENMTcuNSwzMSxCPQ0yNy41LDMxLEUNLTEsMzIsQQ02LjUsMzIsRQ0xOC41LDMyLEI9Nw0yNS41LDMyLEUNMC41LDM1LEUNMC41LDM2LEUvRyMNMy41LDM3LEENMTguNSwzNyxGIzcvQSMNOS41LDM4LEUvQj0NMTgsMzgsQj0NMC41LDQwLEUNMC41LDQxLEUvRyMNMC41LDQyLEENMC41LDQzLEI9Nw05LjUsNDMsRQ0zLjUsNDYsQQ0xNy41LDQ2LEI9DTI5LjUsNDYsRQ02LjUsNDcsQQ0xNS41LDQ3LEI9Nw0yNSw0NyxCPQ0yOSw0NyxFDTQuNSw0OCxBDTE2LjUsNDgsQj0NMjYuNSw0OCxFDS0xLDQ5LEENNC41LDQ5LEUNMTYuNSw0OSxCPTcNMjUuNSw0OSxFDTAuNSw1MixFDTAuNSw1MyxFL0cjDTMuNSw1NCxBDTE4LjUsNTQsRiM3L0EjDTkuNSw1NSxFL0I9DTE4LDU1LEI9DTAuNSw1NyxFDTAuNSw1OCxFL0cjDTAuNSw2MCxBDTAuNSw2MSxCPTcNOS41LDYxLEUN\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 1\r\nDer dir deine S�nde vergibt\r\nund heilt deine Gebrechen.\r\nDer dich vom Verderben erl�st,\r\ndich kr�nt mit Barmherzigkeit.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 2\r\nDer dich fr�hlich singen l�sst;\r\ndu wirst wieder jung wie ein Adler.\r\nEr zeigt dir den richtigen Weg\r\nund schafft dir Gerechtigkeit.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 3\r\nSo hoch, wie der Himmel sich hebt,\r\nl�sst Er Seine Gnade erstrahlen.\r\nBarmherzig und gn�dig ist Er\r\nund ist wie ein Vater zu dir.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n";
            //Act
            Debug.Print("******Der original Songtext\n{0}\n******Ende vom original Songtext\n", Testsong);
            Song testVMSongbeamerFormat = new Song(Testsong);
            string test = testVMSongbeamerFormat.Vorspann + testVMSongbeamerFormat.VerseList.ToString();
            Debug.Print("******Textausgabe aus Objekten:\n{0}{1}******Ende vom objekterzeugten Songtext\n", testVMSongbeamerFormat.Vorspann, testVMSongbeamerFormat.VerseList.ToString());
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Vorspann + testVMSongbeamerFormat.VerseList.ToString() == ErgebnisTestsong);
        }
    }
}
