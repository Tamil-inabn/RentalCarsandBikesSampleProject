namespace RentalCarsandBikes.Models
{
    public class AddVehicleViewPL
    {
        public int VehicleId { get; set; }

        public string? VehicleNo { get; set; }

        public string? VehicleName { get; set; }

        public string? VehicleType { get; set; }

        public IFormFile? VehiclePhoto { get; set; }
        public string? VehiclePhotos { get; set; }

        public int? NoOfVehicle { get; set; }

        public int? AdminId { get; set; }
    }
}
