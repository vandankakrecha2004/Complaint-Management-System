namespace Model.DTO
{
    public class AllocateComplaintDto
    {
        public long ComplaintId { get; set; }
        public string? ComplainReason { get; set; }
        public long? AllocatedAgency { get; set; }
        public long? AllocatedSubNock { get; set; }
        public string? CreatedBy { get; set; }
    }
}