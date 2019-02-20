using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAM.Method {
    static class SystemMethod {
        public static DAM.Model.System Parse (string input) {
            //try
            //{
            //    return Parser.Run(input);
            //}
            //catch (DACException ex)
            //{

            //}

            


            return Parser.Run (input);
        }

        public static string[, ] GetAccessMatrix (DAM.Model.System system) {
            Dictionary<Tuple<string, int>, string> AccessMatrix = new Dictionary<Tuple<string, int>, string> ();
            string[, ] matrix = new string[system.Users.Count + 1, system.Objects.Count + 1];
            int i = 1;
            foreach (int c in system.Objects.Select (f => f.ID).Distinct ()) {
                matrix[0, i] = c.ToString ();
                i++;
            }
            i = 1;
            foreach (string c in system.Users.Select (f => f.Name).Distinct ()) {
                matrix[i, 0] = c;
                i++;
            }
            i = 1;
            foreach (DAM.Model.User u in system.Users) {
                matrix[i, 0] = u.Name;

                for (int j = 1; j < system.Objects.Count; j++) {
                    string t = string.Concat (u.Params.Where (f => f.ID == Convert.ToInt32 (matrix[0, j])).Select (f => f.Value));
                    matrix[i, j] = string.IsNullOrEmpty (t) ? "-" : t.ToString ();
                }
            }

            //     List<KeyValuePair<int, string>> Access = new List<KeyValuePair<int, string>>();
            //     foreach(DAM.Model.Param p in u.Params)
            //     {
            //         Access.Add(new KeyValuePair<int, string>(p.ID, p.Value));
            //         j++;
            //     }
            //     // for(DAM.Model.Object o in system.Objects)
            //     // {

            //     //     j++;
            //     // }
            //     i++;
            // }

            // for(i = 0; i < system.Users.Count; i++)
            // {
            //     for(int j = 0; j < system.Objects.Count; i++)
            //     {
            //         KeyValuePair<int, string> Access = new KeyValuePair<int, string>();
            //         foreach(DAM.Model.Param p in )
            //         matrix[i+1,j+1] = 
            //     }
            // }

            // foreach(int c in system.AccessMatrix.Keys.Select(f=>f.Item2).Distinct())
            // {
            //     //matrix[0, i] = system.Objects.Where(f => f.ID == c).FirstOrDefault().Name;
            //     matrix[0, i] = c.ToString();
            //     i++;
            // }
            // i = 1;
            // foreach(string c in system.AccessMatrix.Keys.Select(f=>f.Item1).Distinct())
            // {
            //     matrix[i, 0] = c;i++;
            // }

            // for(i = 0; i < system.Users.Count; i++)
            // {
            //     for(int j = 0; j<system.Objects.Count; j++)
            //     {
            //         matrix[i+1, j+1] = system.AccessMatrix.ContainsKey(new Tuple<string, int>(matrix[i+1, 0], Convert.ToInt32(matrix[0,j+1]))) 
            //             ? system.AccessMatrix[new Tuple<string, int>(matrix[i+1, 0], Convert.ToInt32(matrix[0, j+1]))] 
            //             : "-";
            //     }
            // }
            // for(i = 1; i < system.Objects.Count; i++)
            // {
            //     matrix[0, i] = system.Objects.Where(f => f.ID == Convert.ToInt32(matrix[0, i])).FirstOrDefault().Name;
            // }//foreach (SystemUser u in system.Users)
            // //{
            // //    matrix[i, 0] = u.Name;
            // //    i++;
            // //}
            // //i = 1;
            // //foreach (SystemObject o in system.Objects)
            // //{
            // //    matrix[0, i] = o.Name;
            // //    int j = 1;
            // //    foreach (SystemUser u in system.Users)
            // //    {
            // //        if (u.Params.Any(f => f.ID == o.ID))
            // //            matrix[j, 1] = u.Params.Where(f => f.ID == o.ID).Select(f => f.Value).Aggregate((k, next) => k + next);
            // //        j++;
            // //    }
            // //}
            return matrix;
        }

        public static int GetLength (int i) {
            string[] ws = Parser.Split(Parser.input, true);
            int l = 0;
            for(int j = 0; j < i + 1; j++)
            {
                l += ws[j].Length;
            }
            return l;
            // int c = 0;
            // c += system.Name.Length + 1;
            // foreach (DAM.Model.Param p in system.Params) {
            //     c += p.Name.Length + 1 + p.ID.ToString ().Length + 1 + p.Value.Length + 1;
            // }
            // foreach (DAM.Model.Object o in system.Objects) {
            //     c += o.Name.Length + 1 + o.ID.ToString ().Length + 1;
            //     foreach (DAM.Model.Param p in o.Params) {
            //         c += p.Name.Length + 1 + p.Value.Length + 1;
            //     }
            // }
            // foreach (DAM.Model.User u in system.Users) {
            //     c += u.Name.Length + 1 + u.ID.ToString ().Length + 1;
            //     foreach (DAM.Model.Param p in u.Params) {
            //         c += p.ID.ToString ().Length + 1 + p.Value.Length + 1;
            //     }
            // }
            // return c;
        }

        //public static string Print(DAM.Model.System system)
        //{
        //    string tab = "  ";

        //    string ret = $"Система:"
        //               + $"\n{tab}Имя: {system.Name}"
        //               + $"\n{tab}Параметры:";
        //    foreach (Param p in system.Params)
        //    {
        //        ret += $"\n{tab}{tab}Имя: {p.Name}"
        //             + $"\n{tab}{tab}ID: {p.ID}"
        //             + $"\n{tab}{tab}Значение: {p.Value}";
        //    }
        //    ret += $"\n{tab}Объекты:";
        //    foreach (SystemObject o in system.Objects)
        //    {
        //        ret += $"\n{tab}{tab}Имя: {o.Name}"
        //             + $"\n{tab}{tab}ID: {o.ID}"
        //             + $"\n{tab}{tab}Параметры:{o.Name}";
        //        foreach (Param p in o.Params)
        //        {
        //            ret += $"\n{tab}{tab}{tab}Имя: {p.Name}"
        //                 + $"\n{tab}{tab}{tab}Значение: {p.Value}";
        //        }
        //    }
        //    ret += $"\n{tab}Пользователи:";
        //    foreach (User u in system.Users)
        //    {
        //        ret += $"\n{tab}{tab}Имя: {u.Name}"
        //             + $"\n{tab}{tab}ID: {u.ID}"
        //             + $"\n{tab}{tab}Параметры: {u.Name}";
        //        foreach (Param p in u.Params)
        //        {
        //            ret += $"\n{tab}{tab}{tab}ID объекта: {p.ID}"
        //                 + $"\n{tab}{tab}{tab}Доступ: {p.Value}";
        //        }
        //    }
        //    return ret;
        //}

        public static int WordCount (DAM.Model.System system) {
            int count = 2;
            foreach (var c in system.Params)
                count += 6;
            foreach (var c in system.Objects) {
                count += 4;
                foreach (var p in c.Params) {
                    count += 4;
                }
            }
            foreach (var c in system.Users) {
                count += 4;
                foreach (var p in c.Params) {
                    count += 4;
                }
            }
            return count;
        }

        //public static void AddAccessParam(OperationSystem system, string UserName, int ObjectID, string Value)
        //{
        //    if (system.NewAccessMatrix[new Tuple<string, int>()])
        //}
        // public static void AddAccessParam(DAM.Model.System system, Tuple<string,int> Key, string Value)
        // {
        //     if (system.AccessMatrix.ContainsKey(Key))
        //     {
        //         if(!system.AccessMatrix[Key].Contains(Value))
        //             system.AccessMatrix[Key] = system.AccessMatrix[Key] + Value;
        //     }
        //     else
        //     {
        //         system.AccessMatrix.Add(Key, Value);
        //     }
        // }
    }
}