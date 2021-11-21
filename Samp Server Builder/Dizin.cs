using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp_Server_Builder
{
    public static class Dizin
    {
        // (C:\gta_sa.exe -> C:\)
        public static string Konum(string dizin)
        {
            int pos = dizin.ToString().LastIndexOf('\\');
            return dizin.Substring(0, pos + 1);
        }

        // (C:\gta_sa.exe -> gta_sa.exe)
        public static string Konum_Dosya_Adi(string dizin)
        {
            int pos = dizin.ToString().LastIndexOf('\\');
            return dizin.ToString().Remove(0, pos + 1);
        }
    }
}
