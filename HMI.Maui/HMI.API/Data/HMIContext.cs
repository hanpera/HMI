using System;
using System.Collections.Generic;
using HMI.API.DataModel;
using Microsoft.EntityFrameworkCore;

namespace HMI.API.Data;

public partial class HMIContext : DbContext
{
    public HMIContext()
    {
    }

    public HMIContext(DbContextOptions<HMIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
