using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dickplom1.DataFolder
{
    public class OrdersViewModel
    {
        public int OrderId { get; set; }
        public int Number { get; set; }
        public int SubcriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string CompanyName { get; set; }
        public int ClientId { get; set; }
        public string FullNameClient { get; set; }
        public string OrderStatus { get; set; }
        public int ClientTypeId { get; set; }
        public string FIOManager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Price { get; set; }
        public int CreatorId { get; set; }
        public string CreatedAt { get; set; }
    }
}
