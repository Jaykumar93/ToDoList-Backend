using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class Team
{
    [Key]
    public int TeamId { get; set; }

    public int OrganizationId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("Teams")]
    public virtual Organization Organization { get; set; } = null!;

    [InverseProperty("Team")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("Team")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    [InverseProperty("Team")]
    public virtual ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
}
