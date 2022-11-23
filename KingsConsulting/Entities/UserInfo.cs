using System;
using System.Collections.Generic;

namespace KingsConsulting.Entities;

public partial class UserInfo
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<OrderInfo> OrderInfos { get; } = new List<OrderInfo>();
}
