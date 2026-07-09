using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_asset_status_history")]
public partial class comp_asset_status_history
{
    [Key]
    public long Id { get; set; }

    public long? ComplaintId { get; set; }

    public long? AssetId { get; set; }

    [StringLength(45)]
    public string? AssetType { get; set; }

    [StringLength(45)]
    public string? SerialNo { get; set; }

    [StringLength(45)]
    public string? ModelNo { get; set; }

    [StringLength(45)]
    public string? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [StringLength(45)]
    public string? Location { get; set; }

    public int? OldAstLocId { get; set; }

    public int? OldAstSubLocId { get; set; }

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
