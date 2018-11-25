using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using SongBeamerEdit.Converting;

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
    }
}
