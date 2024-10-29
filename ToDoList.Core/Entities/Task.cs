using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

[Index("ProjectId", "TeamId", Name = "IX_Tasks_Project")]
[Index("StatusId", Name = "IX_Tasks_Status")]
[Index("UserId", Name = "IX_Tasks_User")]
public partial class Task
{
    [Key]
    public int TaskId { get; set; }

    public int? ProjectId { get; set; }

    public int? TeamId { get; set; }

    public int UserId { get; set; }

    public int? CategoryId { get; set; }

    public int? ParentTaskId { get; set; }

    public int PriorityId { get; set; }

    public int StatusId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public int? EstimatedMinutes { get; set; }

    public bool IsRecurring { get; set; }

    public string? RecurrencePattern { get; set; }

    public DateTime? CompletedAt { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Tasks")]
    public virtual Category? Category { get; set; }

    [InverseProperty("ParentTask")]
    public virtual ICollection<Task> InverseParentTask { get; set; } = new List<Task>();

    [ForeignKey("ParentTaskId")]
    [InverseProperty("InverseParentTask")]
    public virtual Task? ParentTask { get; set; }

    [ForeignKey("PriorityId")]
    [InverseProperty("Tasks")]
    public virtual TaskPriority Priority { get; set; } = null!;

    [ForeignKey("ProjectId")]
    [InverseProperty("Tasks")]
    public virtual Project? Project { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Tasks")]
    public virtual TaskStatus Status { get; set; } = null!;

    [InverseProperty("ParentTask")]
    public virtual ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskActivity> TaskActivities { get; set; } = new List<TaskActivity>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskAttachment> TaskAttachments { get; set; } = new List<TaskAttachment>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();

    [ForeignKey("TeamId")]
    [InverseProperty("Tasks")]
    public virtual Team? Team { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Tasks")]
    public virtual User User { get; set; } = null!;
}
