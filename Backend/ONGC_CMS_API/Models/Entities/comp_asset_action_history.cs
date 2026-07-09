using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_asset_action_history")]
public partial class comp_asset_action_history
{
    [Key]
    public long Id { get; set; }

    public long? ComplaintId { get; set; }

    [StringLength(45)]
    public string? MTConsoleId { get; set; }

    public long? AssetId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActionDate { get; set; }

    [StringLength(100)]
    public string? Location { get; set; }

    [StringLength(45)]
    public string? Source { get; set; }

    public int? ActionTypeId { get; set; }

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
