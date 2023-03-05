using System;
using System.Collections.Generic;

namespace TCIPlaba1.Models;

public partial class England
{
    public DateTime? Date { get; set; }

    public short? Season { get; set; }

    public string? Home { get; set; }

    public string? Visitor { get; set; }

    public string? Ft { get; set; }

    public byte? Hgoal { get; set; }

    public byte? Vgoal { get; set; }

    public byte? Division { get; set; }

    public byte? Tier { get; set; }

    public byte? Totgoal { get; set; }

    public short? Goaldif { get; set; }

    public string? Result { get; set; }
}
