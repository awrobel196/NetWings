using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EnvVariable
    {
        [Key] public int IdEnvVariable { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
