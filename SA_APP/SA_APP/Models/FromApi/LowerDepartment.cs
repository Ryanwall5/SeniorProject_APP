using System;
using System.Collections.Generic;
using System.Text;

namespace SA_APP.Models
{
    public class LowerDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public int AisleCount { get; set; }
        public int AisleStart { get; set; }
        public List<Aisle> Aisles { get; set; } = new List<Aisle>();
    }
}
