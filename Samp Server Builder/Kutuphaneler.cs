using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Samp_Server_Builder
{
    public static class Kutuphaneler
    {
        public struct Data
        {
            public string isim;
            public string dizin;
        };

        public static List<Data> Kutuphane = new List<Data>();

        private static void Kutuphane_List()
        {
            // Kısa dizin yolu
            string p = Application.StartupPath + @"\Library\includes";

            // samp-stdlib
            Kutuphane.Add(new Data { isim = "samp-stdlib", dizin = $@"{p}\samp-stdlib\" });

            // pawn-stdlib
            Kutuphane.Add(new Data { isim = "pawn-stdlib", dizin = $@"{p}\pawn-stdlib\" });

            // weapon-config
            Kutuphane.Add(new Data { isim = "weapon-config", dizin = $@"{p}\weapon-config.inc" });

            // easy-dialog
            Kutuphane.Add(new Data { isim = "easy-dialog", dizin = $@"{p}\easy-dialog.inc" });

            // oscar-broman / strlib
            Kutuphane.Add(new Data { isim = "oscar-broman / strlib", dizin = $@"{p}\strlib.inc" });

            // foreach (non y_iterate version)
            Kutuphane.Add(new Data { isim = "foreach (non y_iterate version)", dizin = $@"{p}\foreach.inc" });

            // y_va (non YSI version)
            Kutuphane.Add(new Data { isim = "y_va (non YSI version)", dizin = $@"{p}\y_va.inc" });

            // YSI v5.05.0403
            Kutuphane.Add(new Data { isim = "YSI v5.05.0403", dizin = $@"{p}\YSI\" });
        }

        public static string Getir(string isim, int istenen = 1)
        {
            // 1 -> Dosyanın dizinini çeker (C:\foreach.inc)
            // 2 -> Dosyanın adını çeker (C:\foreach.inc -> foreach.inc)
            foreach (Data p in Kutuphane)
            {
                if (p.isim == isim)
                {
                    return (istenen == 1) ? (p.dizin) : (Dizin.Konum_Dosya_Adi(p.dizin));
                }
            }
            return string.Empty;
        }

        public static void Yukle()
        {
            // Listeyi yükle
            Kutuphane_List();

            // Verileri listeye aktar
            GUI.Main hook = Application.OpenForms.OfType<GUI.Main>().Single();
            if(hook != null)
            {
                ListView p = (ListView)hook.Controls.Find("listView1", true).First();
                if(p != null)
                {
                    foreach (Data it in Kutuphane)
                    {
                        p.Items.Add(new ListViewItem(it.isim));
                    }
                }
            }
        }
    }
}
