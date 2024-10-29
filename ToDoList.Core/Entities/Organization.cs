using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class Organization
{
    [Key]
    public int OrganizationId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(255)]
    public string? LogoUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("Organization")]
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    [InverseProperty("Organization")]
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();

    [InverseProperty("Organization")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("Organization")]
    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
