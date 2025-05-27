using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dickplom1.DataFolder
{
    public class SubscriptionsViewModel
    {
        public int SubscriptionId { get; set; }
        public int Number { get; set; }
        public string SubscriptionName { get; set; }
        public int SubscriptionPeriodId { get; set; }
        public string SubscriptionPeriod { get; set; }
        public int SubscriptionTypeId { get; set; }
        public string SubscriptionType { get; set; }
        public string Comment { get; set; }
        public string PriceForMonth { get; set; }
        public string PriceFull { get; set; }
        public string FIOManager { get; set; }
        public int CreatorId { get; set; }
        public string CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
