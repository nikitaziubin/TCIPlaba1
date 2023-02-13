using System;
using System.Collections.Generic;

namespace TCIPlaba1;

public partial class Participant
{
    public int Id { get; set; }

    public int Match { get; set; }

    public int Team { get; set; }

    public byte TeamRole { get; set; }

    public byte Goals { get; set; }
}
