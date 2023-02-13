using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TCIPlaba1;

public partial class Ictplaba1Context : DbContext
{
    public Ictplaba1Context()
    {
    }

    public Ictplaba1Context(DbContextOptions<Ictplaba1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<England> Englands { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Stadium> Stadia { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamRole> TeamRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= LAPTOP-J5R1H9E6; Database=ICTPLaba1; Trusted_Connection=True; Trust Server Certificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clubs__3214EC271E05CBE3");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("create_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Division__3214EC2727CC5F5B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DivisionOrLeague).HasColumnName("division_or_league");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<England>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("england");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Division).HasColumnName("division");
            entity.Property(e => e.Ft)
                .HasMaxLength(50)
                .HasColumnName("FT");
            entity.Property(e => e.Goaldif).HasColumnName("goaldif");
            entity.Property(e => e.Hgoal).HasColumnName("hgoal");
            entity.Property(e => e.Home)
                .HasMaxLength(50)
                .HasColumnName("home");
            entity.Property(e => e.Result)
                .HasMaxLength(50)
                .HasColumnName("result");
            entity.Property(e => e.Tier).HasColumnName("tier");
            entity.Property(e => e.Totgoal).HasColumnName("totgoal");
            entity.Property(e => e.Vgoal).HasColumnName("vgoal");
            entity.Property(e => e.Visitor)
                .HasMaxLength(50)
                .HasColumnName("visitor");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Matches__3214EC277BC61D42");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Division).HasColumnName("division");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Particip__3214EC275E062F1E");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Goals).HasColumnName("goals");
            entity.Property(e => e.Match).HasColumnName("match");
            entity.Property(e => e.Team).HasColumnName("team");
            entity.Property(e => e.TeamRole).HasColumnName("team_role");
        });

        modelBuilder.Entity<Stadium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stadium__3214EC272219FE8E");

            entity.ToTable("Stadium");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.MaxCapacity).HasColumnName("max_capacity");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teams__3214EC27E817FD3F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Club).HasColumnName("club");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TeamRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Team_rol__3214EC275791BFE1");

            entity.ToTable("Team_role");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(1)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
