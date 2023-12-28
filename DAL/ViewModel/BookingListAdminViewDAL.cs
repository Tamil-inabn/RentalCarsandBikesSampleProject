using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class BookingListAdminViewDAL
    {
        public List<UserList> userLists { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public class UserList
    {
        public int BookingId { get; set; }

        public int? UserId { get; set; }

        public string? VehicleNo { get; set; }

        public string? PickUpDate { get; set; }

        public string? DropDate { get; set; }
        public string? Name { get; set; }

        public long? MobileNo { get; set; }

        public string? Email { get; set; }

        public string? VehicleName { get; set; }

        public bool? IsActive { get; set; }
    }
}
