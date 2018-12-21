using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SongBeamerEdit.Model;
using SongBeamerEdit.FlagsValueConverter;

namespace SongBeamerEditUnitTests.Model
{
    [TestClass]
    public class LineTests
    {
        [TestMethod]
        public void LineExplicit()
        {
            //Arrange
            string vers = "##2 Das ist eine Zeile mit explizierter Sprachkennzeichnung in der zweiten Sprache";
            //Act
            Line Testline =  new Line(vers);
            Debug.Print("Die Sprache hat die Nummer: {0}.", Testline.BitwiseLanguageNr.ToString());
            Debug.Print("Der Text der Zeile lautet:\r\n{0}", Testline.LineText);
            //Assert
            Assert.IsTrue(Testline.BitwiseLanguageNr == Language.Lang1 & Testline.LineText == "Das ist eine Zeile mit explizierter Sprachkennzeichnung in der zweiten Sprache");
        }
        [TestMethod]
        public void LineImplicit()
        {
            //Arrange
            string vers = "Das ist eine Zeile mit impliziter Sprachkennzeichnung d.h. erste Sprache";
            //Act
            Line Testline = new Line(vers);
            Debug.Print("Die Sprache hat die Nummer: {0}.", Testline.BitwiseLanguageNr.ToString());
            Debug.Print("Der Text der Zeile lautet:\r\n{0}", Testline.LineText);
            //Assert
            Assert.IsTrue(Testline.BitwiseLanguageNr == Language.None & Testline.LineText == "Das ist eine Zeile mit impliziter Sprachkennzeichnung d.h. erste Sprache");
        }
        [TestMethod]
        public void LineEmpty()
        {
            //Arrange
            string vers = "";
            //Act
            Line Testline = new Line(vers);
            Debug.Print("Die Sprache hat die Nummer: {0}.", Testline.BitwiseLanguageNr.ToString());
            Debug.Print("Der Text der Zeile lautet: {0}", Testline.LineText);
            //Assert
            Assert.IsTrue(Testline.BitwiseLanguageNr == Language.None & Testline.LineText == "");
        }
    }
}
