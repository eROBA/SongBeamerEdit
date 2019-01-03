using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongBeamerEdit.ViewModel;
using System.Diagnostics;
using SongBeamerEdit.Properties;

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
    }
}