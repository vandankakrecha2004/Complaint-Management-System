using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.DTO
{
    public class CreateComplaintDto
    {
        [Required] 
        public long FarmerId { get; set; }
        [Required] 
        public int CompStatusId { get; set; }
        [Required]
        public int ApplicationId { get; set; }
        [Required]
        public int CompSubjectId { get; set; }
        [Required]
        public int SystemTypeId { get; set; }
        [Required]
        public long AllocSubNockId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public DateTime CompGeneratedDate { get; set; }
        [MaxLength(50)]
        public string? DeviceId { get; set; }
        [MaxLength(500)]
        public string? CompDetails { get; set; }
    }
}
