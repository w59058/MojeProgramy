using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;

namespace MojeProgramy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int ileApek = 0;
        readonly List<Program> programs = new List<Program>
            {
               /* new Program() { Name = "SumatraPDF", Version = "3.1.2", Link="https://www.sumatrapdfreader.org/dl/SumatraPDF-3.1.2-install.exe" },
                new Program() { Name = "VLC", Version = "3.0.8", Link = "https://get.videolan.org/vlc/3.0.8/win64/vlc-3.0.8-win64.exe"},
                new Program() { Name = "7-zip", Version = "19.0", Link="https://www.7-zip.org/a/7z1900.exe"},
                new Program() { Name="Notepad++", Version="7.8", Link="http://download.notepad-plus-plus.org/repository/7.x/7.8/npp.7.8.Installer.exe" },
                new Program() { Name = "WinSCP", Version = "5.15", Link="https://winscp.net/download/WinSCP-5.15.5-Setup.exe"},
             
            */ new Program() { Name="f.lux", Version="4.111", Link="https://justgetflux.com/flux-setup.exe" }
            };

        public static string gdzie = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public readonly string _serializationFile = Path.Combine(gdzie, "programy.lista");

        public MainWindow()
        {
            InitializeComponent();
            ProgramList.ItemsSource = programs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {

                IList lista = ProgramList.Items;
                foreach (Program itemProgram in lista)
                {
                    if (itemProgram.Install)
                    {
                        Pobierz(itemProgram.Link, dialog);
                        ileApek++;
                    }
                    this.WindowState = WindowState.Minimized;
                    this.ShowInTaskbar = false;
                }
                System.Windows.MessageBox.Show("Pobieranie bedzie dzialac w tle.\nDostaniesz powiadomienie o ukonczeniu pobierania!");
            }
        }

        private void Pobierz(string link, System.Windows.Forms.FolderBrowserDialog dialog = null)
        {
            WebClient myWebClient = new WebClient();
            string url = link ?? throw new ArgumentNullException("itemProgram.Link");
            string fileName = System.IO.Path.GetFileName(url);
            var uri = new Uri(url);
            if (dialog == null) {

                myWebClient.DownloadFileAsync(uri, gdzie + "\\" + fileName);
                myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadFinishedList);
            }
            else
            {
                myWebClient.DownloadFileAsync(uri, dialog.SelectedPath + "\\" + fileName);
                myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadFinished);
            }
        }

        private void downloadFinishedList(object sender, AsyncCompletedEventArgs e)
        {
            Wczytaj();
        }

        // jezeli chcesz zapisac liste do pliku
        private void Zapisz()
        {
            using (Stream stream = File.Open(_serializationFile, FileMode.Create))

            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, ProgramList.ItemsSource);
            }
        }
        public void Wczytaj()
        {
            System.Threading.Thread.Sleep(2000);

            if (File.Exists(_serializationFile))
            {

                using (Stream stream = File.Open(_serializationFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    ProgramList.ItemsSource = (IEnumerable)bformatter.Deserialize(stream);
                }
            }
        }

        public void downloadFinished(object sender, AsyncCompletedEventArgs e)
        {
            ileApek--;
            if (ileApek < 1)
            {

                System.Windows.MessageBox.Show("Pobrano wybrane aplikacje!");
                System.Environment.Exit(0);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            InstallButton.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pobierz("https://github.com/w59058/MojeProgramy/raw/master/programy.lista");
        }

    }
}

