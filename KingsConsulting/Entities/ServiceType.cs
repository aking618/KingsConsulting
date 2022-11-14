using System;
using System.Collections.Generic;

namespace KingsConsulting.Entities;

public partial class ServiceType
{
    public int ServiceTypeId { get; set; }

    public string ServiceName { get; set; } = null!;

    public virtual ICollection<OrderContent> OrderContents { get; } = new List<OrderContent>();
}
