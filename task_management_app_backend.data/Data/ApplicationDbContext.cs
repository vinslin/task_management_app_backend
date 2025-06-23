using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Entities;

namespace task_management_app_backend.data.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<UserReleatedTask> userReleatedTasks { get; set; } 

        public DbSet<TaskRelatedProject> TaskRelatedProjects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) { 

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserReleatedTask>()
                .HasKey(ut=> new { ut.EmployeeId , ut.TaskId});

            modelBuilder.Entity<UserReleatedTask>()
                .HasOne(ut => ut.Employee)
                .WithMany(e => e.UserTasks)
                .HasForeignKey(ut => ut.EmployeeId);

            modelBuilder.Entity<UserReleatedTask>()
                .HasOne(ut => ut.Task)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(ut => ut.TaskId);

            //many to many relationship between task and project
            modelBuilder.Entity<TaskRelatedProject>()
                .HasKey(tp => new { tp.TaskId, tp.ProjectId });

            modelBuilder.Entity<TaskRelatedProject>()
                .HasOne(tp => tp.Task)
                .WithMany(t => t.TaskProjects)
                .HasForeignKey(tp => tp.TaskId);

           modelBuilder.Entity<TaskRelatedProject>()
                .HasOne(tp =>tp.Project)
                .WithMany(p => p.ProjectTasks )
                .HasForeignKey(tp => tp.ProjectId);

        }

    }
}
