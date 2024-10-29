using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class SubTask
{
    [Key]
    public int SubTaskId { get; set; }

    public int ParentTaskId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public int OrderIndex { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("ParentTaskId")]
    [InverseProperty("SubTasks")]
    public virtual Task ParentTask { get; set; } = null!;
}
