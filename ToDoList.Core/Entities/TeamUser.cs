using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

[Index("TeamId", Name = "IX_TeamUsers_Team")]
public partial class TeamUser
{
    [Key]
    public int TeamUserId { get; set; }

    public int TeamId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime JoinedAt { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("TeamUsers")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("TeamId")]
    [InverseProperty("TeamUsers")]
    public virtual Team Team { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TeamUsers")]
    public virtual User User { get; set; } = null!;
}
