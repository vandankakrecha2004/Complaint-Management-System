using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_generation")]
[Index("Id", Name = "Id_UNIQUE", IsUnique = true)]
public partial class comp_generation
{
    [Key]
    public long Id { get; set; }

    [StringLength(45)]
    public string ComplaintUId { get; set; } = null!;

    public long FarmerId { get; set; }

    public long? WellId { get; set; }

    public long? AssetId { get; set; }

    public long? AreaId { get; set; }

    public long? GGSId { get; set; }

    public long? Location { get; set; }

    [StringLength(45)]
    public string? Comp { get; set; }

    [StringLength(45)]
    public string? SolutionType { get; set; }

    public int ApplicationId { get; set; }

    [StringLength(512)]
    public string? ComplainSubject { get; set; }

    public int CompSubjectId { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsInWarranty { get; set; }

    public int SystemTypeId { get; set; }

    public int? CompTypeId { get; set; }

    [StringLength(45)]
    public string? CSNumber { get; set; }

    [StringLength(45)]
    public string? DeviceId { get; set; }

    [StringLength(45)]
    public string? SiteId { get; set; }

    [StringLength(45)]
    public string? OrgId { get; set; }

    public int? ProjectId { get; set; }

    public int? SoultionId { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsVisitRequired { get; set; }

    public int? DefectTypeId { get; set; }

    public int? ActionTypeId { get; set; }

    public int? ReasonTypeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CompGeneratedDate { get; set; }

    [Column(TypeName = "text")]
    public string? CompDetectionLocation { get; set; }

    [Column(TypeName = "text")]
    public string? CompDetails { get; set; }

    [Column(TypeName = "text")]
    public string? ClosingRemarks { get; set; }

    [Column(TypeName = "text")]
    public string? SiteSpecificObservation { get; set; }

    public int? CompStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClosingDate { get; set; }

    [StringLength(45)]
    public string? CloseBy { get; set; }

    public long? GenDeptId { get; set; }

    public long? AllocDeptId { get; set; }

    public long AllocSubNockId { get; set; }

    [StringLength(45)]
    public string? AllocatedDept { get; set; }

    [StringLength(45)]
    [MySqlCharSet("armscii8")]
    [MySqlCollation("armscii8_general_ci")]
    public string? AssignedUserId { get; set; }

    [StringLength(45)]
    public string? AssignedUser { get; set; }

    [StringLength(45)]
    public string? SiteVisitedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SiteVisitedDate { get; set; }

    [StringLength(45)]
    public string? ResolvedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ResolvedDate { get; set; }

    [Column(TypeName = "text")]
    public string? ResolvedRemarks { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsDelete { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsAssetReplaced { get; set; }

    public int? IssueTypeId { get; set; }

    [StringLength(45)]
    public string? CompGeneratorName { get; set; }

    [StringLength(15)]
    public string? MobileNo { get; set; }

    [StringLength(45)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(45)]
    public string? LMB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LMO { get; set; }

    public int DistrictId { get; set; }

    [StringLength(20)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? PumpCapacity { get; set; }

    [StringLength(45)]
    public string? DeviceSerialNo { get; set; }
}
