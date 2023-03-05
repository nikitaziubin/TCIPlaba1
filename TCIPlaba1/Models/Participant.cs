using System;
using System.Collections.Generic;

namespace TCIPlaba1.Models;

public partial class Participant
{
    public int Id { get; set; }

    public int Match { get; set; }

    public int Team { get; set; }

    public byte TeamRole { get; set; }

    public byte Goals { get; set; }

    public virtual Match MatchNavigation { get; set; } = null!;

    public virtual Team TeamNavigation { get; set; } = null!;

    public virtual TeamRole TeamRoleNavigation { get; set; } = null!;
}
