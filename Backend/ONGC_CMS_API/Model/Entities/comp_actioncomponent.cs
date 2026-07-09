using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

public partial class comp_actioncomponent
{
    [Key]
    public long Id { get; set; }

    public int? ActionTypeId { get; set; }

    public int? ComponentTypeId { get; set; }

    [StringLength(45)]
    public string? Observation { get; set; }

    public long? ComplaintId { get; set; }

    public long? SiteVisitId { get; set; }

    [StringLength(45)]
    public string? MTConsoleId { get; set; }

    [StringLength(45)]
    public string? OldAssetId { get; set; }

    [StringLength(45)]
    public string? OldMake { get; set; }

    [StringLength(45)]
    public string? OldModelNo { get; set; }

    [StringLength(45)]
    public string? OldSerialNo { get; set; }

    [StringLength(45)]
    public string? OldConfigurationFile { get; set; }

    [StringLength(45)]
    public string? OldAssetStatus { get; set; }

    [StringLength(45)]
    public string? NewMake { get; set; }

    [StringLength(45)]
    public string? NewModelNo { get; set; }

    [StringLength(45)]
    public string? NewSerialNo { get; set; }

    [Precision(26, 6)]
    public decimal? OldImpKwh { get; set; }

    [Precision(26, 6)]
    public decimal? OldExpKwh { get; set; }

    [Precision(26, 6)]
    public decimal? NewImpKwh { get; set; }

    [Precision(26, 6)]
    public decimal? NewExpKwh { get; set; }

    [StringLength(150)]
    public string? NotTakenReason { get; set; }

    [StringLength(45)]
    public string? AssetType { get; set; }

    [StringLength(45)]
    public string? MeterType { get; set; }

    [StringLength(45)]
    public string? OldAssetLocation { get; set; }

    public int? OldAstLocId { get; set; }

    public int? OldAstSubLocId { get; set; }

    [StringLength(45)]
    public string? NewAssetLocation { get; set; }

    public int? NewAstLocId { get; set; }

    [StringLength(45)]
    public string? Latitude { get; set; }

    [StringLength(45)]
    public string? Longitude { get; set; }

    [Column(TypeName = "text")]
    public string? SiteSpecificRemarks { get; set; }

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
