using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Commitment
{
    public int IdCommitment { get; set; }

    public DateTime PaymentDeadline { get; set; }

    public float LeftToPay { get; set; }

    public int IdSubscription { get; set; }

    public virtual Subscription IdSubscriptionNavigation { get; set; } = null!;
}
