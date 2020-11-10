using System;
using System.Collections.Generic;
using System.Text;

namespace BenchmarkTestForCompetitors.Models
{
    public class NiceLemon
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public Country Country { get; set; }

        public bool IsValid { get; set; }
    }

    public enum Country
    {
        China,
        USA,
        Japan
    }
}
