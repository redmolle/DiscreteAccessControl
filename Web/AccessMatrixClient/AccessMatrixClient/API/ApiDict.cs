using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessMatrixClient.API
{
    static class APIDict
    {
        public static string ServerIP { get; set; }
        public static string ServerPort { get; set; }
        public static string ServerUrl { get { return $"http://{ServerIP}:{ServerPort}/api/"; } }
        public static string SystemControllerUrl { get { return $"{ServerUrl}System/"; } }

        public static string ContentTypeJson { get { return "application/json"; } }

       }
}
