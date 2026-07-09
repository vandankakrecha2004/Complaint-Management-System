using DataProvider.MySQL.Config;
using IDataProvider.MySQL;
using Microsoft.EntityFrameworkCore;
using Model.DTO;

namespace DataProvider.MySQL
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly IiotIntelliscadaDbContext _context;

        public ComplaintRepository(IiotIntelliscadaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ComplaintListDto>> GetAllAsync()
        {
            var result = await (from cg in _context.comp_generations

                                join cs in _context.comp_compstatusmasters
                                on cg.CompStatus equals cs.Id into csj
                                from cs in csj.DefaultIfEmpty()

                                join st in _context.comp_systemtypesmasters
                                on cg.SystemTypeId equals st.Id into stj
                                from st in stj.DefaultIfEmpty()

                                join sub in _context.comp_complaintsubjectmasters
                                on cg.CompSubjectId equals sub.Id into subj
                                from sub in subj.DefaultIfEmpty()

                                orderby cg.Id descending

                                select new ComplaintListDto
                                {
                                    Id = cg.Id,
                                    ComplaintUId = cg.ComplaintUId,
                                    AllocDeptId = cg.AllocDeptId,
                                    AssignedUser = cg.AssignedUser,
                                    CreatedOn = cg.CreatedOn.ToString("dd-MM-yyyy HH:mm:ss"),
                                    Subject = sub.SubjectName,
                                    ComplaintStatus = cs.Name,
                                    CompStatusId = cg.CompStatus,
                                    SystemType = st.Name,
                                    FarmerId = cg.FarmerId,
                                    DistrictId = cg.DistrictId,
                                    CompGeneratedDate = cg.CompGeneratedDate.ToString("dd-MM-yyyy"),
                                    compDetails = cg.CompDetails,
                                }).ToListAsync();

            return result;
        }

        public async Task<comp_generation> AddAsync(CreateComplaintDto dto)
        {
            var entity = new comp_generation
            {
                FarmerId = dto.FarmerId,
                ApplicationId = dto.ApplicationId,
                CompSubjectId = dto.CompSubjectId,
                CompTypeId = dto.CompSubjectId, // same as subject 
                SystemTypeId = dto.SystemTypeId,
                AllocSubNockId = dto.AllocSubNockId,
                DistrictId = dto.DistrictId,
                CompGeneratedDate = dto.CompGeneratedDate,
                CompStatus = dto.CompStatusId,
                DeviceId = dto.DeviceId,
                CompDetails = dto.CompDetails,


                // Auto fields
                ComplaintUId = $"{DateTime.Now:ddMMyyyy}_{new Random().Next(10000, 99999)}",
                CreatedOn = DateTime.Now,
                IsDelete = 0,
                IsInWarranty = 0,
                Comp = "on",
                ProjectId = 0,
                SoultionId = 0,

            };

            _context.comp_generations.Add(entity);
            await _context.SaveChangesAsync();

            // 🔥 INSERT HISTORY FOR CREATE
            await InsertStatusHistoryAsync(
                entity.Id,
                entity.CompStatus,
                entity.CreatedOn
            );

            return entity;
        }

        public async Task<bool> UpdateStatusAsync(UpdateStatusDto dto)
        {
            var complaint = await _context.comp_generations
                                          .FirstOrDefaultAsync(x => x.Id == dto.ComplaintId);

            if (complaint == null)
                return false;

            // 🔥 Update status
            complaint.CompStatus = dto.StatusId;
            complaint.LMO = DateTime.Now;

            // 🔥 Only update AssignedUser
            if (!string.IsNullOrEmpty(dto.AssignedUser))
            {
                complaint.AssignedUser = dto.AssignedUser;
                complaint.AssignedUserId = "0c1c15f16dae49109203dbae5c4c9edf";  // for arjun yadav static user id taken bcz fix user
            }

            // Only for allocate Allocate Dept
            if (dto.AllocDeptId != null)
            {
                complaint.AllocDeptId = dto.AllocDeptId;
                complaint.GenDeptId = dto.AllocDeptId;
            }

            // Onyl for add Resolve Remarks
            if (!string.IsNullOrEmpty(dto.ResolveRemarks))
            {
                complaint.ResolvedRemarks = dto.ResolveRemarks;
            }

            // Onyl for add Close Remarks
            if (!string.IsNullOrEmpty(dto.CloseRemarks))
            {
                complaint.ClosingRemarks = dto.CloseRemarks;
            }

            await _context.SaveChangesAsync();

            // 🔥 INSERT HISTORY FOR STATUS CHANGE
            await InsertStatusHistoryAsync(
                complaint.Id,
                complaint.CompStatus,
                complaint.CreatedOn
            );

            return true;
        }

        public async Task<List<SubjectDto>> GetSubjectsAsync(int? systemTypeId)
        {
            var query = _context.comp_complaintsubjectmasters
                .Where(x => x.IsDelete == 0);

            if (systemTypeId.HasValue)
                query = query.Where(x => x.STypeId == systemTypeId.Value);

            return await query
                .Select(x => new SubjectDto
                {
                    Id = x.Id,
                    SubjectName = x.SubjectName
                })
                .ToListAsync();
        }

        public async Task<List<SystemTypeDto>> GetSystemTypesAsync()
        {
            return await _context.comp_systemtypesmasters
                .Where(x => x.IsDelete == 0)
                .Select(x => new SystemTypeDto
                {
                    Id = x.Id,
                    SystemName = x.Name
                })
                .ToListAsync();
        }

        public async Task AddDocumentAsync(comp_documents doc)
        {
            _context.comp_documents.Add(doc);
            await _context.SaveChangesAsync();
        }

        public async Task InsertStatusHistoryAsync(
    long complaintId,
    int? statusId,
    DateTime complaintCreatedOn)
        {
            if (statusId == null)
                return;   // safety

            var history = new comp_statushistory
            {
                ComplainId = complaintId,   // DB column is int
                CompStatusId = statusId.Value,
                IsDelete = 0,
                CreatedBy = "6f26d7fab77a4c1b8957215b65c9dfed",
                CreatedOn = complaintCreatedOn,
                LMB = "6f26d7fab77a4c1b8957215b65c9dfed",
                LMO = DateTime.Now
            };

            _context.comp_statushistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task AddAllocationAsync(AllocateComplaintDto dto)
        {
            var entity = new comp_allocation
            {
                ComplaintId = dto.ComplaintId,

                ComplainReason = dto.ComplainReason ?? "",

                AllocatedAgency = dto.AllocatedAgency ?? -1,

                AllocatedSubNock = dto.AllocatedSubNock ?? -1,

                IsDelete = 0,

                CreatedBy = dto.CreatedBy ?? "6f26d7fab77a4c1b8957215b65c9dfed",

                CreatedOn = DateTime.Now,

                LMB = dto.CreatedBy ?? "6f26d7fab77a4c1b8957215b65c9dfed",

                LMO = DateTime.Now
            };

            _context.comp_allocations.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAssignmentAsync(AssignComplaintDto dto)
        {
            var entity = new comp_assignment
            {
                ComplaintId = dto.ComplaintId,
                AssignmentTo = dto.AssignmentTo,
                Analysis = dto.Analysis ?? "Default Assign",
                Description = dto.Description ?? "Default Assign",
                IsVisitRequired = 0,
                IsDelete = 0,
                CreatedBy = dto.CreatedBy ?? "6f26d7fab77a4c1b8957215b65c9dfed",
                CreatedOn = DateTime.Now,
                LMB = dto.CreatedBy ?? "6f26d7fab77a4c1b8957215b65c9dfed",
                LMO = DateTime.Now
            };

            _context.comp_assignments.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<comp_documents?> GetDocumentAsync(long complaintId, string type)
        {
            var isClosure = type.Equals("close", StringComparison.OrdinalIgnoreCase)
                ? 1UL
                : 0UL;

            return await _context.comp_documents
                .Where(x =>
                    x.ComplaintId == complaintId &&
                    x.IsClosureDoc == isClosure &&
                    x.IsDelete == 0)
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ComplaintStatusHistoryDto>> GetStatusHistoryAsync(long complaintId)
        {
            var result = await (from sh in _context.comp_statushistories

                                join sm in _context.comp_compstatusmasters
                                on sh.CompStatusId equals sm.Id

                                where sh.ComplainId == complaintId
                                      && sh.IsDelete == 0

                                orderby sh.Id ascending

                                select new ComplaintStatusHistoryDto
                                {
                                    Id = sh.Id,
                                    ComplaintId = sh.ComplainId,
                                    StatusId = sh.CompStatusId,
                                    StatusName = sm.Name,
                                    LMO = sh.LMO,
                                }).ToListAsync();

            return result;
        }
    }
}