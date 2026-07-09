using DataProvider.MySQL;
using FluentFTP;
using IDataProvider.MySQL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.DTO;
using System.Security.Authentication;

namespace Business
{
    public class ComplaintService
    {
        private readonly IComplaintRepository _repo;

        public ComplaintService(IComplaintRepository repo)
        {
            _repo = repo;
        }

        public Task<List<ComplaintListDto>> GetAllAsync()
    => _repo.GetAllAsync();

        public Task<comp_generation> AddAsync(CreateComplaintDto dto)
    => _repo.AddAsync(dto);

        public Task<bool> UpdateStatusAsync(UpdateStatusDto dto)
    => _repo.UpdateStatusAsync(dto);

        public Task<List<SubjectDto>> GetSubjectsAsync(int? systemTypeId)
    => _repo.GetSubjectsAsync(systemTypeId);

        public Task<List<SystemTypeDto>> GetSystemTypesAsync()
    => _repo.GetSystemTypesAsync();

        public Task AddAllocationAsync(AllocateComplaintDto dto)
    => _repo.AddAllocationAsync(dto);

        public Task AddAssignmentAsync(AssignComplaintDto dto)
     => _repo.AddAssignmentAsync(dto);

        public Task AddDocumentAsync(comp_documents doc)
        => _repo.AddDocumentAsync(doc);

        public Task<comp_documents?> GetDocumentAsync(long complaintId, string type)
    => _repo.GetDocumentAsync(complaintId, type);
        public Task<List<ComplaintStatusHistoryDto>> GetStatusHistoryAsync(long complaintId)
    => _repo.GetStatusHistoryAsync(complaintId);

    }


    public class FtpService
    {
        private readonly string _host;
        private readonly string _username;
        private readonly string _password;
        private readonly string _basePath;

        public FtpService(IConfiguration configuration)
        {
            _host = configuration["FtpSettings:Host"]!;
            _username = configuration["FtpSettings:Username"]!;
            _password = configuration["FtpSettings:Password"]!;
            _basePath = configuration["FtpSettings:BasePath"]!; // /CMS
        }

        private AsyncFtpClient CreateClient()
        {
            var client = new AsyncFtpClient(_host, _username, _password);

            client.Config.EncryptionMode = FtpEncryptionMode.Explicit;
            client.Config.SslProtocols = SslProtocols.Tls12;
            client.Config.ValidateAnyCertificate = true;

            return client;
        }

        /// <summary>
        /// Upload file → CMS/resolve OR CMS/close
        /// Filename → ComplaintId_resolve.ext
        /// </summary>

        public class ServiceResult<T>
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public T? Data { get; set; }
        }


        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB
        public async Task<ServiceResult<string>> UploadComplaintFileAsync(
    long complaintId,
    string? type,
    IFormFile file)
        {
            var result = new ServiceResult<string>();

            if (file == null || file.Length == 0)
            {
                result.Success = false;
                result.Message = "File is empty.";
                return result;
            }

            if (file.Length > MaxFileSize)
            {
                result.Success = false;
                result.Message = "File size exceeds 5MB limit.";
                return result;
            }

            var extension = Path.GetExtension(file.FileName)
                                .ToLowerInvariant();


            var folder = type?.Equals("close", StringComparison.OrdinalIgnoreCase) == true
                ? "close"
                : "resolve";

            var safeFileName = $"{complaintId}_{folder}{extension}";
            var fullPath = $"{_basePath}/{folder}/{safeFileName}";

            try
            {
                using var client = CreateClient();
                await client.Connect();

                await client.CreateDirectory($"{_basePath}/{folder}", true);

                using var stream = file.OpenReadStream();
                await client.UploadStream(stream, fullPath, FtpRemoteExists.Overwrite);

                await client.Disconnect();

                result.Success = true;
                result.Message = "File uploaded successfully.";
                result.Data = fullPath;

                return result;
            }
            catch
            {
                result.Success = false;
                result.Message = "File upload failed.";
                return result;
            }
        }

        public async Task<ServiceResult<byte[]>> DownloadFileAsync(string ftpPath)
        {
            var result = new ServiceResult<byte[]>();

            if (string.IsNullOrWhiteSpace(ftpPath))
            {
                result.Success = false;
                result.Message = "Invalid FTP path.";
                return result;
            }

            try
            {
                using var client = CreateClient();
                await client.Connect();

                using var ms = new MemoryStream();

                bool downloaded = await client.DownloadStream(ms, ftpPath);

                await client.Disconnect();

                if (!downloaded)
                {
                    result.Success = false;
                    result.Message = "File not found on FTP server.";
                    return result;
                }

                result.Success = true;
                result.Data = ms.ToArray();
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "File download failed: " + ex.Message;
                return result;
            }
        }

    }
}