namespace Model.DTO
{
    public class ComplaintStatusHistoryDto
    {
        public long Id { get; set; }
        public long ComplaintId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime? LMO { get; set; }
    }
}