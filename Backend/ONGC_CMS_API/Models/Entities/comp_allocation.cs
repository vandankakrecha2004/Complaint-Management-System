using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_allocation")]
public partial class comp_allocation
{
    [Key]
    public long Id { get; set; }

    public long ComplaintId { get; set; }

    [Column(TypeName = "text")]
    public string ComplainReason { get; set; } = null!;

    /// <summary>
    /// DepartmentId
    /// </summary>
    public long AllocatedAgency { get; set; }

    public long AllocatedSubNock { get; set; }

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
