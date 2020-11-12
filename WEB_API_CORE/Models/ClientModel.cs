using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API_CORE.Models
{
    public class ClientModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public DateTime birthDate { get; set; }
    }
}
