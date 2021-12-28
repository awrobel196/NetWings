using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class MachineResetSettings
    {
        [Key] public int IdMachineResetSettings { get; set; }

        /// <summary>
        /// Czas po którym maszyna resetuje stan stron z IsTested True na False i usuwa BenchmarkUrl
        /// </summary>
        public int ResetTime { get; set; }

    }
}
