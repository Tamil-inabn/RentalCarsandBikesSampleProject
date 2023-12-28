using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class VehicleDetail
{
    public int VehicleId { get; set; }

    public string? VehicleNo { get; set; }

    public string? VehicleName { get; set; }

    public string? VehicleType { get; set; }

    public string? VehiclePhoto { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public int? AdminId { get; set; }

    public int? NoOfVehicle { get; set; }

    public bool? IsActive { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual ICollection<UserBooking> UserBookings { get; } = new List<UserBooking>();
}
