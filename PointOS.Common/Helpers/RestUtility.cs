using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using PointOS.Common.Settings;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace PointOS.Common.Helpers
{
    public class RestUtility : IRestUtility
    {
        private const string BaseAddress = "https://localhost:44302/api/v1/";
        private readonly ApiBaseUrlSettings _apiBaseUrlSettings;

        public RestUtility(IOptions<ApiBaseUrlSettings> apiBaseUrlSettings)
        {
            _apiBaseUrlSettings = apiBaseUrlSettings.Value;
        }

        /// <summary>
        /// Generic wrapper class to make Rest API Calls
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="requestBodyObject"></param>
        /// <param name="param"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<object> ApiServiceAsync(BaseUrl baseUrl, string url, string token, object requestBodyObject,
            string param, Verb method)
        {
            var apiUrl = baseUrl switch
            {
                BaseUrl.PointOs => _apiBaseUrlSettings.PointOsUrl,
                BaseUrl.NIA => "",
                BaseUrl.Payment => "",
                _ => BaseAddress
            };

            if (IsNullOrWhiteSpace(apiUrl)) apiUrl = BaseAddress;

            apiUrl += url;

            return baseUrl switch
            {
                BaseUrl.NIA => await CallHmacSha256AuthApi(apiUrl, token, param, requestBodyObject),
                BaseUrl.Payment => await CallBasicAuthApi(apiUrl, token, requestBodyObject, param, method),
                BaseUrl.PointOs => await BlazorClientHandler(apiUrl, token, requestBodyObject, param, method),
                _ => await CallBearerAuthApi(apiUrl, token, requestBodyObject, param, method)
            };
        }

        /// <summary>
        /// A generic wrapper class to Custom Hmac SHA256 REST API calls 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authToken"></param>
        /// <param name="param"></param>
        /// <param name="requestBodyObject"></param>
        /// <returns></returns>
        private static async Task<object> CallHmacSha256AuthApi(string url, string authToken, string param,
            object requestBodyObject)
        {
            var text = "";
            // var json = "";
            if (!IsNullOrWhiteSpace(param)) url += param;

            // Create a request for the url
            try
            {
                var webRequest = WebRequest.Create(url);
                webRequest.Headers.Set(HttpRequestHeader.Authorization, "hmac " + authToken);

                //Serialize request object as JSON and write to request body
                if (requestBodyObject != null)
                {
                    var requestBody = JsonConvert.SerializeObject(requestBodyObject);
                    webRequest.ContentLength = requestBody.Length;
                    await using var streamWriter = new StreamWriter(webRequest.GetRequestStream(), Encoding.ASCII);
                    streamWriter.Write(requestBody);
                    //streamWriter.Close();
                }

                //Get the response
                try
                {
                    using var response = webRequest.GetResponse();
                    // if(response.St)

                    var dataStream = response.GetResponseStream();

                    //op
                    var reader = new StreamReader(dataStream, Encoding.UTF8);

                    text = reader.ReadToEnd();
                    var json = JObject.Parse(text);
                    //text =((HttpWebResponse)response).StatusCode.ToString();
                    // Console.WriteLine(text);
                    // return text;

                    reader.Close();
                    dataStream.Close();
                    response.Close();
                }
                catch (WebException e)
                {
                    using var response = e.Response;
                    var httpResponse = (HttpWebResponse)response;

                    await using var data = response.GetResponseStream();
                    using var reader = new StreamReader(data);
                    text = reader.ReadToEnd();
                    Console.WriteLine(text);

                    reader.Close();
                    data.Close();
                    response.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("There is no internet connection");
            }

            return text;
        }

        /// <summary>
        /// A generic wrapper class to Basic Authentication REST API calls 
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="authToken">The operation.</param>
        /// <param name="requestBodyObject">The request body object which is json.</param>
        /// <param name="method">The method.</param>
        /// <param name="param">The username.</param>
        /// <returns></returns>
        private static async Task<object> CallBasicAuthApi(string url, string authToken, object requestBodyObject,
            string param, Verb method)
        {
            try
            {
                if (!IsNullOrWhiteSpace(param)) url += param;

                var webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.Method = method.ToString();
                webReq.Headers["Authorization"] = $"Basic {authToken}";
                webReq.Headers["Cache-Control"] = "no-cache";
                webReq.Headers["Content-Type"] = "application/json";

                //Serialize request object as JSON and write to request body
                if (requestBodyObject != null)
                {
                    var requestBody = JsonConvert.SerializeObject(requestBodyObject);
                    webReq.ContentLength = requestBody.Length;
                    await using var streamWriter = new StreamWriter(webReq.GetRequestStream(), Encoding.ASCII);
                    streamWriter.Write(requestBody);
                    //streamWriter.Close();
                }

                using var response = webReq.GetResponse();
                using var streamReader =
                    new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(),
                        Encoding.UTF8);

                var result = await streamReader.ReadToEndAsync();

                return result;
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// A generic wrapper class to Bearer Authentication REST API calls 
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="authToken">The operation.</param>
        /// <param name="requestBodyObject">The request body object which is json.</param>
        /// <param name="method">The method.</param>
        /// <param name="param">The username.</param>
        /// <returns></returns>
        private static async Task<object> CallBearerAuthApi(string url, string authToken, object requestBodyObject,
            string param, Verb method)
        {
            try
            {
                if (!IsNullOrWhiteSpace(param)) url += param;

                var webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.Method = method.ToString();
                webReq.Headers["Authorization"] = new AuthenticationHeaderValue("Bearer", authToken).ToString();
                webReq.Headers["Cache-Control"] = "no-cache";
                webReq.Headers["Content-Type"] = "application/json";

                //Serialize request object as JSON and write to request body
                if (requestBodyObject != null)
                {
                    var requestBody = JsonConvert.SerializeObject(requestBodyObject);
                    webReq.ContentLength = requestBody.Length;
                    await using var streamWriter = new StreamWriter(webReq.GetRequestStream(), Encoding.ASCII);
                    streamWriter.Write(requestBody);
                    //streamWriter.Close();
                }

                using var response = webReq.GetResponse();
                using var streamReader =
                    new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(),
                        Encoding.UTF8);

                var result = await streamReader.ReadToEndAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// A generic wrapper class for blazor client project REST API calls 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authToken"></param>
        /// <param name="requestBodyObject"></param>
        /// <param name="param"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static async Task<object> BlazorClientHandler(string url, string authToken, object requestBodyObject,
            string param, Verb method)
        {
            if (!IsNullOrWhiteSpace(param)) url += param;

            var client = new HttpClient();

            if (!IsNullOrWhiteSpace(authToken))
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(authToken);

            var content = new StringContent(Empty);

            if (requestBodyObject != null)
            {
                var request = JsonConvert.SerializeObject(requestBodyObject);
                content = new StringContent(request, Encoding.UTF8, "application/json");
            }

            var response = method switch
            {
                Verb.Get => (object)await client.GetStringAsync(url),
                Verb.Post => await client.PostAsync(url, content),
                Verb.Put => await client.PutAsync(url, content),
                Verb.Delete => await client.DeleteAsync(url),
                _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
            };

            if (method == Verb.Get) return response;

            var httpResponse = (HttpResponseMessage)response;
            response = await httpResponse.Content.ReadAsStringAsync();

            return response;
        }
    }
}
