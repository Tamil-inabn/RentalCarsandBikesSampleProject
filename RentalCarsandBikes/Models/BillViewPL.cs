namespace RentalCarsandBikes.Models
{
    public class BillViewPL
    {
        public int TransactionId { get; set; }
        public string? Name { get; set; }
        public string? TransactionAmount { get; set; }
        public string? VehicleName { get; set; }
        public int? BookingId { get; set; }
        public string? TotalDays { get; set; }
    }
}
