using SA_APP.Models.FromApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace SA_APP.Models
{
    public class Aisle
    {
        public int Id { get; set; }

        // 1, 2, 3, 4, 5
        public string Name { get; set; }
        
        // Left or Right
        public string SideOfAisle { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

        public List<Section> Sections { get; set; } = new List<Section>();
    }
}
