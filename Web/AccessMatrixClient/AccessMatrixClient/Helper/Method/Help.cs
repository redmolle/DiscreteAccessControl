using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using System.Windows.Forms;
using AccessMatrixClient.API;
using DAM.Model;


namespace AccessMatrixClient.Helper
{
    static class Help
    {
        public static DAM.Model.System ParsedSystem { get; set; }

        //public async static Task<DataGridView> ShowAccessMatrix()
        //{
        //    DataGridView dgv = new DataGridView();
        //    using (HttpResponseMessage response =
        //        await APIHelper.PostJsonAsync(APIURL.GetAccessMatrix, ParsedSystem, new CancellationToken()))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        string d = await response.Content.ReadAsStringAsync();
        //        string[,] m = JsonConvert.DeserializeObject<string[,]>(d);
        //        dgv.ColumnCount = m.GetLength(1);
        //        for (int i = 1; i < m.GetLength(1); i++)
        //            dgv.Columns[i - 1].Name = m[0, i];
        //        for (int i = 1; i < m.GetLength(0); i++)
        //        {
        //            string[] tmp = new string[m.GetLength(1) - 1];
        //            for(int j = 1; j< m.GetLength(1); j++)
        //            {
        //                tmp[j - 1] = m[i, j];
        //            }
        //            dgv.Rows.Add(tmp);
        //            dgv.Rows[i - 1].HeaderCell.Value = m[i, 0];
        //        }
        //        return dgv;
        //    }
        //}

        public static string[,] AccessMatrix()
        {
            int ucount = ParsedSystem.Users.Count,
                ocount = ParsedSystem.Objects.Count,
                i = 1;
                //j = 0;
            string[,] m = new string[ucount+1, ocount+1];
            foreach(DAM.Model.Object o in ParsedSystem.Objects)
            {
                m[0, i] = o.ID.ToString();
                i++;
            }
            i = 1;
            foreach(DAM.Model.User u in ParsedSystem.Users)
            {
                m[i, 0] = u.Name;
                for(int j = 1; j < ocount + 1; j++)
                {
                    string tmp = string.Concat(u.Params.Where(f => f.ID.ToString() == m[0, j]).Select(f => f.Value).Distinct());
                    m[i, j] = string.IsNullOrEmpty(tmp) ? "-" : tmp;
                }
                i++;
                //j++;
                //m[i,j] = 
                //foreach(DAM.Model.Param p in u.Params)
                //{
                //    j++;
                //    m[i,j] = 
                //}
            }
            return m;
        }

        public async static Task SaveSystem()
        {
            using (HttpResponseMessage response = await APIHelper.PostJsonAsync(APIURL.SaveSystem, ParsedSystem, new CancellationToken()))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public async static Task<InputOutput> SendToParse(string input)
        {
            using (HttpResponseMessage response = 
                await APIHelper.PostJsonAsync(APIURL.ParseSystem, new Input { text = input }, new CancellationToken()))
            {
                string rtb;
                InputOutput model = new InputOutput();
                switch (Convert.ToInt32(response.StatusCode))
                {
                    case 200:
                        ParsedSystem = JsonConvert.DeserializeObject<DAM.Model.System>(await response.Content.ReadAsStringAsync());
                        model.Output = ShowOutput(ParsedSystem);break;
                    case 418:
                        ParsedSystem = null;
                        DAM.Model.DACException misstake = JsonConvert.DeserializeObject<DACException>(await response.Content.ReadAsStringAsync());
                        model.Output = $"Встречена ошибка в тексте! {misstake.message}";
                        //AccessMatrixClient.MainForm.ActiveForm
                        model.MisstakeWord = misstake.markFrom;
                        model.MisstakeLetter = misstake.markTo;




                        //input = MatchWrongInput(input, response);
                        break;
                    case 500:
                    default:
                        model.Output = "Неизвестная ошибка!";break;
                }
                //model.Input = Helper.Help.FormatInput(input);
                return model;
            }
        }

        public static RichTextBox MatchWrongInput(RichTextBox initialInput, HttpResponseMessage response)
        {
            return initialInput;//добавить выделение слова
        }



        public static string FormatInput(string _input)
        {
            string input = StringFormatter(_input);
            foreach(char c in API.Syncronized.NLSeparators)
            {
               input = input.Replace(c.ToString(), c.ToString()+Environment.NewLine);
            }
            foreach (char c in API.Syncronized.TabSeparators)
            {
                input = input.Replace(c.ToString(), c.ToString() + "\n   ");
            }
            foreach (char c in API.Syncronized.Separators)
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








        public async static void Sync()
        {
            API.Syncronized.Separators = await APIHelper.GetStringAsync(APIURL.GetSeparators);
            API.Syncronized.TabSeparators =await APIHelper.GetStringAsync(APIURL.GetTabSeparators);
            API.Syncronized.NLSeparators = await APIHelper.GetStringAsync(APIURL.GetNLSeparators);
            API.Syncronized.CorrectExample = await APIHelper.GetStringAsync(APIURL.GetCorrectExample);
            API.Syncronized.InCorrectExample = await APIHelper.GetStringAsync(APIURL.GetInCorrectExample);
            //API.Syncronized.IsSynced = true;
        }

    }
}
