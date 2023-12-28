using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModel
{
    public class UserVehicleViewBAL
    {
        public int VehicleId { get; set; }

        public string? VehicleNo { get; set; }

        public string? VehicleName { get; set; }

        public string? VehicleType { get; set; }

        public string? VehiclePhoto { get; set; }
        public string? VehiclePhotos { get; set; }
        public int? NoOfVehicle { get; set; }
        public bool? IsActive { get; set; }
        public int? AdminId { get; set; }

    }
}
