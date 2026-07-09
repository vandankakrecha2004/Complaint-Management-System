using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.MySQL;

[Table("comp_notificationsqueue")]
public partial class comp_notificationsqueue
{
    [Key]
    public long ID { get; set; }

    [StringLength(45)]
    public string? UserId { get; set; }

    public long? ComplaintId { get; set; }

    [StringLength(45)]
    public string? EmailId { get; set; }

    [Column(TypeName = "text")]
    public string? Template { get; set; }

    [Column(TypeName = "bit(1)")]
    public ulong? IsProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InsertedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProcessedOn { get; set; }
}
