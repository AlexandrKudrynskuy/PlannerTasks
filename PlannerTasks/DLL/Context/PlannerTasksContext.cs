using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class PlannerTasksContext : DbContext
    {
        public PlannerTasksContext(DbContextOptions<PlannerTasksContext> dbContext) : base(dbContext) { Database.EnsureCreated(); }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTask>().HasOne<Priority>(x => x.Priority).WithMany(x=>x.MyTasks).HasForeignKey(x => x.PriorityId);
            modelBuilder.Entity<MyTask>().HasOne<Status>(x => x.Status).WithMany(x => x.MyTasks).HasForeignKey(x => x.StatusId);
            modelBuilder.Entity<MyTask>().HasOne<TypeTask>(x => x.TypeTask).WithMany(x => x.MyTasks).HasForeignKey(x => x.TypeTaskId);

            modelBuilder.Entity<MyTask>().HasOne<User>(x => x.Admin).WithMany(x => x.TasksAdmin).HasForeignKey(x => x.AdminId);
            modelBuilder.Entity<MyTask>().HasOne<User>(x => x.Worker).WithMany(x => x.TasksWorker).HasForeignKey(x => x.WorkerId).OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<User>().Property(x=>x.Email).IsRequired(false);//Column is nullable
            modelBuilder.Entity<User>().Property(x => x.Phone).IsRequired(false);//Column is nullable
            modelBuilder.Entity<User>().Property(x => x.Photo).IsRequired(false);//Column is nullable
            modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(15);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.SurName).HasMaxLength(25);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Login).HasMaxLength(15);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(15);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Phone).HasMaxLength(15);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(50);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Photo).HasMaxLength(200);//Column maxLength 

            modelBuilder.Entity<MyTask>().Property(x => x.DateStart).IsRequired(false);//Column is nullable 
            modelBuilder.Entity<MyTask>().Property(x => x.DateFinich).IsRequired(false);//Column is nullable 
            //modelBuilder.Entity<MyTask>().Property(x => x.TimeSpan).IsRequired(false);//Column is nullable 


            modelBuilder.Entity<User>().HasData(new User { Id = 1, Login = "Alex", Password = "1", Name = "Олександр", SurName = "Кудринський", TypeUser =TypeUser.SuperAdmin, Phone = "0969294279", Email="kudrynskuy@gmail.com", Photo=""});
            modelBuilder.Entity<User>().HasData(new User { Id = 2, Login = "Serg", Password = "1", Name = "Сергій", SurName = "Потапіцин", TypeUser = TypeUser.Admin, Phone = "09690000000", Email = "serg@gmail.com", Photo = "" });
            modelBuilder.Entity<User>().HasData(new User { Id = 3, Login = "Roma", Password = "1", Name = "Роман", SurName = "Романченко", TypeUser = TypeUser.Worker, Phone = "09690000001", Email = "roma@gmail.com", Photo = "" });
            modelBuilder.Entity<User>().HasData(new User { Id = 4, Login = "Igor", Password = "1", Name = "Igor", SurName = "Ігоренко", TypeUser = TypeUser.Admin, Phone = "09690000001", Email = "roma@gmail.com", Photo = "" });
            modelBuilder.Entity<User>().HasData(new User { Id = 5, Login = "Ivan", Password = "1", Name = "Ivan", SurName = "Іваненко", TypeUser = TypeUser.Worker, Phone = "09690000001", Email = "roma@gmail.com", Photo = "" });

            modelBuilder.Entity<Status>().HasData(new Status { Id = 1, Name = "Початок" });
            modelBuilder.Entity<Status>().HasData(new Status { Id = 2, Name = "Середина" });
            modelBuilder.Entity<Status>().HasData(new Status { Id = 3, Name = "Завершено" });

            modelBuilder.Entity<Priority>().HasData(new Priority { Id = 1, Name = "низький" });
            modelBuilder.Entity<Priority>().HasData(new Priority { Id = 2, Name = "середній" });
            modelBuilder.Entity<Priority>().HasData(new Priority { Id = 3, Name = "високий" });

            modelBuilder.Entity<TypeTask>().HasData(new TypeTask { Id = 1, Name = "WPF" });
            modelBuilder.Entity<TypeTask>().HasData(new TypeTask { Id = 2, Name = "Console" });
            modelBuilder.Entity<TypeTask>().HasData(new TypeTask { Id = 3, Name = "Web" });

            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 1, Name="TaskPlaner", Description="Task Planer", PriorityId=1, StatusId=1, TypeTaskId=1, AdminId=2, WorkerId=5 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 2, Name = "RepositoryAbstract", Description = "RepositoryAbstract", PriorityId = 2, StatusId = 1, TypeTaskId = 2, AdminId = 4, WorkerId = 5 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 3, Name = "ArraySort", Description = "Array", PriorityId = 3, StatusId = 2, TypeTaskId = 3, AdminId = 4, WorkerId = 3 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 4, Name = "Sequences", Description = "Task Planer", PriorityId = 1, StatusId = 2, TypeTaskId = 3, AdminId = 2, WorkerId = 3 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 5, Name = "Thred", Description = "Task Planer", PriorityId = 1, StatusId = 2, TypeTaskId = 2, AdminId = 4, WorkerId = 5 });

            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 6, Name = "TaskPlaner", Description = "Task Planer", PriorityId = 2, StatusId = 1, TypeTaskId = 3, AdminId = 2, WorkerId = 3 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 7, Name = "RepositoryAbstract", Description = "RepositoryAbstract", PriorityId = 2, StatusId = 3, TypeTaskId = 2, AdminId = 4, WorkerId = 5 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 8, Name = "ArraySort", Description = "Array", PriorityId = 1, StatusId = 1, TypeTaskId = 1, AdminId = 2, WorkerId = 5 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 9, Name = "Sequences", Description = "Task Planer", PriorityId = 3, StatusId = 3, TypeTaskId = 1, AdminId = 4, WorkerId = 5 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 10, Name = "Thred", Description = "Task Planer", PriorityId = 2, StatusId = 1, TypeTaskId = 2, AdminId = 2, WorkerId = 3 });

            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 11, Name = "TaskPlaner", Description = "Task Planer", PriorityId = 2, StatusId = 3, TypeTaskId = 2, AdminId = 4, WorkerId = 3 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 12, Name = "RepositoryAbstract", Description = "RepositoryAbstract", PriorityId = 1, StatusId = 3, TypeTaskId = 2, AdminId = 4, WorkerId = 5 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 13, Name = "ArraySort", Description = "Array", PriorityId = 1, StatusId = 1, TypeTaskId = 1, AdminId = 2, WorkerId = 5 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 14, Name = "Sequences", Description = "Task Planer", PriorityId = 3, StatusId = 3, TypeTaskId = 2, AdminId = 2, WorkerId = 5 });
            modelBuilder.Entity<MyTask>().HasData(new MyTask { Id = 15, Name = "Thred", Description = "Task Planer", PriorityId = 2, StatusId = 1, TypeTaskId = 3, AdminId = 2, WorkerId = 5 });

            base.OnModelCreating(modelBuilder);
        }
        DbSet<User> Users { get; set;}
        DbSet<MyTask> MyTasks { get; set;}
        DbSet<Priority> Priorities { get; set; }
        DbSet<Status>  Statuses{ get; set; }
        DbSet<TypeTask>  TypeTasks { get; set; }

    }
}
