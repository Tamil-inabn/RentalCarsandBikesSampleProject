using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class State
{
    public int StateId { get; set; }

    public string? StateName { get; set; }

    public virtual ICollection<City> Cities { get; } = new List<City>();
}
