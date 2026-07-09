using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

public partial class comp_comment
{
    [Key]
    public long Id { get; set; }

    [Column(TypeName = "text")]
    public string? Comment { get; set; }

    public long ComplaintId { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsDelete { get; set; }

    [StringLength(45)]
    public string? CommentedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CommentedOn { get; set; }

    [StringLength(45)]
    public string? LMB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LMO { get; set; }
}
