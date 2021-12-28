using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class MachineResponseLog
    {
        [Key] public int IdMachineResponseLog { get; set; }
        public DateTime LogDate { get; set; }
        public string? Log { get; set; }
    }
}
