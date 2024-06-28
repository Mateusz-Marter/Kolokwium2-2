using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
