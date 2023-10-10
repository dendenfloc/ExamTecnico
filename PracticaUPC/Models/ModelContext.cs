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
        builder.HasDefaultSchema("PRACTICA");
        #region Squema
        builder.Entity<Matricula>().ToTable("MATRICULA");
        builder.Entity<DetMatricula>().ToTable("DETMATRICULA").HasKey( t => new {t.IDMATRICULA , t.CODLINEANEGOCIO , t.CODCURSO });
        builder.Entity<Curso>().ToTable("CURSO").HasKey(t => new { t.CODLINEANEGOCIO, t.CODCURSO });

        builder.Entity<Matricula>()
            .HasMany(m => m.DetMatriculas)
            .WithOne(d => d.Matricula)
            .HasForeignKey(d => d.IDMATRICULA);

        builder.Entity<DetMatricula>()
         .HasOne(d => d.Curso)
         .WithMany()
         .HasForeignKey(d => new { d.CODLINEANEGOCIO, d.CODCURSO });
        #endregion
    }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<DetMatricula> DetMatriculas { get; set; }
    public DbSet<Curso> Cursos { get; set; }

}
