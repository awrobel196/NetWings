using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UptimeMachineRoot.Services
{
    public static class GetDateTime
    {
        
        public static DateTime GetCurrentDateTime()
        {
            DateTime date = DateTime.Now;


            if (date.Second != 0)
            {
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour + 1,
                    date.Minute + 1, 0);
            }
            else
            {
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour + 1,
                    date.Minute, date.Second);
            }

            return date;
        }
    }
}
