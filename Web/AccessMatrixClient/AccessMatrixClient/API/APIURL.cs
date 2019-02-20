using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessMatrixClient.API
{
    class APIURL
    {
        public static string ParseSystem { get { return $"{API.APIDict.SystemControllerUrl}Parse"; } }
        public static string GetSeparators { get { return $"{API.APIDict.SystemControllerUrl}Separators"; } }
        public static string GetNLSeparators { get { return $"{API.APIDict.SystemControllerUrl}NLSeparators"; } }
        public static string GetTabSeparators { get { return $"{API.APIDict.SystemControllerUrl}TabSeparators"; } }
        public static string GetAccessMatrix { get { return $"{API.APIDict.SystemControllerUrl}AccessMatrix"; } }
        public static string GetCorrectExample { get { return $"{API.APIDict.SystemControllerUrl}CorrectExample"; } }
        public static string GetInCorrectExample { get { return $"{API.APIDict.SystemControllerUrl}InCorrectExample"; } }
        public static string SaveSystem { get { return $"{API.APIDict.SystemControllerUrl}SaveSystem"; } }
        public static string ForConnect { get { return $"{API.APIDict.SystemControllerUrl}"; } }

    }
}
