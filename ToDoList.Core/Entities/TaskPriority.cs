using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class TaskPriority
{
    [Key]
    public int PriorityId { get; set; }

    [StringLength(20)]
    public string Name { get; set; } = null!;

    public int Level { get; set; }

    [StringLength(7)]
    public string Color { get; set; } = null!;

    [InverseProperty("Priority")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
