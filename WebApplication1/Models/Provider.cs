using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Provider
{
    public int IdProvider { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
