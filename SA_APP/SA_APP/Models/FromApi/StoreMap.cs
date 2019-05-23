﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SA_APP.Models.FromApi
{
    public class StoreMap
    {

        public StoreMap()
        {

        }

        public int Id { get; set; }


        // A comma seperated string *,*,60,30,* that will be split and thats how to create row definitions
        public string RowDefinitions { get; set; }
        // A comma seperated string *,*,60,30,* that will be split and thats how to create column definitions
        public string ColumnsDefinitions { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
