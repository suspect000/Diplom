using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dickplom1.DataFolder
{
    public class LogsViewModel
    {
        public int Number { get; set; }
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public string FIO { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}
