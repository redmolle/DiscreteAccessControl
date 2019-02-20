using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAM.Dict
{
    static class CommonDict
    {
        public static string SeparateOutput { get { return $"{Environment.NewLine}-------------------------------------------------{Environment.NewLine}"; } }
        public static string Separators { get { return ":/=,;."; } }        
        public static string NewLineAfterSeparators { get { return ".;"; } }
        public static string TabAfterSeparators { get { return ":,"; } }
        public static string SplitPattern { get { return $@"([{CommonDict.Separators}]\s*)"; } }
        public static string EndSystemElement { get { return "."; } }
        public static string EndSystemSubElement { get { return $"[{Separators.Replace(EndSystemElement, string.Empty)}]"; } }
        public static List<string> ObjectParamNameList
        {
            get
            {
                string[] m = { "visible", "size" };
                List<string> l = new List<string>();
                foreach (string s in m)
                {
                    l.Add(s);
                }
                return l;
            }
        }

        public static List<string> UserAccessParamNameList
        {
            get
            {
                string[] m = { "w", "r", "e" };
                List<string> l = new List<string>();
                foreach (string s in m)
                {
                    l.Add(s);
                }
                return l;
            }
        }

        public static List<char> numeric
        {
            get
            {
                string tmp = "0123456789";
                List<char> l = new List<char>();
                foreach (char c in tmp)
                {
                    l.Add(c);
                }
                return l;
            }
        }  //"[0-9]"

        public static List<char> alphabet
        {
            get
            {
                string tmp = "abcdefghijklmnopqrstuvwxyz";
                List<char> l = new List<char>();
                foreach (char c in tmp)
                {
                    l.Add(c);
                }
                return l;
            }
        }  //"[a-zA-Zа-яА-Я]"

        public static List<char> alphabetWithNumbers
        {
            get
            {
                string tmp = "abcdefghijklmnopqrstuvwxyz0123456789";
                List<char> l = new List<char>();
                foreach (char c in tmp)
                {
                    l.Add(c);
                }
                return l;
            }
        }   //[a-zA-Zа-яА-Я0-9]

        public static string ListToString(List<string> list, string separator)
        
{
            string s = string.Empty;
            foreach (string l in list)
            {
                s += l + separator;
            }
            return s;
        }

        public static string ListToNumericString(List<string> list)
        {
            string s = string.Empty;
            int i = 1;
            foreach (string l in list)
            {
                s += $"\n{i.ToString()}. {l}";
            }
            return s;
        }
    }
}
