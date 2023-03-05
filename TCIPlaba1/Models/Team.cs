using System;
using System.Collections.Generic;

namespace TCIPlaba1.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Club { get; set; }

    public virtual Club ClubNavigation { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; } = new List<Participant>();
}
