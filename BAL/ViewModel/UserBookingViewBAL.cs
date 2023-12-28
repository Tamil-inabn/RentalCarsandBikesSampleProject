using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModel
{
    public class UserBookingViewBAL
    {
        public int BookingId { get; set; }
        public string? Name { get; set; }
        public long? MobileNo { get; set; }
        public string? Email { get; set; }
        public int? UserId { get; set; }
        public string? VehicleName { get; set; }
        public string? VehicleNo { get; set; }

        public DateTime? PickUpDate { get; set; }

        public DateTime? DropDate { get; set; }
        public int? VehicleId { get; set; }
    }
}
