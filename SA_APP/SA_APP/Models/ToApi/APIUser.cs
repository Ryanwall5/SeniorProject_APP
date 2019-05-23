using System;
using System.Collections.Generic;
using System.Text;

namespace SA_APP.Models.ToApi
{
    public class APIUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int HomeStoreId { get; set; }
    }
}
