using System;
using System.Collections.Generic;

namespace KingsConsulting.Entities;

public partial class OrderInfo
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual ICollection<OrderContent> OrderContents { get; } = new List<OrderContent>();

    public virtual UserInfo User { get; set; } = null!;
}
