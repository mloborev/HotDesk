using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesc.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Device mouse = new Device { Id = 1, Name = "Мышь" };
            Device keyboard = new Device { Id = 2, Name = "Клавиатура" };
            Device screen = new Device { Id = 3, Name = "Монитор" };
            Device secondScreen = new Device { Id = 4, Name = "Второй монитор" };
            Device desktop = new Device { Id = 5, Name = "Системный блок" };
            Device notebook = new Device { Id = 6, Name = "Ноутбук" };*/

            Workplace workplace1 = new Workplace { Id = 1, Description = "First workplace", IsInUse = false };
            Workplace workplace2 = new Workplace { Id = 2, Description = "Second workplace", IsInUse = false };
            Workplace workplace3 = new Workplace { Id = 3, Description = "Third workplace", IsInUse = false };
            Workplace workplace4 = new Workplace { Id = 4, Description = "Fourth workplace", IsInUse = false };
            Workplace workplace5 = new Workplace { Id = 5, Description = "Fifth workplace", IsInUse = false };

            Role adminRole = new Role { Id = 1, Name = "admin" };
            Role userRole = new Role { Id = 2, Name = "user" };

            Employee admin = new Employee { Id = 1, Surname = "Админов", Name = "Админ", Login = "admin", Password = "123456", RoleId = adminRole.Id };
            Employee user1 = new Employee { Id = 2, Surname = "Семёныч", Name = "Семён", Login = "1", Password = "123", RoleId = userRole.Id };
            Employee user2 = new Employee { Id = 3, Surname = "Вэнс", Name = "Илай", Login = "12", Password = "123", RoleId = userRole.Id };
            Employee user3 = new Employee { Id = 4, Surname = "Асина", Name = "Гэнитиро", Login = "123", Password = "123", RoleId = userRole.Id };
            Employee user4 = new Employee { Id = 5, Surname = "Кицураги", Name = "Ким", Login = "1234", Password = "123", RoleId = userRole.Id };
            Employee user5 = new Employee { Id = 6, Surname = "Скала", Name = "Хавел", Login = "12345", Password = "123", RoleId = userRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<Employee>().HasData(new Employee[] { admin, user1, user2, user3, user4, user5});
            //modelBuilder.Entity<Device>().HasData(new Device[] { mouse, keyboard, screen, secondScreen, desktop, notebook});
            modelBuilder.Entity<Workplace>().HasData(new Workplace[] { workplace1, workplace2, workplace3, workplace4, workplace5});
            //modelBuilder.Entity<Workplace>().HasOne(x => x.Reservation).WithOne(x => x.Workplace);

            base.OnModelCreating(modelBuilder);
        }
    }
}
