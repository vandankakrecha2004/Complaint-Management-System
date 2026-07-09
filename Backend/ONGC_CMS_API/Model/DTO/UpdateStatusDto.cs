using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.DTO
{
    public class UpdateStatusDto
    {
        [Required]
        public int ComplaintId { get; set; }
        [Required]
        public int StatusId { get; set; }

        // Optional (only for assign step)
        [MaxLength(50)]
        public string? AssignedUser { get; set; }

        // Optional (only for assign step)
        public long? AllocDeptId { get; set; }
        [MaxLength(500)]
        public string? ResolveRemarks { get; set; }
        [MaxLength(500)]
        public string? CloseRemarks { get; set; }
    }
}
