using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class WebsiteUptimeMachineLibrary
    {
        [Key]
        public int IdWebsiteUptimeMachineLibrary { get; set; }
        public string? MachineAdress { get; set; }

        public TestLocation MachineLocation { get; set; }
        public TestEnviroment MachinEnviroment { get; set; }
    }
}
