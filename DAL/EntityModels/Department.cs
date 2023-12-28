using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class Department
{
    public int DId { get; set; }

    public string? DName { get; set; }

    public DateTime? DateOfJoin { get; set; }

    public int EId { get; set; }

    public virtual Employee1 EIdNavigation { get; set; } = null!;
}
