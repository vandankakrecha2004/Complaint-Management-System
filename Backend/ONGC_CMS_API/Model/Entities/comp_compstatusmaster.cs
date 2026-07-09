using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_compstatusmaster")]
[Index("Id", Name = "Id_UNIQUE", IsUnique = true)]
public partial class comp_compstatusmaster
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "bit(1)")]
    public ulong IsDelete { get; set; }

    [StringLength(150)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(150)]
    public string LMB { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LMO { get; set; }
}
