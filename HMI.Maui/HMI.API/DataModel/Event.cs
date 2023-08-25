using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HMI.API.DataModel;

[Table("events")]
public partial class Event
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    public string? UserName { get; set; }

    public string? UserSurname { get; set; }

    [Column("date", TypeName = "text")]
    public DateTime? Date { get; set; }

    [Column("allarms")]
    public string? Allarms { get; set; }
}
