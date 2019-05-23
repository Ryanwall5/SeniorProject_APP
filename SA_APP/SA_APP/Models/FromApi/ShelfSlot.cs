using System;
using System.Collections.Generic;
using System.Text;

namespace SA_APP.Models.FromApi
{
    public class ShelfSlot
    {
        public int Id { get; set; }

        public int SlotOnShelf { get; set; }
        public Item Item { get; set; }
    }
}
