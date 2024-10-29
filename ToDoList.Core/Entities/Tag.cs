using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class Tag
{
    [Key]
    public int TagId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(7)]
    public string? Color { get; set; }

    public DateTime CreatedAt { get; set; }

    [InverseProperty("Tag")]
    public virtual ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}
