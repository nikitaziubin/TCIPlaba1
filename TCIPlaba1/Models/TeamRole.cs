using System;
using System.Collections.Generic;

namespace TCIPlaba1.Models;

public partial class TeamRole
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; } = new List<Participant>();
}
