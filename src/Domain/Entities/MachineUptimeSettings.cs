using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class MachineUptimeSettings
    {
        [Key] public int IdMachineUptimeSettings { get; set; }

        /// <summary>
        /// Czas po którym maszyna znów sprawdza czy nie ma dostępnej jakiejść strony do przeprowadzenia benchmarku
        /// </summary>
        public int WaitingTime { get; set; }

        /// <summary>
        /// Ilość stron jaką maszyna pobiera przy każdym sprawdzaniu
        /// </summary>
        public int NumberOfSelectedWebsite { get; set; }

        /// <summary>
        /// Czas ostatnio wykonywanego zadania
        /// </summary>
        public string? LastTaskTime { get; set; }
    }
}
