using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAM.Model;
using AccessMatrixClient.Helper;

namespace AccessMatrixClient.API
{
    static class APIHelper
    {
        private static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        public static Input CreateInputModel(string text)
        {
            return new Input
            {
                text = Helper.Help.FormatInput(text)
            };
        }
        //public static HttpContent ExtractJsonContent(HttpResponseMessage response)
        //{
        //    return JsonConvert.DeserializeObject<>response.Content;
        //}
        private static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;
            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(APIDict.ContentTypeJson);
            }
            return httpContent;
        }
        public static async Task<string> PostStreamAsync(string Url, object content, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url))
            using (HttpContent httpContent = CreateHttpContent(content))
            {
                using (HttpResponseMessage response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
        


        public static async Task<HttpResponseMessage> PostJsonAsync(string Url, object content, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
                return await client.PostAsJsonAsync(Url, content);
            //using (var response = await client.PostAsJsonAsync(Url, content))
            //{
            //    //response.EnsureSuccessStatusCode();
            //    return response;
            //}
        }

        public static async Task<string> GetStringAsync(string Url)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(Url))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
