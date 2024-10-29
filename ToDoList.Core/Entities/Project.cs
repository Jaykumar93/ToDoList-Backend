using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class Project
{
    [Key]
    public int ProjectId { get; set; }

    public int OrganizationId { get; set; }

    public int? TeamId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(7)]
    public string? Color { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("Projects")]
    public virtual Organization Organization { get; set; } = null!;

    [InverseProperty("Project")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    [ForeignKey("TeamId")]
    [InverseProperty("Projects")]
    public virtual Team? Team { get; set; }
}
