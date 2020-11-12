using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API_CORE.Models
{
    public class HistoryModel
    {
        public int ID { get; set; }
        public string workData { get; set; }
        public string stateNumber { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public int intermediateMileage { get; set; }
        public int carID { get; set; }
    }
}
