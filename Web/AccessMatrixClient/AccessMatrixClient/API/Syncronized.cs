using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessMatrixClient.API
{
    static class Syncronized
    {
        public static bool IsSynced { get; set; }
        public static string CorrectExample { get; set; }
        public static string InCorrectExample { get; set; }
        public static string Separators { get; set; }
        public static string NLSeparators { get; set; }
        public static string TabSeparators { get; set; }

    }
}
