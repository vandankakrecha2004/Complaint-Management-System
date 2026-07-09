using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_assignment")]
public partial class comp_assignment
{
    [Key]
    public long Id { get; set; }

    public long ComplaintId { get; set; }

    /// <summary>
    /// UserId
    /// </summary>
    [StringLength(45)]
    public string? AssignmentTo { get; set; }

    [Column(TypeName = "text")]
    public string? Analysis { get; set; }

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsVisitRequired { get; set; }

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
