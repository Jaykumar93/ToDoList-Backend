using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class TaskComment
{
    [Key]
    public int CommentId { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskComments")]
    public virtual Task Task { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TaskComments")]
    public virtual User User { get; set; } = null!;
}
