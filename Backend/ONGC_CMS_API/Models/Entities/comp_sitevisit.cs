using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_sitevisit")]
[Index("CreatedOn", "IsAlwDone", "IsSiteVisited", "IsDelete", "CreatedBy", Name = "SiteVisit_Index")]
public partial class comp_sitevisit
{
    [Key]
    public long Id { get; set; }

    [StringLength(45)]
    public string? SiteTitle { get; set; }

    public int? VisitTypeId { get; set; }

    public long? ComplaintId { get; set; }

    [StringLength(45)]
    public string? SiteCLatitude { get; set; }

    [StringLength(45)]
    public string? SiteCLongitude { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsDelete { get; set; }

    [StringLength(45)]
    public string? SVisitedLatitude { get; set; }

    [StringLength(45)]
    public string? SVisitedLongitude { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsSiteVisited { get; set; }

    [StringLength(45)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [StringLength(45)]
    public string? LMB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LMO { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsAlwDone { get; set; }

    public long? AlwId { get; set; }
}
