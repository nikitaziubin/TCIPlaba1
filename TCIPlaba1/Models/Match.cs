using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TCIPlaba1;

public partial class Match
{
    public int Id { get; set; }
    [Display(Name = "Дата")]
    public DateTime Date { get; set; }
    [Display(Name = "Дівізіон")]
    public byte Division { get; set; }
}
