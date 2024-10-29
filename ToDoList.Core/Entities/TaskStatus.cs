using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class TaskStatus
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(20)]
    public string Name { get; set; } = null!;

    [StringLength(7)]
    public string Color { get; set; } = null!;

    public bool IsFinal { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
