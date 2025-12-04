using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Infrastructure.Data;

public partial class TicketsAndMerchContext : DbContext
{
    public TicketsAndMerchContext()
    {
    }

    public TicketsAndMerchContext(DbContextOptions<TicketsAndMerchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Concert> Concerts { get; set; }

    public virtual DbSet<Merch> Merches { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public DbSet<BuyTicket> BuyTickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}