using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_readingsnapshot")]
public partial class comp_readingsnapshot
{
    [Key]
    public long Id { get; set; }

    public long ComplainId { get; set; }

    [StringLength(45)]
    public string? RegisterAddress { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Timestamp { get; set; }

    [Precision(10, 2)]
    public decimal? Value { get; set; }

    [StringLength(20)]
    public string? Spam { get; set; }

    [StringLength(45)]
    public string? SoftDeviceId { get; set; }

    [StringLength(45)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [StringLength(45)]
    public string? LMB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LMO { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsDelete { get; set; }
}
