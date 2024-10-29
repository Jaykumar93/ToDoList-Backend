using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

[Index("OrganizationId", Name = "IX_OrganizationUsers_Org")]
public partial class OrganizationUser
{
    [Key]
    public int OrganizationUserId { get; set; }

    public int OrganizationId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime JoinedAt { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("OrganizationUsers")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("OrganizationUsers")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("OrganizationUsers")]
    public virtual User User { get; set; } = null!;
}
