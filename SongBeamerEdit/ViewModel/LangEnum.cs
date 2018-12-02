using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongBeamerEdit
{
    [Flags]
    public enum Language : ushort
    {
        Lang0 = 1,      //1
        Lang1 = 2,      //2
        Lang2 = 4,      //3
        Lang3 = 8,      //4
    }
}
