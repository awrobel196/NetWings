using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UptimeMachineRoot.ViewModels;

namespace UptimeMachineRoot.Services
{
    public static class SendRequest
    {
        public static void Send(UptimeBenchmarkViewModel k)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{k.MachineUrl}");
            req.Method = "POST";
            req.ContentType = "application/json";

            Stream stream = req.GetRequestStream();

            string json = JsonSerializer.Serialize<UptimeBenchmarkViewModel>(k);
            byte[] buffer = Encoding.UTF8.GetBytes(json);

            stream.Write(buffer, 0, buffer.Length);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
        }
    }
}
