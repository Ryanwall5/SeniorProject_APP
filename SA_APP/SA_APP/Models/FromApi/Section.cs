using System;
using System.Collections.Generic;
using System.Text;

namespace SA_APP.Models.FromApi
{
    public class Section
    {
        public Section() { }

        public int Id { get; set; }
    
        public string Name { get; set; }
  
        public int ItemsPerShelf { get; set; }

        public List<Shelf> Shelves { get; set; }
    }
}
