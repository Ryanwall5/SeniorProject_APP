using System;
using System.Collections.Generic;
using System.Text;

namespace SA_APP.Models.FromApi
{
    public class Shelf
    {
        public Shelf() { }

        public int Id { get; set; }
        
        public int ShelfNumber { get; set; }

        public List<ShelfSlot> Slots { get; set; }
    }
}
