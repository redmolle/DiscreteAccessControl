using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAM.Model
{
    class DACException : Exception
    {

        public int markFrom{ get; set; }
        public int markTo { get; set; }
        public int wrongWordAt { get; set; }
        public int? wrongWordLenght { get; set; }
        public int wrongLetterAt { get; set; }
        public string sentence { get; set; }
        public string terminal { get; set; }
        public string message { get; set; }
    }           
}