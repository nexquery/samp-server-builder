using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Samp_Server_Builder.GUI
{
    public partial class Main : Form
    {
        // Mause Hareketlerini Algıla
        private int mX, mY;
        private bool mHareket;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Focus sorununu düzelt
            label9.Select();

            // Index değerlerini (textbox)
            textBox1.TabIndex = 0;
            textBox2.TabIndex = 1;

            // Kütüphane listesini yükle
            Kutuphaneler.Yukle();

            // Eklenti listesini yükle
            Eklentiler.Yukle();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            mHareket = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mX = e.X;
            mY = e.Y;
            mHareket = true;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mHareket)
            {
                SetDesktopLocation(Control.MousePosition.X - mX, Control.MousePosition.Y - mY);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Builder uygulamasının konumu
            string bPath = $@"{Application.StartupPath}\Library";

            // Dosyaların kopyalanacağı klasör
            string ePath = string.Empty;

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                Log("Sunucu adını belirlemediniz.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                Log("Oyun modunun (.pwn dosyasının) adını belirlemediniz.");
                return;
            }

            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                Log("Platformu belirlemediniz.");
                return;
            }

            if (comboBox1.Text == string.Empty)
            {
                Log("Sunucu versionunu belirlemediniz.");
                return;
            }

            Log("Sunucu dosyalarının hazırlanacağı klasörü seçin.");

            FolderBrowserDialog klasor = new FolderBrowserDialog()
            {
                Description = "Sunucu dosyalarının hazırlanacağı klasörü seçin.",
                ShowNewFolderButton = true
            };

            if (klasor.ShowDialog() != DialogResult.OK)
            {
                Log("Sunucu dosyalarının kayıt edileceği klasörü seçmediniz.");
                return;
            }

            Log("Sunucu dosyaları hazırlanıyor...");
            ePath = $@"{klasor.SelectedPath}";

            // Sunucu dosyasını kopyala (Windows 037)
            if (radioButton1.Checked == true && comboBox1.Text == "0.3.7-R2")
            {
                Kopyala.Copy($@"{bPath}\version\037\windows\", ePath + @"\");
            }

            // Sunucu dosyasını kopyala (Linux 037)
            if (radioButton2.Checked == true && comboBox1.Text == "0.3.7-R2")
            {
                Kopyala.Copy($@"{bPath}\version\037\linux\", ePath + @"\");
            }

            // Sunucu dosyasını kopyala (Windows DL)
            if (radioButton1.Checked == true && comboBox1.Text == "0.3 DL")
            {
                Kopyala.Copy($@"{bPath}\version\DL\windows\", ePath + @"\");
            }

            // Sunucu dosyasını kopyala (Linux DL)
            if (radioButton2.Checked == true && comboBox1.Text == "0.3 DL")
            {
                Kopyala.Copy($@"{bPath}\version\DL\linux\", ePath + @"\");
            }

            // (.vscode)
            Log("(.vscode) yapılandırması kontrol ediliyor.");
            if (checkBox1.Checked == true)
            {
                // Yapılandırma dosyası varsa sil (.vscode)
                if (Directory.Exists(ePath + @"\.vscode"))
                {
                    Directory.Delete(ePath + @"\.vscode", true);
                    Thread.Sleep(150);
                }

                // Yapılandırma klasörünü oluştur
                Directory.CreateDirectory(ePath + @"\.vscode");
                Thread.Sleep(150);

                // task.json kopyala
                File.Copy(bPath + @"\.vscode\tasks.json", ePath + @"\.vscode\tasks.json");
                Thread.Sleep(150);
            }

            // Pawn Compiler
            Log("Pawn Compiler kontrol ediliyor.");
            if (checkBox3.Checked == true)
            {
                string[] compiler = { "pawnc.dll", "pawncc.exe", "pawndisasm.exe" };
                foreach (string p in compiler)
                {
                    if (File.Exists($@"{ePath}\pawno\{p}"))
                    {
                        File.Delete($@"{ePath}\pawno\{p}");
                        Thread.Sleep(150);

                        File.Copy($@"{bPath}\compiler\{p}", $@"{ePath}\pawno\{p}");
                        Thread.Sleep(10);
                    }
                }
            }

            // Kütüphaneleri yükle
            Log("Kütüphane (Includes) dosyaları hazırlanıyor...");
            foreach (ListViewItem p in listView1.Items)
            {
                if (p.Checked == true && (p.Text == "samp-stdlib" || p.Text == "pawn-stdlib"))
                {
                    FileInfo[] dosyalar = new DirectoryInfo(Kutuphaneler.Getir(p.Text, 1)).GetFiles("*.inc");
                    foreach (FileInfo j in dosyalar)
                    {
                        if (File.Exists($@"{ePath}\pawno\include\{j.Name}"))
                        {
                            File.Delete($@"{ePath}\pawno\include\{j.Name}");
                            Thread.Sleep(50);

                            File.Copy(j.FullName, $@"{ePath}\pawno\include\{j.Name}");
                            Thread.Sleep(50);
                        }
                    }
                }
                else if (p.Checked == true && p.Text == "YSI v5.05.0403")
                {
                    /*
                    if (Directory.Exists($@"{ePath}\pawno\include\YSI\"))
                    {
                        Directory.Delete($@"{ePath}\pawno\include\YSI\", true);
                        Thread.Sleep(100);
                    }
                    Directory.CreateDirectory($@"{ePath}\pawno\include\YSI\");
                    Thread.Sleep(10);
                    Kopyala.Copy(Kutuphaneler.Getir(p.Text, 1), $@"{ePath}\pawno\include\YSI\");
                    */
                    Kopyala.Copy(Kutuphaneler.Getir(p.Text, 1), $@"{ePath}\pawno\include\");
                }
                else if (p.Checked == true)
                {
                    string dosya = Kutuphaneler.Getir(p.Text, 2);
                    if (File.Exists($@"{ePath}\pawno\include\{dosya}"))
                    {
                        File.Delete($@"{ePath}\pawno\include\{dosya}");
                        Thread.Sleep(100);
                    }
                    File.Copy(Kutuphaneler.Getir(p.Text, 1), $@"{ePath}\pawno\include\{dosya}");
                }
            }

            // Eklentileri yükle
            Log("Eklentiler (Plugins) dosyaları hazırlanıyor...");
            foreach (ListViewItem p in listView2.Items)
            {
                if (p.Checked == true)
                {
                    foreach (Eklentiler.Data j in Eklentiler.Eklenti)
                    {
                        if (j.isim == p.Text)
                        {
                            // Kütüphaneyi yükle
                            File.Copy(j.klasor + j.include, $@"{ePath}\pawno\include\{j.include}");

                            // Eklentiyi yükle (windows)
                            if (radioButton1.Checked == true)
                            {
                                // .dll
                                File.Copy(j.klasor + j.dosya_win32, $@"{ePath}\plugins\{j.dosya_win32}");

                                // Ana dizine atılacak dosya var mı ?
                                if (j.ana_dizin_win32 != string.Empty)
                                {
                                    string[] ana_dosyalar = j.ana_dizin_win32.Split('|');
                                    foreach (string it in ana_dosyalar)
                                    {
                                        File.Copy(j.klasor + it, $@"{ePath}\{it}");
                                    }
                                }
                            }
                            else
                            {
                                // .so
                                File.Copy(j.klasor + j.dosya_linux, $@"{ePath}\plugins\{j.dosya_linux}");

                                // Ana dizine atılacak dosya var mı ?
                                if (j.ana_dizin_linux != string.Empty)
                                {
                                    string[] ana_dosyalar = j.ana_dizin_linux.Split('|');
                                    foreach (string it in ana_dosyalar)
                                    {
                                        File.Copy(j.klasor + it, $@"{ePath}\{it}");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Server cfg oluştur
            Log("server.cfg dosyası hazırlanıyor...");
            FileStream fs = new FileStream($@"{ePath}\server.cfg", FileMode.Append, FileAccess.Write, FileShare.Write);
            using (StreamWriter w = new StreamWriter(fs, new UTF8Encoding()))
            {
                int pos = (comboBox1.Text == "0.3 DL") ? (Cfg.e_037_dl.Length) : (Cfg.e_037_dl.Length - 2);
                for (int i = 0; i < pos; i++)
                {
                    string sz = Cfg.e_037_dl[i];

                    if (sz.Contains("hostname"))
                    {
                        w.Write($"hostname {textBox1.Text}\n");
                    }
                    else if (sz.Contains("gamemode0"))
                    {
                        w.Write($"gamemode0 {textBox2.Text.Replace(".pwn", "")} 1\n");
                    }
                    else if (sz.Contains("plugins"))
                    {
                        w.Write("plugins ");
                        foreach (ListViewItem p in listView2.Items)
                        {
                            if (p.Checked == true)
                            {
                                foreach (Eklentiler.Data j in Eklentiler.Eklenti)
                                {
                                    if (j.isim == p.Text)
                                    {
                                        // Windows
                                        if (radioButton1.Checked == true)
                                        {
                                            w.Write(j.dosya_win32.Replace(".dll", "") + " ");
                                        }
                                        else
                                        {
                                            w.Write(j.dosya_linux + " ");
                                        }
                                    }
                                }
                            }
                        }
                        w.Write("\n");
                    }
                    else
                    {
                        w.Write(sz + "\n");
                    }
                }
                if (checkBox2.Checked)
                {
                    w.Write("long_call_time 0\n");
                }
                w.Close();
            }
            fs.Close();

            // Oyun modunu oluştur
            Log("Oyun modu (.pwn) dosyası hazırlanıyor...");
            FileStream jx = new FileStream($@"{ePath}\gamemodes\{textBox2.Text.Replace(".pwn", "")}.pwn", FileMode.Append, FileAccess.Write, FileShare.Write);
            using (StreamWriter w = new StreamWriter(jx, Encoding.GetEncoding("windows-1254")))
            {
                w.Write("#include\t<a_samp>\n");
                foreach (ListViewItem p in listView2.Items)
                {
                    if (p.Checked == true)
                    {
                        foreach (Eklentiler.Data j in Eklentiler.Eklenti)
                        {
                            if (j.isim == p.Text)
                            {
                                w.Write($"#include\t<{j.include.Replace(".inc", "")}>\n");
                            }
                        }
                    }
                }
                w.Write("\n");

                foreach (ListViewItem p in listView1.Items)
                {
                    if (p.Checked == true && (p.Text == "samp-stdlib" || p.Text == "pawn-stdlib"))
                    {
                        continue;
                    }
                    else if (p.Checked == true && p.Text == "YSI v5.05.0403")
                    {
                        w.Write("\n");
                        w.Write($"#include\t{@"<YSI_Coding\y_va>"}\n");
                        w.Write($"#include\t{@"<YSI_Coding\y_hooks>"}\n");
                        w.Write($"#include\t{@"<YSI_Data\y_iterate>"}\n");
                        w.Write($"#include\t{@"<YSI_Visual\y_dialog>"}\n");
                    }
                    else if (p.Checked == true)
                    {
                        string dosya = Kutuphaneler.Getir(p.Text, 2).Replace(".inc", "");
                        w.Write($"#include\t<{dosya}>\n");
                    }
                }
                w.Write("\nmain()\n{\n\n}");
                w.Close();
            }
            jx.Close();

            Log("Sunucu dosyası hazırlandı.");

            Process.Start(ePath);
        }


        private void Log(string yazi)
        {
            label9.Text = yazi;
            label9.Update();
        }
    }
}
