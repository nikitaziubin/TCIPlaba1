using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCIPlaba1.Models;

public partial class Match
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

    public DateTime Date { get; set; }

    public byte Division { get; set; }

    public byte Stadium { get; set; }

    public virtual Division DivisionNavigation { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual Stadium StadiumNavigation { get; set; } = null!;
}
