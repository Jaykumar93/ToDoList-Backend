using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Entities;

namespace ToDoList.Core;

public partial class ToDoContext : DbContext
{
    public ToDoContext()
    {
    }

    public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationUser> OrganizationUsers { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubTask> SubTasks { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Entities.Task> Tasks { get; set; }

    public virtual DbSet<TaskActivity> TaskActivities { get; set; }

    public virtual DbSet<TaskAttachment> TaskAttachments { get; set; }

    public virtual DbSet<TaskComment> TaskComments { get; set; }

    public virtual DbSet<TaskPriority> TaskPriorities { get; set; }

    public virtual DbSet<Entities.TaskStatus> TaskStatuses { get; set; }

    public virtual DbSet<TaskTag> TaskTags { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamUser> TeamUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSetting> UserSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B405023CE");

            entity.HasOne(d => d.Organization).WithMany(p => p.Categories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Categorie__Organ__5EBF139D");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK__Organiza__CADB0B12CEF9DD3E");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<OrganizationUser>(entity =>
        {
            entity.HasKey(e => e.OrganizationUserId).HasName("PK__Organiza__5CA70F7FF8C99283");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizat__Organ__4F7CD00D");

            entity.HasOne(d => d.Role).WithMany(p => p.OrganizationUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizat__RoleI__5165187F");

            entity.HasOne(d => d.User).WithMany(p => p.OrganizationUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizat__UserI__5070F446");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABEF0342EAA64");

            entity.HasOne(d => d.Organization).WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Projects__Organi__5AEE82B9");

            entity.HasOne(d => d.Team).WithMany(p => p.Projects).HasConstraintName("FK__Projects__TeamId__5BE2A6F2");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__RefreshT__658FEEEA299C0325");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RefreshTo__UserI__48CFD27E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A8EBE35E8");
        });

        modelBuilder.Entity<SubTask>(entity =>
        {
            entity.HasKey(e => e.SubTaskId).HasName("PK__SubTasks__869FF1824D946FA0");

            entity.HasOne(d => d.ParentTask).WithMany(p => p.SubTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubTasks__Parent__71D1E811");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CF9AC60C11A52");
        });

        modelBuilder.Entity<Entities.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B14C08F3B2");

            entity.HasIndex(e => e.DueDate, "IX_Tasks_DueDate").HasFilter("([DueDate] IS NOT NULL)");

            entity.HasOne(d => d.Category).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__CategoryI__6B24EA82");

            entity.HasOne(d => d.ParentTask).WithMany(p => p.InverseParentTask).HasConstraintName("FK__Tasks__ParentTas__6C190EBB");

            entity.HasOne(d => d.Priority).WithMany(p => p.Tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__PriorityI__6D0D32F4");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__ProjectId__68487DD7");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__StatusId__6E01572D");

            entity.HasOne(d => d.Team).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__TeamId__693CA210");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__UserId__6A30C649");
        });

        modelBuilder.Entity<TaskActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__TaskActi__45F4A791C3C0CD49");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskActivities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskActiv__TaskI__01142BA1");

            entity.HasOne(d => d.User).WithMany(p => p.TaskActivities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskActiv__UserI__02084FDA");
        });

        modelBuilder.Entity<TaskAttachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__TaskAtta__442C64BEFA722A06");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskAttachments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskAttac__TaskI__7E37BEF6");
        });

        modelBuilder.Entity<TaskComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__TaskComm__C3B4DFCA1C35119C");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskComme__TaskI__7A672E12");

            entity.HasOne(d => d.User).WithMany(p => p.TaskComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskComme__UserI__7B5B524B");
        });

        modelBuilder.Entity<TaskPriority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("PK__TaskPrio__D0A3D0BEB34622F6");
        });

        modelBuilder.Entity<Entities.TaskStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__TaskStat__C8EE20633B558C34");
        });

        modelBuilder.Entity<TaskTag>(entity =>
        {
            entity.HasKey(e => new { e.TaskId, e.TagId }).HasName("PK__TaskTags__AA3E862BF9CD8B8C");

            entity.HasOne(d => d.Tag).WithMany(p => p.TaskTags)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskTags__TagId__778AC167");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskTags)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaskTags__TaskId__76969D2E");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__123AE7998A9E820B");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Organization).WithMany(p => p.Teams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teams__Organizat__3B75D760");
        });

        modelBuilder.Entity<TeamUser>(entity =>
        {
            entity.HasKey(e => e.TeamUserId).HasName("PK__TeamUser__0AF5EAD3ACE15A8E");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Role).WithMany(p => p.TeamUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamUsers__RoleI__571DF1D5");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamUsers__TeamI__5535A963");

            entity.HasOne(d => d.User).WithMany(p => p.TeamUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamUsers__UserI__5629CD9C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CDEA34B2A");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<UserSetting>(entity =>
        {
            entity.HasKey(e => e.UserSettingsId).HasName("PK__UserSett__E13193A9B12D2007");

            entity.Property(e => e.Language).HasDefaultValue("en");
            entity.Property(e => e.ThemePreference).HasDefaultValue("light");
            entity.Property(e => e.TimeZone).HasDefaultValue("UTC");

            entity.HasOne(d => d.User).WithMany(p => p.UserSettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSetti__UserI__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
