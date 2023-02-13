using System;
using System.Collections.Generic;

namespace TCIPlaba1;

public partial class Club
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public string Address { get; set; } = null!;
}
