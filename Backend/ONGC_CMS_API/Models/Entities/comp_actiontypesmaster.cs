using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_actiontypesmaster")]
public partial class comp_actiontypesmaster
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string? Name { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsDelete { get; set; }

    [StringLength(45)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [StringLength(45)]
    public string? LMB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LMO { get; set; }
}
