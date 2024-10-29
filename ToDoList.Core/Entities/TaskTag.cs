using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

[PrimaryKey("TaskId", "TagId")]
public partial class TaskTag
{
    [Key]
    public int TaskId { get; set; }

    [Key]
    public int TagId { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("TagId")]
    [InverseProperty("TaskTags")]
    public virtual Tag Tag { get; set; } = null!;

    [ForeignKey("TaskId")]
    [InverseProperty("TaskTags")]
    public virtual Task Task { get; set; } = null!;
}
