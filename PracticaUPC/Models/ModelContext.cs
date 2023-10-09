using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace PracticaUPC.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("CURSO");
        #region Squema
        builder.Entity<Prueba>().ToTable("PRUEBA").HasNoKey();
        builder.Entity<Matricula>().ToTable("MATRICULA");
        builder.Entity<DetMatricula>().ToTable("DETMATRICULA").HasNoKey();

        #endregion
    }
    public DbSet<Prueba> Pruebas { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<DetMatricula> DetMatriculas { get; set; }

}
