using System;
using System.Collections.Generic;
using System.Linq;

namespace DAM.Method
{
    static class SystemMethod
    {
        public static DAM.Model.System Parse(string input)
        {
            return Parser.Run(input);
        }

        public static string[,] GetAccessMatrix(DAM.Model.System system)
        {
            Dictionary<Tuple<string, int>, string> AccessMatrix = new Dictionary<Tuple<string, int>, string>();
            string[,] matrix = new string[system.Users.Count + 1, system.Objects.Count + 1];
            int i = 1;
            foreach (int c in system.Objects.Select(f => f.ID).Distinct())
            {
                matrix[0, i] = c.ToString();
                i++;
            }
            i = 1;
            foreach (string c in system.Users.Select(f => f.Name).Distinct())
            {
                matrix[i, 0] = c;
                i++;
            }
            i = 1;
            foreach (DAM.Model.User u in system.Users)
            {
                matrix[i, 0] = u.Name;

                for (int j = 1; j < system.Objects.Count; j++)
                {
                    string t = string.Concat(u.Params.Where(f => f.ID == Convert.ToInt32(matrix[0, j])).Select(f => f.Value));
                    matrix[i, j] = string.IsNullOrEmpty(t) ? "-" : t.ToString();
                }
            }
            return matrix;
        }

        public static int GetLength(int i)
        {
            string[] ws = Parser.Split(Parser.input, true);
            int l = 0;
            for (int j = 0; j < i + 1; j++)
            {
                l += ws[j].Length;
            }
            return l;
        }

        public static int WordCount(DAM.Model.System system)
        {
            int count = 2;
            foreach (var c in system.Params)
                count += 6;
            foreach (var c in system.Objects)
            {
                count += 4;
                foreach (var p in c.Params)
                {
                    count += 4;
                }
            }
            foreach (var c in system.Users)
            {
                count += 4;
                foreach (var p in c.Params)
                {
                    count += 4;
                }
            }
            return count;
        }
    }
}