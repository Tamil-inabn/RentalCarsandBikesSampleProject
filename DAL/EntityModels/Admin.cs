using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class Admin
{
    public int AdminId { get; set; }

    public string? AdminName { get; set; }

    public string? EmailId { get; set; }

    public string? Password { get; set; }

    public int? RollId { get; set; }

    public virtual ICollection<VehicleDetail> VehicleDetails { get; } = new List<VehicleDetail>();
}
