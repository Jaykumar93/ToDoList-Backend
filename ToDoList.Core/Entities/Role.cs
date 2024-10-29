using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class Role
{
    [Key]
    public int RoleId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    public string? Permissions { get; set; }

    public bool IsSystem { get; set; }

    public DateTime CreatedAt { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();

    [InverseProperty("Role")]
    public virtual ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
}
