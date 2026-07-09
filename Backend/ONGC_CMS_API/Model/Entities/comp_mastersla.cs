using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_mastersla")]
[Index("Id", Name = "Id_UNIQUE", IsUnique = true)]
public partial class comp_mastersla
{
    [Key]
    public int Id { get; set; }

    public int StatusId { get; set; }

    [StringLength(45)]
    public string StatusName { get; set; } = null!;

    public int SLADuration { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(45)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LMO { get; set; }

    [StringLength(45)]
    public string LMB { get; set; } = null!;

    [Column(TypeName = "bit(1)")]
    public ulong IsDelete { get; set; }
}
