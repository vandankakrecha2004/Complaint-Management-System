public class ComplaintListDto
{
    public long Id { get; set; }

    public string? ComplaintUId { get; set; }

    public string? Subject { get; set; }

    public string? ComplaintStatus { get; set; }
    public int? CompStatusId { get; set; }
    public long? AllocDeptId { get; set; }
    public string? AssignedUser { get; set; }

    public string? ComplaintType { get; set; }

    public string? SystemType { get; set; }

    public string? CreatedOn { get; set; }
    public string? CompGeneratedDate { get; set; }
    public long FarmerId { get; set; }
    public int DistrictId { get; set; }

    public string? compDetails { get; set; }
}