using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCIPlaba1;

public partial class Participant
{
    public int Id { get; set; }
    [Display(Name = "Матч")]
    public int Match { get; set; }
    [Display(Name = "Команда")]
    public int Team { get; set; }
    [Display(Name = "Роль Команди")]
    public byte TeamRole { get; set; }
    [Display(Name = "Голи")]
    public byte Goals { get; set; }
}
