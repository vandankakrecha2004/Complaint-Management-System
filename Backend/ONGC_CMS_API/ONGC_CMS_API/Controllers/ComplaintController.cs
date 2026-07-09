using Business;
using DataProvider.MySQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;


//using Microsoft.EntityFrameworkCore;
using Model.DTO;

namespace ONGC_CMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly ComplaintService _service;
        private readonly FtpService _ftpService;

        public ComplaintController(
            ComplaintService service,
            FtpService ftpService)
        {
            _service = service;
            _ftpService = ftpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateComplaintDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AddAsync(dto);

            return Ok(result);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ok = await _service.UpdateStatusAsync(dto);

            if (!ok)
                return NotFound(new { success = false, message = "Complaint not found" });

            return Ok(new
            {
                success = true,
                message = "Status updated successfully"
            });
        }

        [HttpGet("subjects")]
        public async Task<IActionResult> GetSubjects([FromQuery] int? systemTypeId)
        {
            var data = await _service.GetSubjectsAsync(systemTypeId);
            return Ok(data);
        }

        [HttpGet("system-types")]
        public async Task<IActionResult> GetSystemTypes()
        {
            var data = await _service.GetSystemTypesAsync();
            return Ok(data);
        }
        [HttpPost("upload-complaint-file")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadComplaintFile(
            [FromForm] UploadComplaintFileDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File missing");

            // ✅ 1. File Size Validation (5MB)
            const long maxSize = 5 * 1024 * 1024; // 5MB

            if (dto.File.Length > maxSize)
            {
                return BadRequest(new
                {
                    message = "File size cannot be more than 5 MB."
                });
            }

            // ✅ 2. Extension Validation
            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".pdf" };

            var extension = Path.GetExtension(dto.File.FileName)
                                .ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new
                {
                    message = "Only PNG, JPG, JPEG and PDF files are allowed."
                });
            }

            // ✅ 3. Upload file to FTP (ONLY AFTER VALIDATION)
            var result = await _ftpService.UploadComplaintFileAsync(
                dto.ComplaintId,
                dto.Type,
                dto.File);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            var path = result.Data;

            // ✅ 4. Save document record
            var document = new comp_documents
            {
                ComplaintId = dto.ComplaintId,
                SpaceId = 51,
                SpaceRefId = dto.ComplaintId,
                IsClosureDoc = (ulong?)(dto.Type == "close" ? 1UL : 0UL),
                DocName = dto.File.FileName,
                FileName = dto.File.FileName,
                FtpPath = path,
                Version = "",
                CompStatus = dto.Type == "close" ? 7 : 6,
                IsDelete = 0,
                CreatedBy = "3822ef3b5bc1430f9205826ff1c7e644",
                CreatedOn = DateTime.Now,
                LMB = "3822ef3b5bc1430f9205826ff1c7e644",
                LMO = DateTime.Now
            };

            await _service.AddDocumentAsync(document);

            return Ok(new
            {
                Message = "File uploaded & document saved successfully",
                Path = path
            });
        }

        [HttpPost("allocate")]
        public async Task<IActionResult> Allocate([FromBody] AllocateComplaintDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAllocationAsync(dto);

            return Ok(new { message = "Allocation saved successfully" });
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign([FromBody] AssignComplaintDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAssignmentAsync(dto);

            return Ok(new { message = "Assignment saved successfully" });
        }

        [HttpGet("download-file/{complaintId}")]
        public async Task<IActionResult> DownloadFile(long complaintId, [FromQuery] string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return BadRequest("Type is required.");

            var document = await _service.GetDocumentAsync(complaintId, type);

            if (document == null || string.IsNullOrWhiteSpace(document.FtpPath))
                return NotFound("Document not found.");

            var ftpResult = await _ftpService.DownloadFileAsync(document.FtpPath!);

            if (!ftpResult.Success || ftpResult.Data == null)
                return NotFound(ftpResult.Message);

            // 🔥 Detect content type dynamically
            var provider = new FileExtensionContentTypeProvider();

            string contentType = "application/octet-stream"; // default fallback

            if (!string.IsNullOrWhiteSpace(document.FileName))
            {
                provider.TryGetContentType(document.FileName, out contentType);
            }

            return File(
                ftpResult.Data,
                contentType,
                document.FileName ?? "downloaded-file"
            );
        }

        [HttpGet("status-history/{complaintId}")]
        public async Task<IActionResult> GetStatusHistory(long complaintId)
        {
            var data = await _service.GetStatusHistoryAsync(complaintId);

            if (data == null || !data.Any())
                return NotFound("No status history found.");

            return Ok(data);
        }
    }
}