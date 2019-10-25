using System;

namespace MojeProgramy
{
    [Serializable()]
    public class Program
    {

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>  Jezeli Install== true, to uzytkownik wybral ten program do instalacji </summary>
        ///
        public bool Install { get; set; } = false;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Nazwa programu </summary>
        ///

        public string Name { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Wersja programu z linku </summary>
        ///

        public string Version { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>  Bezposredni adres do pobrania programu </summary>
        ///

        public string Link { get; set; }
    }
}
