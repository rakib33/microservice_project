using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// HasData is used for seeding data, and EF Core is noticeing that the data is changing every time the application starts.
        /// Avoid EF Core warning for dynamic seed data like this.new DateTime(), Guid.NewGuid()).'
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings =>
           warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            await OrderContextSeed.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Order> Orders { get; set; }

    }
}
