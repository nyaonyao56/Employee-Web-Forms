using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login {
    public class Employees {
        public int Id { get; set; }
        public string image { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string jobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}