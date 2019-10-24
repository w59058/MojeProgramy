using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojeProgramy
{
    [Serializable()]
    public class Program
    {
        public bool Install { get; set; } = false;
        public string Name { get; set; }
        public string Version { get; set; }
        public string Link { get; set; }
    }
}
