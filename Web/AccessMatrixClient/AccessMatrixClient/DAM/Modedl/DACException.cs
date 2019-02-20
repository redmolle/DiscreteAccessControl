using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAM.Model
{
    class DACException : Exception
    {
        public int wrongWordAt { get; set; }
        public int? wrongLetterAt { get; set; }
        public string sentence { get; set; }
        public string terminal { get; set; }
        public string message { get; set; }
    }           
}