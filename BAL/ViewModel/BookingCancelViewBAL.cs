using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModel
{
    public class BookingCancelViewBAL
    {
        public int CancelId { get; set; }

        public string? UserName { get; set; }

        public string? Reason { get; set; }

        public string? VehicleName { get; set; }

        public int? UserId { get; set; }

        public bool? IsActive { get; set; }
    }
}
