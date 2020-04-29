using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DAL.RemoteStorage
{
    public class HttpContext
    {

        public string Host { get; set; }

        public void Post<T>(T obj)
        {
            var request = WebRequest.Create(Host) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            
            var jsonObj = JsonSerializer.Serialize(obj);
            var byteArray = Encoding.UTF8.GetBytes(jsonObj);
            request.ContentLength = byteArray.Length;

            using var stream = request.GetRequestStream();
            stream.Write(byteArray, 0, byteArray.Length);
            request.GetResponse();
        }

        public T Get<T>(Dictionary<string, string> parameters = null)
        {

            var host = Host;
            
            if (parameters != null)
            {
                var strParameters = "?";
                foreach (var (key, value) in parameters)
                {
                    strParameters += key + "=" + value;
                    strParameters += "&";
                }

                strParameters.Remove(strParameters.Length - 1);

                host += strParameters;
            }

            var request = WebRequest.Create(host) as HttpWebRequest;
            request.Method = "GET";

            var response = request.GetResponse();
            var jsonObj = "";
            using (var stream = response.GetResponseStream())
            {
                using var reader = new StreamReader(stream);
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    jsonObj += line;
                }
            }

            var obj = JsonSerializer.Deserialize<T>(jsonObj);

            return obj;
        }
    }
}