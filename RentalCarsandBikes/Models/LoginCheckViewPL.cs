namespace RentalCarsandBikes.Models
{
    public class LoginCheckViewPL
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public int? RollId { get; set; }
    }
    enum RollId
    {
        User = 1,
        Admin = 2
    }
}
