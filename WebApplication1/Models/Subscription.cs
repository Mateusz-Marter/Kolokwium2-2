using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Subscription
{
    public int IdSubscription { get; set; }

    public int IdUser { get; set; }

    public int IdService { get; set; }

    public virtual ICollection<Commitment> Commitments { get; set; } = new List<Commitment>();

    public virtual Service IdServiceNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
