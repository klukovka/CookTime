using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CookTime.Models
{
    public class Connection
    {
        public string ConnStr
        {
            get => WebConfigurationManager.ConnectionStrings["CookBookContext"].ConnectionString;
        }
    }
}