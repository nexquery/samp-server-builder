using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp_Server_Builder
{
    public static class Kopyala
    {
        public static void Copy(string kaynakDizin, string hedefDizin)
        {
            var diKaynak = new DirectoryInfo(kaynakDizin);
            var diHedef = new DirectoryInfo(hedefDizin);
            CopyAll(diKaynak, diHedef);
        }

        public static void CopyAll(DirectoryInfo kaynak, DirectoryInfo hedef)
        {
            Directory.CreateDirectory(hedef.FullName);

            foreach (FileInfo fi in kaynak.GetFiles())
            {
                fi.CopyTo(Path.Combine(hedef.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diKaynakAltDizin in kaynak.GetDirectories())
            {
                DirectoryInfo sonrakiHedefAltDizin = hedef.CreateSubdirectory(diKaynakAltDizin.Name);
                CopyAll(diKaynakAltDizin, sonrakiHedefAltDizin);
            }
        }
    }
}
