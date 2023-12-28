using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class BookingCancel
{
    public int CancelId { get; set; }

    public string? UserName { get; set; }

    public string? Reason { get; set; }

    public string? VehicleName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public int? UserId { get; set; }

    public bool? IsActive { get; set; }

    public virtual UserRegister? User { get; set; }
}
