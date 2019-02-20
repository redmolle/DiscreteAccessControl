using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAM.Model
{
    class ForException
    {
        public int wrongWordAt { get; set; }

        public int wrongWordLength { get; set; }
        public int wrongLetterAt { get; set; }
        public char letter { get; set; }
        public string found { get; set; }
        public string sentence { get; set; }
        public string sentenceForMessage { get; set; }
        public string previousWord { get; set; }
        public string expectedWord { get; set; }
    }
}
