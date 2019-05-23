using System.Collections.Generic;

namespace SA_APP.Models.FromApi
{
    public class Store
    {
        public int id { get; set; }

        public string name { get; set; }

        public string phoneNumber { get; set; }

        public string website { get; set; }

        public Address address { get; set; }

        public StoreMap storeMap { get; set; }

        public List<Item> items { get; set; } = new List<Item>();
    }
}
