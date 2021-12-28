using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netwings.UptimeMachine.ViewModel
{
    public class SMSWebsiteViewModel
    {
        public string IdWebsite { get; set; }
        public string Name { get; set; }
        public bool UptimeResult { get; set; }
        public string PhoneNumber { get; set; }
        public string SMSMessage { get; set; }
    }
}
