using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Netwings.UptimeMachine.Services
{
    public static class CreateUptimeRequest
    {
        public static (string statusNumber, int elapsedTime) Create(string url)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient();
            Stopwatch stopwatch = new();
            (string statusNumber, int elapsedTime) WebsieTuple = new();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"http://{url}");

                request.Headers.Add("Accept", "text/html");
                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                request.Headers.Add("Accept-Language", "en");

                stopwatch.Start();
                var response = client.Send(request);

                WebsieTuple.elapsedTime = (int)stopwatch.ElapsedMilliseconds;
                WebsieTuple.statusNumber = ((int) response.StatusCode).ToString();

                return WebsieTuple;
            }
            catch (HttpRequestException e)
            {
                WebsieTuple.elapsedTime = (int)stopwatch.ElapsedMilliseconds;
                WebsieTuple.statusNumber = ((int)e.StatusCode).ToString();
                return WebsieTuple;
            }
        }
    }
}
