namespace RentalCarsandBikes.Models
{
    public class PaymentViewPL
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public long? MobileNo { get; set; }
        public string? Email { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? DropDate { get; set; }
        public string? TransactionAmount { get; set; }
        public string? VehicleName { get; set; }
        public int? BookingId { get; set; }
        public string? TotalDays { get; set; }
    }
}
