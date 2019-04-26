using System;

namespace SongBeamerEdit.FlagsValueConverter
{
    [Flags]
    public enum Language : int
    {
        None  = 0,      //0000
        Lang0 = 1,      //0001
        Lang1 = 2,      //0010
        Lang2 = 4,      //0100
        Lang3 = 8,      //1000
    }
}
