using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using NetWings.Data.Context;
using Netwings.UptimeMachine.ViewModel;

namespace Netwings.UptimeMachine.Services
{
    public static class SendSMSRequest
    {

        //public static void CheckSendSms(NetWingsContext context, string idWebsite, string statusNumber)
        //{

        //    //Pobieranie strony i sprawdzanie czy jej dotychczasowy status Uptime jest taki sam jak ostatni
        //    var thisWebsite = context.Website.Where(x => x.IdWebsite == new Guid(idWebsite))
        //        .Include(x => x.License).ThenInclude(x => x.User).First();

        //    if (thisWebsite.LatestUptimeResult == true && statusNumber != "200")
        //    {
        //        SendRequest(new SMSWebsiteViewModel()
        //            {
        //                IdWebsite = idWebsite,
        //                Name = thisWebsite.Name,
        //                PhoneNumber = thisWebsite.License.User.FirstOrDefault().Phone,
        //                SMSMessage = $"Twoja strona {thisWebsite.Name} jest niedostępna."
        //            });

        //        thisWebsite.LatestUptimeResult = false;
        //        context.SaveChanges();
        //    }
        //    else if (thisWebsite.LatestUptimeResult == false && statusNumber == "200")
        //    {
        //        SendRequest(new SMSWebsiteViewModel()
        //            {
        //                IdWebsite = idWebsite,
        //                Name = thisWebsite.Name,
        //                PhoneNumber = thisWebsite.License.User.FirstOrDefault().Phone,
        //                SMSMessage = $"Twoja strona {thisWebsite.Name} jest jest znów dostępna."
        //            });

        //        thisWebsite.LatestUptimeResult = true;
        //        context.SaveChanges();
        //    }
        //}
        //public static bool SendRequest(SMSWebsiteViewModel smsWebsite)
        //{
        //    try
        //    {
        //        SMSApi.Api.IClient client = new SMSApi.Api.ClientOAuth("Fxv6LmmzJMtP9i7X7lvuh5ZP9qSgH6i5c69fzxEJ");

        //        var smsApi = new SMSApi.Api.SMSFactory(client);
        //        // for SMSAPI.com clients:
        //        // var smsApi = new SMSApi.Api.SMSFactory(client, ProxyAddress.SmsApiCom);

        //        var result =
        //            smsApi.ActionSend()
        //                .SetText($"{smsWebsite.SMSMessage}")
        //                .SetTo($"{smsWebsite.PhoneNumber}")
        //                .SetSender("Netwings") //Sender name
        //                .Execute();

        //        System.Console.WriteLine("Send: " + result.Count);

        //        string[] ids = new string[result.Count];

        //        for (int i = 0, l = 0; i < result.List.Count; i++)
        //        {
        //            if (!result.List[i].isError())
        //            {
        //                if (!result.List[i].isFinal())
        //                {
        //                    ids[l] = result.List[i].ID;
        //                    l++;
        //                }
        //            }
        //        }

        //        System.Console.WriteLine("Get:");
        //        result =
        //            smsApi.ActionGet()
        //                .Ids(ids)
        //                .Execute();

        //        foreach (var status in result.List)
        //        {
        //            System.Console.WriteLine("ID: " + status.ID + " NUmber: " + status.Number + " Points:" + status.Points + " Status:" + status.Status + " IDx: " + status.IDx);
        //        }

        //        return true;
        //    }
        //    catch (SMSApi.Api.ActionException e)
        //    {
        //        /**
        //         * Action error
        //         */
        //        System.Console.WriteLine(e.Message);
        //    }
        //    catch (SMSApi.Api.ClientException e)
        //    {
        //        /**
        //         * Error codes (list available in smsapi docs). Example:
        //         * 101 	Invalid authorization info
        //         * 102 	Invalid username or password
        //         * 103 	Insufficient credits on Your account
        //         * 104 	No such template
        //         * 105 	Wrong IP address (for IP filter turned on)
        //         * 110	Action not allowed for your account
        //         */
        //        System.Console.WriteLine(e.Message);
        //    }
        //    catch (SMSApi.Api.HostException e)
        //    {
        //        /* 
        //         * Server errors
        //         * SMSApi.Api.HostException.E_JSON_DECODE - problem with parsing data
        //         */
        //        System.Console.WriteLine(e.Message);
        //    }
        //    catch (SMSApi.Api.ProxyException e)
        //    {
        //        // communication problem between client and sever
        //        System.Console.WriteLine(e.Message);
        //    }

        //    return false;
        //}
    }
}
