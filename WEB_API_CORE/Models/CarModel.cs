using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API_CORE.Models
{
    public class CarModel
    {
        public int ID { get; set; }
        public string VIN { get; set; }
        public string stateNumber { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public int mileage { get; set; }
        public DateTime yearOfIssue { get; set; }
        public int clientID { get; set; }
    }
}
