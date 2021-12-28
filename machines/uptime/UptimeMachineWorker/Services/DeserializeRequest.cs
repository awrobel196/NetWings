using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Netwings.UptimeMachine.ViewModel;

namespace Netwings.UptimeMachine.Services
{
    public static class DeserializeRequest
    {
        public static UptimeBenchmarkViewModel Deserialize(HttpRequest req)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            UptimeBenchmarkViewModel websites = JsonSerializer.Deserialize<UptimeBenchmarkViewModel>(requestBody);

            return websites;
        }
    }
}
