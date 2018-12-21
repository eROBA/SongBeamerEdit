using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongBeamerEditTests.Model
{
    public class TestsongDummyCreator
    {
        public TestsongDummyCreator() { }
        public string GenerateTestSong(int anzahlVerse, int anzahlVerszeilen, int anzahlSprachen, int maxZeilenProPage)
        {
            if (maxZeilenProPage < anzahlSprachen) maxZeilenProPage = anzahlSprachen;
            string vorspann = "#LangCount=" + anzahlSprachen + "\r\n#Title=Titel\r\n#TitleLang2=Titel zweite Sprache\r\n#OTitle=Originaltitel\r\n#Author=Autor\r\n#(c)=Copyright\r\n#NatCopyright=Nationale Rechte\r\n#Melody=Melodie\r\n#Bible=Römer 8,14-16\r\n#Editor=SongBeamer 2.42f\r\n#Version=3\r\n#Format=F/K//\r\n#TitleFormat=U\r\n#Songbook=Du bist Herr IV/001\r\n";
            string testSong = vorspann;
            for (int i = 1; i < anzahlVerse + 1; i++)
            {
                testSong += "---" + "\r\n" + "Vers" + (i) + "\r\n";
                int spracheNr = 0;
                int iii = 0;
                for (int ii = 1; ii < anzahlVerszeilen + 1; ii++)
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
    }
}
