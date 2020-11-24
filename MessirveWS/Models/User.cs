using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessirveWS.Models
{
    public class User
    {
        public int IdLogin { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}