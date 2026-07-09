using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

public partial class comp_replace_visit_reason
{
    [Key]
    public long Id { get; set; }

    public long ComplaintId { get; set; }

    [StringLength(45)]
    public string? MTConsoleId { get; set; }

    public long? AssetId { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? LocalSCADA_orSoftwareIssues { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? CloudSCADA_Configuration { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? EmbeddedSCADA_Configuration { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? Physical_Damage { get; set; }

    [Column("SiteInstallation&Commissioning", TypeName = "bit(1)")]
    public ulong? SiteInstallation_Commissioning { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? PowerSupply_Failure { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? FieldInterface_Failure { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? LocalCommunication_Failure { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? Configuration_Issue { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? CellularCommunication_Failure { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? Production_Failure { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? ProductComponent_Failure { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? ProductDesign_Failure { get; set; }

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
