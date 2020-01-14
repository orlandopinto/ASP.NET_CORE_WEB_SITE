using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CORE_WEB_SITE.Models
{
    public class AppSettings
    {
        public string Colour;

        public string ConnectionString;
        //public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string Test { get; set; }
        public string Uat { get; set; }
        public string Prod { get; set; }
    }
}
