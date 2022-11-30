using System;
using System.Collections.Generic;

namespace KingsConsulting.Entities;

public partial class OrderInfo
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public string ShippingAddress1 { get; set; }

    public string ShippingAddress2 { get; set; }

    public string BillingAddress1 { get; set; }

    public string BillingAddress2 { get; set; }

    public virtual ICollection<OrderContent> OrderContents { get; } = new List<OrderContent>();

    public virtual UserInfo User { get; set; } = null!;
}
