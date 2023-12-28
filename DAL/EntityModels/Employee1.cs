using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class Employee1
{
    public int EId { get; set; }

    public string? EName { get; set; }

    public string? Gender { get; set; }

    public decimal? ESalary { get; set; }

    public long? EMobile { get; set; }

    public string? Address { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Department> Departments { get; } = new List<Department>();
}
