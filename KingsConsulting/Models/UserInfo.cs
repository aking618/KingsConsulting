using System;
using System.Collections.Generic;

namespace KingsConsulting.Models;

public partial class UserInfo
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Passcode { get; set; } = null!;

    public virtual ICollection<OrderInfo> OrderInfos { get; } = new List<OrderInfo>();
}
