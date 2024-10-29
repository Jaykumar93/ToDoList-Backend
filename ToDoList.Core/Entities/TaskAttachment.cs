using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class TaskAttachment
{
    [Key]
    public int AttachmentId { get; set; }

    public int TaskId { get; set; }

    [StringLength(255)]
    public string FileName { get; set; } = null!;

    [StringLength(100)]
    public string FileType { get; set; } = null!;

    [StringLength(500)]
    public string FilePath { get; set; } = null!;

    public long FileSize { get; set; }

    public DateTime UploadedAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskAttachments")]
    public virtual Task Task { get; set; } = null!;
}
