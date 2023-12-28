namespace RentalCarsandBikes.Models
{
    public class BookingTransationViewPL
    {
        public int TransactionId { get; set; }

        public string? UserName { get; set; }

        public int? TotalDays { get; set; }

        public decimal? TransactionAmount { get; set; }

        public string? VehicleName { get; set; }

        public int? BookingId { get; set; }
    }
}
