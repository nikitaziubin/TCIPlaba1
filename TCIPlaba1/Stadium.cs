using System;
using System.Collections.Generic;

namespace TCIPlaba1;

public partial class Stadium
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? Capacity { get; set; }

    public int? MaxCapacity { get; set; }
}
