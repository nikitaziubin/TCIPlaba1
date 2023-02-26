using System;
using System.Collections.Generic;

namespace TCIPlaba1;

public partial class Division
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public byte Level { get; set; }

    public bool DivisionOrLeague { get; set; }
}
