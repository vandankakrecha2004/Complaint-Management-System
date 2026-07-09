using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

public partial class comp_document
{
    [Key]
    public long Id { get; set; }

    public long ComplaintId { get; set; }

    public int SpaceId { get; set; }

    public long SpaceRefId { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsClosureDoc { get; set; }

    [StringLength(45)]
    public string? DocName { get; set; }

    [StringLength(250)]
    public string? FileName { get; set; }

    [StringLength(255)]
    public string? FtpPath { get; set; }

    [StringLength(10)]
    public string? Version { get; set; }

    public int? CompStatus { get; set; }

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
