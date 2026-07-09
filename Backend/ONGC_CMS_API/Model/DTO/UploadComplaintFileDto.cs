using Microsoft.AspNetCore.Http;

namespace Model.DTO
{
    public class UploadComplaintFileDto
    {
        public IFormFile? File { get; set; }
        public long ComplaintId { get; set; }
        public string? Type { get; set; }   // resolve / close
    }
}