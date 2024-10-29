using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

[Index("Email", Name = "IX_Users_Email")]
[Index("Email", Name = "UQ__Users__A9D105340C34F105", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(255)]
    public string? ProfilePicture { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();

    [InverseProperty("User")]
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    [InverseProperty("User")]
    public virtual ICollection<TaskActivity> TaskActivities { get; set; } = new List<TaskActivity>();

    [InverseProperty("User")]
    public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

    [InverseProperty("User")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    [InverseProperty("User")]
    public virtual ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();

    [InverseProperty("User")]
    public virtual ICollection<UserSetting> UserSettings { get; set; } = new List<UserSetting>();
}
