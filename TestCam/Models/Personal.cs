using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCam.Models
{
    public class Personal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ToMeet { get; set; }
        public string Purpose { get; set; }
        public string FromWhere { get; set; }

        public DateTime PassDate { get; set; }

    }
}