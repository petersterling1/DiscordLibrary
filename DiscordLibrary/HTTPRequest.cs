using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscordLibrary
{
    internal class HTTPRequest
    {
        public HTTPRequest()
        {
            //Empty constructor
        }

        /// <summary>
        /// Sends a get request and returns the server response
        /// </summary>
        /// <param name="uri">Where to make the GET request</param>
        /// <param name="authorization">An authorization value, if one is needed</param>
        /// <param name="headers">A dictionary of other headers, if needed</param>
        /// <returns></returns>
        public String GetRequest(String uri, String authorization = null, Dictionary<String, String> headers = null)
        {
            WebRequest webRequest;
            Stream stream;
            StreamReader streamReader;

            try
            {
                webRequest = WebRequest.Create(uri);

                if (authorization != null)
                {
                    webRequest.Headers[HttpRequestHeader.Authorization] = authorization;
                }

                if (headers != null)
                {
                    foreach (KeyValuePair<String, String> header in headers)
                    {
                        webRequest.Headers[header.Key] = header.Value;
                    }
                }

                stream = webRequest.GetResponse().GetResponseStream();
                streamReader = new StreamReader(stream);
            }
            catch (WebException e)
            {
                String response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                return response;
            }

            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// Sends a post request with the data sent as a JSON payload
        /// </summary>
        /// <param name="uri">Where to make the POST request</param>
        /// <param name="data">Data to put in the body of the request</param>
        /// <param name="authorization">An authorization value, if one is needed</param>
        /// <param name="headers">A dictionary of other headers, if needed</param>
        /// <returns>Server response. Will be null if the request failed.</returns>
        public String SendPostRequest(String uri, String data, String authorization = null, Dictionary<String, String> headers = null)
        {
            WebRequest webRequest = WebRequest.Create(uri);

            if (authorization != null)
            {
                webRequest.Headers[HttpRequestHeader.Authorization] = authorization;
            }

            if (headers != null)
            {
                foreach (KeyValuePair<String, String> header in headers)
                {
                    webRequest.Headers[header.Key] = header.Value;
                }
            }

            webRequest.ContentType = "application/json; charset=utf-8";
            webRequest.Method = "POST";

            using (StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            String result = null;

            try
            {
                HttpWebResponse response = (HttpWebResponse) webRequest.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }

                return result;
        }
    }
}
