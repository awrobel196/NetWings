using System.Collections.Generic;
using Domain.Entities;

namespace Application.ViewModels.Website.Details
{
    public class ResponseViewModel
    {
        public string AverageResponse { get; set; }
        public List<WebsiteUptimeScore> ResponseList { get; set; }
    }

}
