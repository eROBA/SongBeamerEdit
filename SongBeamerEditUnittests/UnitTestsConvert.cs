using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongBeamerEdit;
using System.Diagnostics;

namespace UnitTests
{
    [TestClass]
    public class UnitTestsConvert
    {
        [TestMethod]
        public void NurEinVersMitVerskennzeichnung()
        {
            //Arrange
            string Testsong1 = "#LangCount=1\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nVers\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            string ErgebnisTestsong1 = "#LangCount=1\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nVers\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            //Act
            Debug.Print("Der original Songtext\n{0}\nDas ist das Ende vom original Songtext", Testsong1);
            ConvertSong testVMSongbeamerFormat = new ConvertSong(Testsong1);
            Debug.Print("{0}", testVMSongbeamerFormat.Ergebnis);
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Ergebnis == ErgebnisTestsong1);
        }
        [TestMethod]
        public void NurEinVersOhneVerskennzeichnung()
        {
            //Arrange
            string Testsong2 = "#LangCount=1\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            string ErgebnisTestsong1 = "#LangCount=1\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            //Act
            Debug.Print("Der original Songtext\n{0}\nDas ist das Ende vom original Songtext", Testsong2);
            ConvertSong testVMSongbeamerFormat = new ConvertSong(Testsong2);
            Debug.Print("{0}", testVMSongbeamerFormat.Ergebnis);
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Ergebnis == ErgebnisTestsong1);
        }
        [TestMethod]
        public void NurEinChorusVerskennzeichnung()
        {
            //Arrange
            string Testsong3 = "#LangCount=1\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nChorus\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            string ErgebnisTestsong1 = "#LangCount=1\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nChorus\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            //Act
            Debug.Print("Der original Songtext\n{0}\nDas ist das Ende vom original Songtext", Testsong3);
            ConvertSong testVMSongbeamerFormat = new ConvertSong(Testsong3);
            Debug.Print("{0}", testVMSongbeamerFormat.Ergebnis);
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Ergebnis == ErgebnisTestsong1);
        }
        [TestMethod]
        public void NurEinVersMitVerskennzeichnungZweiSprachen()
        {
            //Arrange
            string Testsong4 = "#LangCount=2\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nVers\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            string ErgebnisTestsong4 = "#LangCount=2\r\n#Title=Take The Name Of Jesus With You\r\n#(c)=Public Domain\r\n#Transpose=0\r\n#TransposeAccidental=0\r\n#Speed=0\r\n#Editor=SongBeamer 0.85w\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nVers\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\n--\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.\r\nPrecious name, O how sweet!\r\nHope of earth and joy of heaven.";
            //Act
            Debug.Print("Der original Songtext\n{0}\nDas ist das Ende vom original Songtext", Testsong4);
            ConvertSong testVMSongbeamerFormat = new ConvertSong(Testsong4);
            Debug.Print("{0}", testVMSongbeamerFormat.Ergebnis);
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Ergebnis == ErgebnisTestsong4);
        }
        [TestMethod]
        public void MehrereVerseOhneKennzeichnung()
        {
            //Arrange
            string Testsong5 = "#LangCount=1\r\n#Title=Schwabdidu\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nSchwappdidu\r\nGott kennt meinen Namen\r\nSchwappdidu\r\nEr wei� wer ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\nEr liebt mich,\r\nso wie ich bin\r\n---\r\nSchwappdidu\r\nGott kennt meinen Namen\r\nSchwappdidu\r\nEr wei� wer ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\nEr liebt mich,\r\nso wie ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\nSchwappdidu\r\nHalleluja Halleluja Amen\r\n";
            string SollTestsong5 = "#LangCount=1\r\n#Title=Schwabdidu\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nSchwappdidu\r\nGott kennt meinen Namen\r\n--\r\nSchwappdidu\r\nEr wei� wer ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\n--\r\nEr liebt mich,\r\nso wie ich bin\r\n---\r\nSchwappdidu\r\nGott kennt meinen Namen\r\n--\r\nSchwappdidu\r\nEr wei� wer ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\n--\r\nEr liebt mich,\r\nso wie ich bin\r\n---\r\nSchwappdidu\r\nHalleluja Amen\r\n--\r\nSchwappdidu\r\nHalleluja Halleluja Amen";
            //Act
            Debug.Print("Der original Songtext\n{0}\nDas ist das Ende vom original Songtext", Testsong5);
            ConvertSong testVMSongbeamerFormat= new ConvertSong(Testsong5);
            Debug.Print("{0}", testVMSongbeamerFormat.Ergebnis);
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Ergebnis == SollTestsong5);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlSprachen == 1);
            Assert.IsTrue(testVMSongbeamerFormat.MaxAnzahlZeilenProSeite == 2);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlSichererVerswechsel == 0);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlUnsichererVerswechsel == 5);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlVerszahlen == 0);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlVerseOhneVerszahlen == 0, "Die Anzahl der Verse die mit 'Vers' gekennzeichnet sind jedoch keine Verszahlen haben, stimmt nicht");
        }
        [TestMethod]
        public void MitVersenUndRefrain()
        {
            //Arrange
            string Testsong6 = "#LangCount=1\r\n#Title=All heav�n declares - 4\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Songbook=Wiedenester / 004\r\n#Author=Noel Richard\r\n#(c)=(c) 1987 Kingsway�s Thankyou Music\r\n#Melody=Trica Richard\r\n#AddCopyrightInfo=F�r D,A,CH: Projektion J<br>Musikverlag, Asslar<br>www.inAktion.de\r\n---\r\nVers 1\r\nAll heav�n declares\r\nthe glory of the risen Lord.\r\nWho can compare\r\nwith the beauty of the Lord?\r\n---\r\nRefrain\r\nForever He (You) will be\r\nthe Lamb upon the throne.\r\nI gladly bow the knee\r\nand worship Him (You) alone.\r\n---\r\nVers 2\r\nI will proclaim\r\nthe glory of the risen Lord,\r\nwho once was slain\r\nto reconcile man of God.\r\n";
            string SollTestsong6 = "#LangCount=1\r\n#Title=All heav�n declares - 4\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Songbook=Wiedenester / 004\r\n#Author=Noel Richard\r\n#(c)=(c) 1987 Kingsway�s Thankyou Music\r\n#Melody=Trica Richard\r\n#AddCopyrightInfo=F�r D,A,CH: Projektion J<br>Musikverlag, Asslar<br>www.inAktion.de\r\n---\r\nVers 1\r\nAll heav�n declares\r\nthe glory of the risen Lord.\r\n--\r\nWho can compare\r\nwith the beauty of the Lord?\r\n---\r\nRefrain\r\nForever He (You) will be\r\nthe Lamb upon the throne.\r\n--\r\nI gladly bow the knee\r\nand worship Him (You) alone.\r\n---\r\nVers 2\r\nI will proclaim\r\nthe glory of the risen Lord,\r\n--\r\nwho once was slain\r\nto reconcile man of God.";
            //Act
            Debug.Print("Der original Songtext\n{0}\nDas ist das Ende vom original Songtext", Testsong6);
            ConvertSong testVMSongbeamerFormat= new ConvertSong(Testsong6);
            Debug.Print("{0}", testVMSongbeamerFormat.Ergebnis);
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Ergebnis == SollTestsong6);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlSprachen == 1);
            Assert.IsTrue(testVMSongbeamerFormat.MaxAnzahlZeilenProSeite == 2);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlSichererVerswechsel == 3);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlUnsichererVerswechsel == 0);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlVerszahlen == 2);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlVerseOhneVerszahlen == 0, "Die Anzahl der Verse die mit 'Vers' gekennzeichnet sind jedoch keine Verszahlen haben, stimmt nicht");
        }
        [TestMethod]
        public void MitVersenUndRefrains()
        {
            //Arrange
            string Testsong7 = "#LangCount=1\r\n#Title=Vater, wir kommen vor deinen Thron\r\n#Author=Albert Frey\r\n#(c)=H�nssler Verlag, Holzgerlingen\r\n#NatCopyright=2004\r\n#Melody=Albert Frey\r\n#Key=C\r\n#Songbook=Feiert Jesus III / 002\r\n#AddCopyrightInfo=Tempo 113\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nVers 1\r\nVater, wir kommen vor deinen Thron.\r\nDu bist voll Freude, erwartest uns schon.\r\nDu gibst uns Raum und du hast f�r uns Zeit,\r\n�ffnest uns dein Herz weit.\r\n---\r\nRefrain 1\r\nDu bist gro�, du gibst den Sternen ihren Lauf.\r\nDu bist gut und deine Liebe h�rt nie auf.\r\n---\r\nVers 2\r\nDu bist uns heilig, wir lieben dich, Herr.\r\nDein Einfluss wachse bei uns immer mehr.\r\nDein heiliger Name wird so in der Welt\r\nneu wiederhergestellt.\r\n---\r\nRefrain 2\r\nDu bist gro�, du gibst den Sternen ihren Lauf.\r\nDu bist gut und deine Liebe h�rt nie auf. (2 x)\r\nDu bist gro�, du bist gut.\r\n\r\n";
            string SollTestsong7 = "#LangCount=1\r\n#Title=Vater, wir kommen vor deinen Thron\r\n#Author=Albert Frey\r\n#(c)=H�nssler Verlag, Holzgerlingen\r\n#NatCopyright=2004\r\n#Melody=Albert Frey\r\n#Key=C\r\n#Songbook=Feiert Jesus III / 002\r\n#AddCopyrightInfo=Tempo 113\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n---\r\nVers 1\r\nVater, wir kommen vor deinen Thron.\r\nDu bist voll Freude, erwartest uns schon.\r\n--\r\nDu gibst uns Raum und du hast f�r uns Zeit,\r\n�ffnest uns dein Herz weit.\r\n---\r\nRefrain 1\r\nDu bist gro�, du gibst den Sternen ihren Lauf.\r\nDu bist gut und deine Liebe h�rt nie auf.\r\n---\r\nVers 2\r\nDu bist uns heilig, wir lieben dich, Herr.\r\nDein Einfluss wachse bei uns immer mehr.\r\n--\r\nDein heiliger Name wird so in der Welt\r\nneu wiederhergestellt.\r\n---\r\nRefrain 2\r\nDu bist gro�, du gibst den Sternen ihren Lauf.\r\nDu bist gut und deine Liebe h�rt nie auf. (2 x)\r\n--\r\nDu bist gro�, du bist gut.";
            //Act
            Debug.Print("Der original Songtext\n{0}\nDas ist das Ende vom original Songtext", Testsong7);
            ConvertSong testVMSongbeamerFormat= new ConvertSong(Testsong7);
            Debug.Print("{0}", testVMSongbeamerFormat.Ergebnis);
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Ergebnis == SollTestsong7);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlSprachen == 1);
            Assert.IsTrue(testVMSongbeamerFormat.MaxAnzahlZeilenProSeite == 2);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlSichererVerswechsel == 4);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlUnsichererVerswechsel == 0);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlVerszahlen == 2);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlVerseOhneVerszahlen == 0, "Die Anzahl der Verse die mit 'Vers' gekennzeichnet sind jedoch keine Verszahlen haben, stimmt nicht");
        }

        [TestMethod]
        public void RefrainVersNrMehrmals()
        {
            //Arrange
            string Testsong8 = "#LangCount=1\r\n#Title=Lobe den Herrn! Sing Ihm dein Lied!\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Author=Werner Arthur Hoffmann\r\n#(c)=1992 Musikverlag Klaus Gerth, Asslar\r\n#Melody=Werner Arthur Hoffmann\r\n#Key=E\r\n#Bible=Psalm 103\r\n#Songbook=Feiert Jesus I / 54\r\n#Chords=MC41LDEsRQ0wLjUsMixFL0cjDTMuNSwzLEENMTguNSwzLEYjNy9BIw05LjUsNCxFL0I9DTE4LDQsQj0NMC41LDYsRQ0wLjUsNyxFL0cjDTAuNSw4LEENMC41LDksQj03DTkuNSw5LEUNNC41LDEyLEENMTQuNSwxMixCPQ0yMy41LDEyLEUNNC41LDEzLEENMTAuNSwxMyxCPTcNMTguNSwxMyxCPQ0yMywxMyxFDTQuNSwxNCxBDTE2LjUsMTQsQj0NMjYsMTQsRQ0tMSwxNSxBDTYuNSwxNSxFDTE5LjUsMTUsQj03DTI1LjUsMTUsRQ0wLjUsMTgsRQ0wLjUsMTksRS9HIw0zLjUsMjAsQQ0xOC41LDIwLEYjNy9BIw05LjUsMjEsRS9CPQ0xOCwyMSxCPQ0wLjUsMjMsRQ0wLjUsMjQsRS9HIw0wLjUsMjUsQQ0wLjUsMjYsQj03DTkuNSwyNixFDTQuNSwyOSxBDTE4LjUsMjksQj0NMjYuNSwyOSxFDTMuNSwzMCxBDTE3LDMwLEI9Nw0yOS41LDMwLEI9DTMyLDMwLEUNMy41LDMxLEENMTcuNSwzMSxCPQ0yNy41LDMxLEUNLTEsMzIsQQ02LjUsMzIsRQ0xOC41LDMyLEI9Nw0yNS41LDMyLEUNMC41LDM1LEUNMC41LDM2LEUvRyMNMy41LDM3LEENMTguNSwzNyxGIzcvQSMNOS41LDM4LEUvQj0NMTgsMzgsQj0NMC41LDQwLEUNMC41LDQxLEUvRyMNMC41LDQyLEENMC41LDQzLEI9Nw05LjUsNDMsRQ0zLjUsNDYsQQ0xNy41LDQ2LEI9DTI5LjUsNDYsRQ02LjUsNDcsQQ0xNS41LDQ3LEI9Nw0yNSw0NyxCPQ0yOSw0NyxFDTQuNSw0OCxBDTE2LjUsNDgsQj0NMjYuNSw0OCxFDS0xLDQ5LEENNC41LDQ5LEUNMTYuNSw0OSxCPTcNMjUuNSw0OSxFDTAuNSw1MixFDTAuNSw1MyxFL0cjDTMuNSw1NCxBDTE4LjUsNTQsRiM3L0EjDTkuNSw1NSxFL0I9DTE4LDU1LEI9DTAuNSw1NyxFDTAuNSw1OCxFL0cjDTAuNSw2MCxBDTAuNSw2MSxCPTcNOS41LDYxLEUN\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 1\r\nDer dir deine S�nde vergibt\r\nund heilt deine Gebrechen.\r\n--\r\nDer dich vom Verderben erl�st,\r\ndich kr�nt mit Barmherzigkeit.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 2\r\nDer dich fr�hlich singen l�sst;\r\ndu wirst wieder jung wie ein Adler.\r\n--\r\nEr zeigt dir den richtigen Weg\r\nund schafft dir Gerechtigkeit.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 3\r\nSo hoch, wie der Himmel sich hebt,\r\nl�sst Er Seine Gnade erstrahlen.\r\n--\r\nBarmherzig und gn�dig ist Er\r\nund ist wie ein Vater zu dir.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n";
            string SollTestsong8 = "#LangCount=1\r\n#Title=Lobe den Herrn! Sing Ihm dein Lied!\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Author=Werner Arthur Hoffmann\r\n#(c)=1992 Musikverlag Klaus Gerth, Asslar\r\n#Melody=Werner Arthur Hoffmann\r\n#Key=E\r\n#Bible=Psalm 103\r\n#Songbook=Feiert Jesus I / 54\r\n#Chords=MC41LDEsRQ0wLjUsMixFL0cjDTMuNSwzLEENMTguNSwzLEYjNy9BIw05LjUsNCxFL0I9DTE4LDQsQj0NMC41LDYsRQ0wLjUsNyxFL0cjDTAuNSw4LEENMC41LDksQj03DTkuNSw5LEUNNC41LDEyLEENMTQuNSwxMixCPQ0yMy41LDEyLEUNNC41LDEzLEENMTAuNSwxMyxCPTcNMTguNSwxMyxCPQ0yMywxMyxFDTQuNSwxNCxBDTE2LjUsMTQsQj0NMjYsMTQsRQ0tMSwxNSxBDTYuNSwxNSxFDTE5LjUsMTUsQj03DTI1LjUsMTUsRQ0wLjUsMTgsRQ0wLjUsMTksRS9HIw0zLjUsMjAsQQ0xOC41LDIwLEYjNy9BIw05LjUsMjEsRS9CPQ0xOCwyMSxCPQ0wLjUsMjMsRQ0wLjUsMjQsRS9HIw0wLjUsMjUsQQ0wLjUsMjYsQj03DTkuNSwyNixFDTQuNSwyOSxBDTE4LjUsMjksQj0NMjYuNSwyOSxFDTMuNSwzMCxBDTE3LDMwLEI9Nw0yOS41LDMwLEI9DTMyLDMwLEUNMy41LDMxLEENMTcuNSwzMSxCPQ0yNy41LDMxLEUNLTEsMzIsQQ02LjUsMzIsRQ0xOC41LDMyLEI9Nw0yNS41LDMyLEUNMC41LDM1LEUNMC41LDM2LEUvRyMNMy41LDM3LEENMTguNSwzNyxGIzcvQSMNOS41LDM4LEUvQj0NMTgsMzgsQj0NMC41LDQwLEUNMC41LDQxLEUvRyMNMC41LDQyLEENMC41LDQzLEI9Nw05LjUsNDMsRQ0zLjUsNDYsQQ0xNy41LDQ2LEI9DTI5LjUsNDYsRQ02LjUsNDcsQQ0xNS41LDQ3LEI9Nw0yNSw0NyxCPQ0yOSw0NyxFDTQuNSw0OCxBDTE2LjUsNDgsQj0NMjYuNSw0OCxFDS0xLDQ5LEENNC41LDQ5LEUNMTYuNSw0OSxCPTcNMjUuNSw0OSxFDTAuNSw1MixFDTAuNSw1MyxFL0cjDTMuNSw1NCxBDTE4LjUsNTQsRiM3L0EjDTkuNSw1NSxFL0I9DTE4LDU1LEI9DTAuNSw1NyxFDTAuNSw1OCxFL0cjDTAuNSw2MCxBDTAuNSw2MSxCPTcNOS41LDYxLEUN\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 1\r\nDer dir deine S�nde vergibt\r\nund heilt deine Gebrechen.\r\n--\r\nDer dich vom Verderben erl�st,\r\ndich kr�nt mit Barmherzigkeit.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 2\r\nDer dich fr�hlich singen l�sst;\r\ndu wirst wieder jung wie ein Adler.\r\n--\r\nEr zeigt dir den richtigen Weg\r\nund schafft dir Gerechtigkeit.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.\r\n---\r\nVers 3\r\nSo hoch, wie der Himmel sich hebt,\r\nl�sst Er Seine Gnade erstrahlen.\r\n--\r\nBarmherzig und gn�dig ist Er\r\nund ist wie ein Vater zu dir.\r\n---\r\nRefrain\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nVergiss nicht, Er hat\r\nDir viel Gutes getan.\r\n---\r\nLobe den Herrn!\r\nSing Ihm dein Lied!\r\n--\r\nEr ist der K�nig,\r\nbete Ihn an.";
            //Act
            Debug.Print("Der original Songtext\n{0}\nDas ist das Ende vom original Songtext", Testsong8);
            ConvertSong testVMSongbeamerFormat= new ConvertSong(Testsong8);
            Debug.Print("{0}", testVMSongbeamerFormat.Ergebnis);
            //Assert
            Assert.IsTrue(testVMSongbeamerFormat.Ergebnis == SollTestsong8);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlSprachen == 1);
            Assert.IsTrue(testVMSongbeamerFormat.MaxAnzahlZeilenProSeite == 2);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlSichererVerswechsel == 7);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlUnsichererVerswechsel == 4);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlVerszahlen == 3);
            Assert.IsTrue(testVMSongbeamerFormat.AnzahlVerseOhneVerszahlen == 0, "Die Anzahl der Verse die mit 'Vers' gekennzeichnet sind jedoch keine Verszahlen haben, stimmt nicht");
        }
    }
}
