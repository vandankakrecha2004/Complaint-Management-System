using DataProvider.MySQL;
using Model.DTO;

namespace IDataProvider.MySQL
{
    public interface IComplaintRepository
    {
        Task<List<ComplaintListDto>> GetAllAsync();
        Task<comp_generation> AddAsync(CreateComplaintDto dto);
        Task<bool> UpdateStatusAsync(UpdateStatusDto dto);

        //get subject and id for dropdown
        Task<List<SubjectDto>> GetSubjectsAsync(int? systemTypeId);

        // get system and id for dropdown
        Task<List<SystemTypeDto>> GetSystemTypesAsync();

        // insert status history table
        Task InsertStatusHistoryAsync(long complaintId, int? statusId, DateTime complaintCreatedOn);

        // add in allocation table
        Task AddAllocationAsync(AllocateComplaintDto dto);
        // add in assign table
        Task AddAssignmentAsync(AssignComplaintDto dto);
        // add in document table
        Task AddDocumentAsync(comp_documents doc);
        // download 
        Task<comp_documents?> GetDocumentAsync(long complaintId, string type);
        // get status history by complainId
        Task<List<ComplaintStatusHistoryDto>> GetStatusHistoryAsync(long complaintId);
    }
}