using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sportsmanagementsystem4.Models
{
    public class PlayerentryDTO
    {
        
        public string name { get; set; }
        public string reg_number { get; set; }
        public string role { get; set; }
        public int id { get; set; }
        public string teamName { get; set; }
    }
}