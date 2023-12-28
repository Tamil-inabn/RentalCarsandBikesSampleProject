using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class BookingTransationViewDAL
    {
        public int TransactionId { get; set; }

        public string? UserName { get; set; }

        public int? TotalDays { get; set; }

        public decimal? TransactionAmount { get; set; }

        public string? VehicleName { get; set; }
        
        public int? BookingId { get; set; }
    }
}
