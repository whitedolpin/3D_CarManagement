using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public int? ShelfNumber { get; set; }

    public int? Levels { get; set; }

    public int? Position1 { get; set; }

    public bool? Available { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
