using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_observationmaster")]
public partial class comp_observationmaster
{
    [Key]
    public int Id { get; set; }

    public string Observation { get; set; } = null!;

    public int SubProblemId { get; set; }

    public sbyte InputType { get; set; }

    [StringLength(45)]
    public string? Max { get; set; }

    [StringLength(45)]
    public string? Min { get; set; }

    [StringLength(45)]
    public string? Unit { get; set; }

    [StringLength(10)]
    public string? ONText { get; set; }

    [StringLength(10)]
    public string? OFFText { get; set; }

    public int? TextLength { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsDelete { get; set; }

    [StringLength(45)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [StringLength(45)]
    public string? LMB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LMO { get; set; }
}
