namespace RentalCarsandBikes.Models
{
    public class BookingCancelViewPL
    {
        public int CancelId { get; set; }

        public string? UserName { get; set; }

        public string? Reason { get; set; }

        public string? VehicleName { get; set; }

        public int? UserId { get; set; }

        public bool? IsActive { get; set; }
    }
}
