using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModel
{
    public class MoneyTransactionListViewBAL
    {
        public int TransactionId { get; set; }
        public string? Name { get; set; }
        public string? TransactionAmount { get; set; }
        public string? VehicleName { get; set; }
        public int? BookingId { get; set; }
        public int? UserId { get; set; }
        public string? TotalDays { get; set; }
    }
}
