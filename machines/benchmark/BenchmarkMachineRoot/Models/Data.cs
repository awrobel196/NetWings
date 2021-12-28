using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkMachineRoot.Models
{
    public class Data
    {
        public string type { get; set; } = "test";
        public Attributes attributes { get; set; } = new();
    }
}
