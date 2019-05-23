using System;
using System.Collections.Generic;
using System.Text;

namespace SA_APP.Models.FromApi
{
    public class ShoppingListItem
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool InStock { get; set; }

        public int StockAmount { get; set; }

        public string Aisle { get; set; }

        public string Section { get; set; }

        public int? QuantityBought { get; set; }
    }
}
