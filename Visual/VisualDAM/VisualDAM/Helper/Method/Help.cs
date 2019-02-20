using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;
//using AccessMatrixClient.API;
using DAM.Model;


namespace Helper
{
    static class Help
    {
        public static DAM.Model.System ParsedSystem { get; set; }

        public static string[,] AccessMatrix()
        {
            int ucount = ParsedSystem.Users.Count,
                ocount = ParsedSystem.Objects.Count,
                i = 1;
            string[,] m = new string[ucount + 1, ocount + 1];
            foreach (DAM.Model.Object o in ParsedSystem.Objects)
            {
                m[0, i] = o.ID.ToString();
                i++;
            }
            i = 1;
            foreach (DAM.Model.User u in ParsedSystem.Users)
            {
                m[i, 0] = u.Name;
                for (int j = 1; j < ocount + 1; j++)
                {
                    string tmp = string.Concat(u.Params.Where(f => f.ID.ToString() == m[0, j]).Select(f => f.Value).Distinct());
                    m[i, j] = string.IsNullOrEmpty(tmp) ? "-" : tmp;
                }
                i++;
            }
            return m;
        }

        public static void SendToParse(string input)
        {
            ParsedSystem = DAM.Method.SystemMethod.Parse(input);
        }

        public static string FormatInput(string _input)
        {
            string input = StringFormatter(_input);
            foreach (char c in DAM.Dict.CommonDict.NewLineAfterSeparators)
            {
                input = input.Replace(c.ToString(), c.ToString() + Environment.NewLine);
            }
            foreach (char c in DAM.Dict.CommonDict.TabAfterSeparators)
            {
                input = input.Replace(c.ToString(), c.ToString() + "\n   ");
            }
            foreach (char c in DAM.Dict.CommonDict.Separators)
            {
                input = input.Replace(c.ToString(), c.ToString() + " ");
            }
            return input;
        }

        public static string StringFormatter(string input)
        {
            return Regex.Replace(input, @"\s*", string.Empty);
        }

        public static string ShowOutput(DAM.Model.System system)
        {
            return JsonConvert.SerializeObject(system, Formatting.Indented);// вывод
        }

        public static string ShowParseError(DACException ex)
        {
            return $"Встречена ошибка в тексте! {ex.message}";
        }

        public static string ShowError(Exception ex)
        {
            return $"Произошла непредвиденная ошибка!\n{ex.Message}\nStackTrace:\n{ex.StackTrace}";
        }
    }
}
