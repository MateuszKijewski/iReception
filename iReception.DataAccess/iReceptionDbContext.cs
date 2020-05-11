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
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<MinuteService> MinuteServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RoomToMinuteService> RoomToMinuteServices { get; set; }
        public DbSet<RoomToService> RoomToServices { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MinuteServiceToReservation> MinuteServicesToReservation { get; set; }
        public DbSet<HotelCompany> HotelCompanies { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(r => r.RoomId);
            builder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.ClientId);

            builder.Entity<MinuteServiceToReservation>()
                .HasKey(mstr => new {mstr.ReservationId, mstr.MinuteServiceId});
            builder.Entity<MinuteServiceToReservation>()
                .HasOne(mstr => mstr.Reservation)
                .WithMany(r => r.MinuteServicesReserved)
                .HasForeignKey(mstr => mstr.ReservationId);
            builder.Entity<MinuteServiceToReservation>()
                .HasOne(mstr => mstr.MinuteService)
                .WithMany(ms => ms.MinuteServiceToReservations)
                .HasForeignKey(mstr => mstr.MinuteServiceId);
                

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

            builder.Entity<RoomToService>()
                .HasKey(rts => new { rts.RoomId, rts.ServiceId });
            builder.Entity<RoomToService>()
                .HasOne(rts => rts.Room)
                .WithMany(r => r.RoomToServices)
                .HasForeignKey(rts => rts.RoomId);
            builder.Entity<RoomToService>()
                .HasOne(rts => rts.Service)
                .WithMany(s => s.RoomToServices)
                .HasForeignKey(rts => rts.ServiceId);


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

        }
    }
}
