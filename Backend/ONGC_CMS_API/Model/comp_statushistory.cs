namespace Model
{
    public class comp_statushistory
    {
        public int Id { get; set; }
        public long ComplainId { get; set; }
        public int CompStatusId { get; set; }
        public int IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LMB { get; set; }
        public DateTime LMO { get; set; }
    }
}