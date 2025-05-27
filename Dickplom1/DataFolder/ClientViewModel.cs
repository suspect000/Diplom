using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dickplom1.DataFolder
{
    public class ClientViewModel
    {
        public int ClientId { get; set; }
        public int Number { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string SubscriptionStatus { get; set; }
        public byte[] ClientPhoto { get; set; }
        public string CompanyName { get; set; }
        public int? CreatorId { get; set; }
    }
}
