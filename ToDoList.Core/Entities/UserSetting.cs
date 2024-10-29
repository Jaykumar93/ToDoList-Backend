using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class UserSetting
{
    [Key]
    public int UserSettingsId { get; set; }

    public int UserId { get; set; }

    [StringLength(20)]
    public string? ThemePreference { get; set; }

    [StringLength(50)]
    public string? TimeZone { get; set; }

    [StringLength(10)]
    public string? Language { get; set; }

    public string? NotificationPreferences { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserSettings")]
    public virtual User User { get; set; } = null!;
}
