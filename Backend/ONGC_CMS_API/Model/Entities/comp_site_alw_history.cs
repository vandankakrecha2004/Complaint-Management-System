using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_site_alw_history")]
public partial class comp_site_alw_history
{
    [Key]
    public long Id { get; set; }

    [Column(TypeName = "text")]
    public string CmpIds { get; set; } = null!;

    [Column(TypeName = "text")]
    public string ALW_Type_Ids { get; set; } = null!;

    [Column(TypeName = "text")]
    public string ALW_Costs { get; set; } = null!;

    [Column(TypeName = "text")]
    public string Assg_SVIds { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? CsNo_Ids { get; set; }

    [Column(TypeName = "text")]
    public string? Site_Ids { get; set; }

    public int? ProjectId { get; set; }

    [StringLength(45)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(45)]
    public string LMB { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LMO { get; set; }

    public sbyte? Status { get; set; }

    [Column(TypeName = "text")]
    public string? Reason { get; set; }

    [StringLength(45)]
    public string? ReasonABy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReasonAOn { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsDelete { get; set; }
}
