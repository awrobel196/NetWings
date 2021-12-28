using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.ViewModels.License
{
    public class LicenseDashboardViewModel
    {
        public string ShortIdLicense { get; set; }
        public string LicenseExpired { get; set; }
        public int MaxWebsites { get; set; }
        public int CurrentWebsites { get; set; }
        public int MaxUsers { get; set; }
       
        public int CurrentUsers { get; set; }
        public int DaysLeft { get; set; }
        public List<User> Users { get; set; }  

    }
}
