using System;
using System.Collections.Generic;

namespace KingsConsulting.Models;

public partial class OrderContent
{
    public int OrderContentsId { get; set; }

    public int OrderId { get; set; }

    public int ServiceId { get; set; }

    public int Quanitity { get; set; }

    public virtual OrderInfo Order { get; set; } = null!;

    public virtual ServiceType Service { get; set; } = null!;
}
