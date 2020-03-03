using iReception.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.DataAccess
{
    public class iReceptionDbContext : IdentityDbContext
    {
        public iReceptionDbContext(DbContextOptions<iReceptionDbContext> options)
            : base(options)            
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<MinuteService> MinuteServices { get; set; }
        public DbSet<RoomToMinuteService> RoomToMinuteServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RoomToMinuteService>()
                .HasKey(rtms => new { rtms.RoomId, rtms.MinuteServiceId });
            builder.Entity<RoomToMinuteService>()
                .HasOne(rtms => rtms.Room)
                .WithMany(r => r.RoomToMinuteServices)
                .HasForeignKey(rtms => rtms.RoomId);
            builder.Entity<RoomToMinuteService>()
                .HasOne(rtms => rtms.MinuteService)
                .WithMany(ms => ms.RoomToMinuteServices)
                .HasForeignKey(rtms => rtms.MinuteServiceId);

            builder.Entity<Room>()
                .HasOne(r => r.Building)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BuildingId)
                .IsRequired(true);

            builder.Entity<Client>()
                .HasOne(c => c.Room)
                .WithOne(r => r.Client)
                .HasForeignKey<Room>(r => r.ClientId)
                .IsRequired(false);

            builder.Entity<Transaction>()
                .HasOne(t => t.Worker)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WorkerId);

            builder.Entity<Transaction>()
                .HasOne(t => t.Client)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.ClientId);

            builder.Entity<Worker>()
                .HasOne(w => w.Department)
                .WithMany(d => d.Workers)
                .HasForeignKey(w => w.DepartmentId);      
        }
    }
}
