#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
using TVM.Models;

namespace TVM.Repository;

class TvmDataContext : DbContext
{
    public DbSet<Ticket> Tickets { get; set; } 
    public DbSet<TicketSale> TicketSales { get; set; } 

    public TvmDataContext() 
        : base()
    {
        
    }


    public TvmDataContext(DbContextOptions<TvmDataContext> options)
        :base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            optionsBuilder.UseSqlite("Data Source=tvm.db");
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {            
        // seed data
        modelBuilder.Entity<Ticket>().HasData(
            new Ticket[] {
                new Ticket("TK001", 1.30, DateTime.UtcNow),
                new Ticket("TK002", 1.50, DateTime.UtcNow),
                new Ticket("TK003", 2.30, DateTime.UtcNow),
                new Ticket("TK004", 5.50, DateTime.UtcNow)
            }
        );

    }         
}


#pragma warning restore CS8618