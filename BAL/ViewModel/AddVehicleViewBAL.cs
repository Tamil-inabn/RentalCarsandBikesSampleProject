using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModel
{
    public  class AddVehicleViewBAL
    {
        public int VehicleId { get; set; }

        public string? VehicleNo { get; set; }

        public string? VehicleName { get; set; }

        public string? VehicleType { get; set; }
        public string? VehiclePhotos { get; set; }
        public IFormFile? VehiclePhoto { get; set; }
        public int? NoOfVehicle { get; set; }

        public int? AdminId { get; set; }
    }
}
