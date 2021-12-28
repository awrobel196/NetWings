using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class MachineUptimeLog
    {
        [Key] public int IdMachineUptimeLog { get; set; }
        public DateTime LogDate { get; set; }
        public string? Log { get; set; }
    }
}
