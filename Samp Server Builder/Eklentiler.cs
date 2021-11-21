using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samp_Server_Builder
{
    public static class Eklentiler
    {
        public struct Data
        {
            public string isim;
            public string klasor;
            public string include;

            public string dosya_win32;
            public string ana_dizin_win32;

            public string dosya_linux;
            public string ana_dizin_linux;
        };

        public static List<Data> Eklenti = new List<Data>();

        private static void Eklenti_List()
        {
            // Kısa dizin yolu
            string p = Application.StartupPath + @"\Library\plugins";

            // CrashDetect 4.20
            Eklenti.Add(new Data 
            {
                isim            = "CrashDetect 4.20",
                klasor          = $@"{p}\crashdetect-4.20\",
                include         = "crashdetect.inc",

                dosya_win32     = "crashdetect.dll",
                ana_dizin_win32 = string.Empty,

                dosya_linux     = "crashdetect.so",
                ana_dizin_linux = string.Empty
            });;

            // MySQL R41-4
            Eklenti.Add(new Data
            {
                isim            = "MySQL R41-4",
                klasor          = $@"{p}\mysql-R41-4\",
                include         = "a_mysql.inc",

                dosya_win32     = "mysql.dll",
                ana_dizin_win32 = "libmariadb.dll|log-core.dll",

                dosya_linux     = "mysql.so",
                ana_dizin_linux = "log-core.so"
            });

            // Pawn.CMD 3.3.6
            Eklenti.Add(new Data
            {
                isim            = "Pawn.CMD 3.3.6",
                klasor          = $@"{p}\pawncmd-3.3.6\",
                include         = "Pawn.CMD.inc",

                dosya_win32     = "pawncmd.dll",
                ana_dizin_win32 = string.Empty,

                dosya_linux     = "pawncmd.so",
                ana_dizin_linux = string.Empty
            });

            // Pawn.RakNet 1.5.1
            Eklenti.Add(new Data
            {
                isim = "Pawn.RakNet 1.5.1",
                klasor = $@"{p}\pawn.raknet-1.5.1\",
                include = "Pawn.RakNet.inc",

                dosya_win32 = "pawnraknet.dll",
                ana_dizin_win32 = string.Empty,

                dosya_linux = "pawnraknet.so",
                ana_dizin_linux = string.Empty
            });

            // precise-timers 2.0.2
            Eklenti.Add(new Data
            {
                isim = "precise-timers 2.0.2",
                klasor = $@"{p}\precise-timers-2.0.2\",
                include = "samp-precise-timers.inc",

                dosya_win32 = "samp-precise-timers.dll",
                ana_dizin_win32 = string.Empty,

                dosya_linux = "samp-precise-timers.so",
                ana_dizin_linux = string.Empty
            });

            // SKY 2.3.1
            Eklenti.Add(new Data
            {
                isim = "SKY 2.3.1",
                klasor = $@"{p}\SKY-2.3.1\",
                include = "SKY.inc",

                dosya_win32 = "SKY.dll",
                ana_dizin_win32 = string.Empty,

                dosya_linux = "SKY.so",
                ana_dizin_linux = string.Empty
            });

            // sscanf 2.8.3
            Eklenti.Add(new Data
            {
                isim = "sscanf 2.8.3",
                klasor = $@"{p}\sscanf-2.8.3\",
                include = "sscanf2.inc",

                dosya_win32 = "sscanf.dll",
                ana_dizin_win32 = string.Empty,

                dosya_linux = "sscanf.so",
                ana_dizin_linux = string.Empty
            });

            // streamer v2.9.5
            Eklenti.Add(new Data
            {
                isim = "streamer v2.9.5",
                klasor = $@"{p}\streamer-2.9.5\",
                include = "streamer.inc",

                dosya_win32 = "streamer.dll",
                ana_dizin_win32 = string.Empty,

                dosya_linux = "streamer.so",
                ana_dizin_linux = string.Empty
            });

            // textdraw-streamer v1.6.2
            Eklenti.Add(new Data
            {
                isim = "textdraw-streamer v1.6.2",
                klasor = $@"{p}\textdraw.streamer-1.6.2\",
                include = "textdraw-streamer.inc",

                dosya_win32 = "textdraw-streamer.dll",
                ana_dizin_win32 = string.Empty,

                dosya_linux = "textdraw-streamer.so",
                ana_dizin_linux = string.Empty
            });

            // pawn-chrono 1.1.1
            Eklenti.Add(new Data
            {
                isim = "pawn-chrono 1.1.1",
                klasor = $@"{p}\pawn.chrono-1.1.1\",
                include = "chrono.inc",

                dosya_win32 = "chrono.dll",
                ana_dizin_win32 = string.Empty,

                dosya_linux = "chrono.so",
                ana_dizin_linux = string.Empty
            });
        }

        public static void Yukle()
        {
            // Eklentileri yükle
            Eklenti_List();

            // Verileri listeye aktar
            GUI.Main hook = Application.OpenForms.OfType<GUI.Main>().Single();
            if (hook != null)
            {
                ListView p = (ListView)hook.Controls.Find("listView2", true).First();
                if (p != null)
                {
                    foreach (Data it in Eklenti)
                    {
                        p.Items.Add(new ListViewItem(it.isim));
                    }
                }
            }
        }
    }
}
