using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading;
using AccessMatrixClient.Helper;

namespace AccessMatrixClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            API.APIDict.ServerIP = "192.168.1.97";
            API.APIDict.ServerPort = "5000";
        }

        //private async void SetAccessMatrix(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        AccessMatrix.Rows.Clear();
        //        using (HttpResponseMessage response =
        //           await API.APIHelper.PostJsonAsync(API.APIURL.GetAccessMatrix, Helper.Help.ParsedSystem, new CancellationToken()))
        //        {
        //            response.EnsureSuccessStatusCode();
        //            string d = await response.Content.ReadAsStringAsync();
        //            string[,] m = JsonConvert.DeserializeObject<string[,]>(d);
        //            AccessMatrix.ColumnCount = m.GetLength(1) - 1;
        //            for (int i = 1; i < m.GetLength(1); i++)
        //                AccessMatrix.Columns[i - 1].Name = m[0, i];
        //            for (int i = 1; i < m.GetLength(0); i++)
        //            {
        //                string[] tmp = new string[m.GetLength(1) - 1];
        //                for (int j = 1; j < m.GetLength(1); j++)
        //                {
        //                    tmp[j - 1] = m[i, j];
        //                }
        //                AccessMatrix.Rows.Add(tmp);
        //                AccessMatrix.Rows[i - 1].HeaderCell.Value = m[i, 0];
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SetText(sender, e, null, string.Concat(Output.Text, Helper.Help.ShowError(ex)));
        //    }
        //}

        private void SetInput(object sender, EventArgs e, string text)
        {
            Input.Text = Helper.Help.FormatInput(text);
        }
        private void SetOutput(object sender, EventArgs e, string text)
        {
            Output.Text = text;
        }


        private void SetText(object sender, EventArgs e, InputOutput model)
        {
            if(!string.IsNullOrEmpty(model.Input))
                SetInput(sender, e, model.Input);
            if(!string.IsNullOrEmpty(model.Output))
                SetOutput(sender, e, model.Output);
            if (model.MisstakeWord != null)
                MarkMisstakes(Convert.ToInt32(model.MisstakeWord), model.MisstakeLetter, sender, e);
            //RichTextBox rtbInput = new RichTextBox();
            //RichTextBox rtbOutput = new RichTextBox();

            //rtbInput.Text = input;
            //rtbOutput.Text = output;
            //SetInputOutput(sender, e, new InputOutput
            //{
                //Input = rtbInput,
                //Output = rtbOutput
            //});
            // }
        }

        //private async void SetInputOutput(object sender, EventArgs e, InputOutput model)
        //{
        //    try
        //    {
        //        if (model.Input != null)
        //            Input = model.Input;
        //        //Input.Text = await Helper.Help.FormatInput(model.Input.Text);

        //        if (model.Output != null)
        //            Output = model.Output;
        //            //Output.Text = model.Output.Text;
        //    }
        //    catch (Exception ex)
        //    {
        //        Output.Text = AccessMatrixClient.Helper.Help.ShowError(ex);
        //    }
        //}



        public void MarkMisstakes(int word, int? letter, object sender, EventArgs e)
        {
            Input.SelectionStart = word;
            Input.SelectionLength = Convert.ToInt32(letter);
            Input.SelectionColor = Color.Red;
            Input.SelectionLength = 0;
            Input.SelectionStart = Input.Text.Length;
        }

        
        private void ShowAccessMatrix()
        {
            if (Helper.Help.ParsedSystem == null)
            {
                return;
            }

            string [,] m = Helper.Help.AccessMatrix();
            AccessMatrix.Rows.Clear();
            AccessMatrix.ColumnCount = m.GetLength(1);
            for(int i = 0; i < m.GetLength(1); i++)
            {
                string[] tmp = new string[m.GetLength(0)];
                for(int j = 0; j < m.GetLength(0); j++)
                {
                    tmp[j] = m[i, j];
                }
                AccessMatrix.Rows.Add(tmp);
            }
        }

        private void MarkUnnecessary(int l)
        {
            Input.SelectionStart = l;
            Input.SelectionLength = Input.Text.Length - l;
            Input.SelectionColor = Color.OrangeRed;
            Input.SelectionLength = 0;
            Input.SelectionStart = Input.Text.Length;
        }

        private async void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!API.Syncronized.IsSynced)
            {
                MessageBox.Show("Прежде необходимо синхронизироваться с сервиром (Settings - Sync)!");
                return;
            }


            try
            {
                SetInput(sender, e, Helper.Help.FormatInput(Input.Text));
                SetText(sender, e, await Helper.Help.SendToParse(Input.Text));

                MarkUnnecessary(Helper.Help.ParsedSystem.Length);

                ShowAccessMatrix();
                //SetInputOutput(sender, e, await Helper.Help.SendToParse(Input));
                //SetAccessMatrix(sender, e);
                
                    //AccessMatrix = await Helper.Help.ShowAccessMatrix();//через БД
                //AccessMatrix.Refresh();
                //Output.Text = await Helper.SendToParse(Input.Text);



                //var response = JsonConvert.DeserializeObject<WebDAC.SystemOperation>(await APIHelper.PostJsonAsync(
                //    Url, 
                //    Helper.SetInputModel(Input.Text), 
                //    new CancellationToken()));

                //if(response is WebDAC.DACExceptionModel)
                //{
                //    SetOutputText(Helper.ShowError(response), sender, e);
                //    return;
                //}
                //if(response is WebDAC.SystemOperation)
                //{
                //    SetOutputText(Helper.ShowOutput(response), sender, e);
                //}




                ////string g = Helper.getContent(Url);
                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                //var stringTask = client.GetStringAsync("http://192.168.72.130:5000/api/system/");
                //var msg = await stringTask;
                //Input.Text = Input.Text + msg;





                //var client = new RestClient("http://192.168.72.130:5000/api/system/parse");
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("Postman-Token", "1ef75241-9769-4012-a527-e9a17a83bb61");
                //request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("Content-Type", "application/json");
                //request.AddParameter("undefined", "{\n    \"text\": \"k:dds/123=a33,dds/123=ds.assdsads22/211:size=1,visible=true;ads22/21:size=0,visible=true.keka/12:211=e;kukareka/12:21=r,21=e.\"\n}", ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);

                //HttpClient client = new HttpClient();
                //var myContent = JsonConvert.SerializeObject(new InputModel { text = Regex.Replace(InputTerminal.Text, @"s+", " ").Trim() });
                //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                //var byteContent = new ByteArrayContent(buffer);
                //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //var result = client.PostAsync(Url, byteContent).Result;
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var content = new StringContent(JsonConvert.SerializeObject(new InputModel { text = Regex.Replace(InputTerminal.Text, @"s+", " ").Trim() }), Encoding.UTF8, "application/json");
                ////var result = client.PostAsync(Url, content);
                //var result = client.GetAsync(Url);            
                //var r = client.PostAsync(Url, )

                //using (HttpClient client = new HttpClient())
                ////{
                ////    //var r4 = await client.GetAsync(Url.Replace("/parse", ""));
                ////    client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                //{
                //    client.BaseAddress = new Uri("http://192.168.72.130:5000/");
                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "91584fa5-5bff-42c2-97e7-5d80f193f3b5");
                //    client.DefaultRequestHeaders.Accept.Clear();
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    HttpResponseMessage resp = await client.PostAsJsonAsync("api/system/parse", "dsfsfsdfdsfd");
                //    var r = resp.Content;
                //    string g = "asdsal;dklas";
                //}

                //}
                ////Helper.Run();
                //var t = Helper.GetAsync(Url);
                //Helper.PostJson(Input.Text, Url, new CancellationToken()).Wait();
                ////Helper.PostStreamAsync(JsonConvert.SerializeObject(new InputModel { text = Regex.Replace(InputTerminal.Text, @"s+", " ").Trim() }), Url, new CancellationToken()).Wait();
                //////{
                ////    ////MessageBox.Show(httpResponse.StatusCode + Environment.NewLine + httpResponse.Content.ToString());
                ////string g;
                ////}
            }
            catch (Exception ex)
            {
                SetText(sender, e, new InputOutput
                {
                    Output = AccessMatrixClient.Helper.Help.ShowError(ex)
                });
            }


            //    client.BaseAddress = new Uri("https://192.168.72.130:5001");
            //string Address = "/api/system/parse";
            ////client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var d = new InputModel
            //{
            //    text = Regex.Replace(InputTerminal.Text, @"\s+", string.Empty)
            //};
            //HttpResponseMessage resp = client.PostAsync(Address, d.);
        }


        private void correctToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!API.Syncronized.IsSynced)
            {
                MessageBox.Show("Прежде необходимо синхронизироваться с сервиром (Settings - Sync)!");
                return;
            }

            SetInput(sender, e, API.Syncronized.CorrectExample);


            //try
            //{
            //    SetText(
            //        sender,
            //        e,
            //        await API.APIHelper.GetStringAsync(API.APIURL.GetCorrectExample),
            //        null);
            //}
            //catch(Exception ex)
            //{
            //    SetText(sender, e, null, string.Concat(Output.Text, Helper.Help.ShowError(ex)));
            //}
            //using (RichTextBox rtb = new RichTextBox())
            //{
            //    rtb.Text = "k:dds / 123 = a33,dds / 123 = ds.assdsads22 / 211:size = 1,visible = true;ads22 / 21:size = 0,visible = true.keka / 12:211 = e;kukareka / 12:21 = r,21 = e.";
            //    SetText(sender, e,
            //        new InputOutput
            //        {
            //            Input = rtb
            //        });
            //}
        }

        private void incorrectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!API.Syncronized.IsSynced)
            {
                MessageBox.Show("Прежде необходимо синхронизироваться с сервиром (Settings - Sync)!");
                return;
            }

            SetInput(sender, e, API.Syncronized.InCorrectExample);






            //try
            //{
            //    SetText(
            //    sender,
            //    e,
            //    await API.APIHelper.GetStringAsync(API.APIURL.GetInCorrectExample),
            //    null);
            //}   
            //catch (Exception ex)
            //{
            //    SetText(sender, e, null, string.Concat(Output.Text, Helper.Help.ShowError(ex)));
            //}
            //RichTextBox rtb = new RichTextBox();//)
            ////{
            //    rtb.Text = "k:ddsfmd.,m.,samd.,am ,sm,da mm  / 123 = a33,dds / 123 = ds.assdsads22 / 211:size = 1,visible = true;ads22 / 21:size = 0,visible = true.keka / 12:211 = e;kukareka / 12:21 = r,21 = e.";
            //    SetText(sender, e,
            //            new InputOutput
            //            {
            //                Input = rtb
            //            });
            //}
        }

        private void configureServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigureServerForm f = new ConfigureServerForm();
            f.ShowDialog();
            syncToolStripMenuItem_Click(sender, e);
        }

        private void syncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SetOutput(sender, e, "Синхронизация...");
                Helper.Help.Sync();
                API.Syncronized.IsSynced = true;
                SetOutput(sender, e, "Синхронизация завершина!");
            }
            catch
            {
                MessageBox.Show("Ошибка синхронизации!\n Попробуйте еще раз.");
            }
            outputToolStripMenuItem_Click(sender, e);

        }

        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Clear();
        }

        private void matrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccessMatrix.Rows.Clear();
            AccessMatrix.RowCount = 0;
            AccessMatrix.ColumnCount = 0;
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Output.Clear();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputToolStripMenuItem_Click(sender, e);
            matrixToolStripMenuItem_Click(sender, e);
            outputToolStripMenuItem_Click(sender, e);
        }

        
        private async void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!API.Syncronized.IsSynced)
            {
                MessageBox.Show("Прежде необходимо синхронизироваться с сервиром (Settings - Sync)!");
                return;
            }
            if (Helper.Help.ParsedSystem == null)
            {
                MessageBox.Show("Система некорректна или пуста!");
                return;
            }
            try
            {
                await Helper.Help.SaveSystem();
                MessageBox.Show("Система успешна занесена в базу данных!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка сохранения, попробуйте еще раз!\n\n{ex.Message}");
            }
        }
    }
}