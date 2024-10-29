using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

[Index("TaskId", "CreatedAt", Name = "IX_TaskActivities_Task")]
public partial class TaskActivity
{
    [Key]
    public int ActivityId { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }

    [StringLength(50)]
    public string ActivityType { get; set; } = null!;

    public string? ActivityData { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskActivities")]
    public virtual Task Task { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TaskActivities")]
    public virtual User User { get; set; } = null!;
}
