using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class UserBooking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public string? VehicleNo { get; set; }

    public DateTime? PickUpDate { get; set; }

    public DateTime? DropDate { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public int? VehicleId { get; set; }

    public string? Name { get; set; }

    public long? MobileNo { get; set; }

    public string? Email { get; set; }

    public string? VehicleName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<MoneyTransaction> MoneyTransactions { get; } = new List<MoneyTransaction>();

    public virtual UserRegister? User { get; set; }

    public virtual VehicleDetail? Vehicle { get; set; }
}
