using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Service
{
    public int IdService { get; set; }

    public string Name { get; set; } = null!;

    public int IdProvider { get; set; }

    public virtual Provider IdProviderNavigation { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
