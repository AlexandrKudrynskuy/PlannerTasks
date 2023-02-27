using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class PlannerTasksContext : DbContext
    {
        public PlannerTasksContext(DbContextOptions<PlannerTasksContext> dbContext) : base(dbContext) { Database.EnsureCreated(); }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x=>x.Email).IsRequired(false);//Column is nullable
            modelBuilder.Entity<User>().Property(x => x.Phone).IsRequired(false);//Column is nullable
            modelBuilder.Entity<User>().Property(x => x.Photo).IsRequired(false);//Column is nullable
            modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(15);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.SurName).HasMaxLength(25);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Login).HasMaxLength(15);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(15);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Photo).HasMaxLength(100);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(50);//Column maxLength 
            modelBuilder.Entity<User>().Property(x => x.Photo).HasMaxLength(15);//Column maxLength 



            modelBuilder.Entity<User>().HasData(new User { Id = 1, Login = "Alex", Password = "1", Name = "Олександр", SurName = "Кудринський", isAdmin = true, Phone = "0969294279", Email="kudrynskuy@gmail.com", Photo=""});
            modelBuilder.Entity<User>().HasData(new User { Id = 2, Login = "Serg", Password = "1", Name = "Сергій", SurName = "Потапіцин", isAdmin = false, Phone = "09690000000", Email = "serg@gmail.com", Photo = "" });
            modelBuilder.Entity<User>().HasData(new User { Id = 3, Login = "Roma", Password = "1", Name = "Роман", SurName = "Романченко", isAdmin = false, Phone = "09690000001", Email = "roma@gmail.com", Photo = "" });

            base.OnModelCreating(modelBuilder);
        }
        DbSet<User> Users { get; set;}
    }
}
