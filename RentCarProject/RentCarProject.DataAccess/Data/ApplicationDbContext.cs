using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentCarProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentCarProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vehicle> Vehicles  { get; set; }

        public DbSet<ApplicationUser>  ApplicationUsers { get; set; }

        public DbSet<ReservationCart> ReservationCarts { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }


    }
}
