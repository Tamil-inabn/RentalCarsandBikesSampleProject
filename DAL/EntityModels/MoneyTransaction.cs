using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class MoneyTransaction
{
    public int TransactionId { get; set; }

    public string? UserName { get; set; }

    public string? TotalDays { get; set; }

    public string? TransactionAmount { get; set; }

    public string? VehicleName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public int? BookingId { get; set; }

    public bool? IsActive { get; set; }

    public int? UserId { get; set; }

    public virtual UserBooking? Booking { get; set; }
}
