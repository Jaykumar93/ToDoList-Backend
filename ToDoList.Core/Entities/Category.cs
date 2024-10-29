using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Entities;

public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    public int OrganizationId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(7)]
    public string? Color { get; set; }

    [StringLength(50)]
    public string? Icon { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("Categories")]
    public virtual Organization Organization { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
