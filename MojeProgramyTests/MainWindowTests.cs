using Microsoft.VisualStudio.TestTools.UnitTesting;
using MojeProgramy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MojeProgramy.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void PobierzListe()
        {
            string gdzie = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            WebClient myWebClient = new WebClient();
            string url = "https://github.com/w59058/MojeProgramy/raw/master/programy.lista";
            string fileName = System.IO.Path.GetFileName(url);
            var uri = new Uri(url);
            string _serializationFile = Path.Combine(gdzie, "programy.lista");
            
            myWebClient.DownloadFileAsync(uri, gdzie + "\\" + fileName);
            

            if (!File.Exists(_serializationFile))
            {
                Assert.Fail("Brak pliku!");
            }
        }

        [TestMethod()]
        public void WczytajTest()
        {
            System.Threading.Thread.Sleep(2000);
            string gdzie = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string _serializationFile = Path.Combine(gdzie, "programy.lista");

            if (File.Exists(_serializationFile))
            {
                using (Stream stream = File.Open(_serializationFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    IEnumerable test = (IEnumerable)bformatter.Deserialize(stream);
                    if (test == null)
                        Assert.Fail("Nie mozna wczytac listy programow!");
                }
            }
        }
    }
}