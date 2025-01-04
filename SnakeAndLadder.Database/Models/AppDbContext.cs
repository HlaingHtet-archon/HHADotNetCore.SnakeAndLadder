using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SnakeAndLadder.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBoard> TblBoards { get; set; }

    public virtual DbSet<TblGameplay> TblGameplays { get; set; }

    public virtual DbSet<TblPlayer> TblPlayers { get; set; }

    public virtual DbSet<TblWinnerPlayer> TblWinnerPlayers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=SnakeAndLadder;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBoard>(entity =>
        {
            entity.HasKey(e => e.BoardId).HasName("PK__Tbl_Boar__F9646BF259FB343F");

            entity.ToTable("Tbl_Board");

            entity.Property(e => e.Destination)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblGameplay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Game__3214EC07D9FC2E43");

            entity.ToTable("Tbl_Gameplay");

            entity.Property(e => e.GameCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlayerCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlayerColor)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblPlayer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblPlaye__3214EC0722721D26");

            entity.ToTable("Tbl_Player");

            entity.Property(e => e.PlayerCode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblWinnerPlayer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Winn__3214EC07E9F45605");

            entity.ToTable("Tbl_WinnerPlayer");

            entity.Property(e => e.GameCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SecondPlaceId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ThirdPlaceId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WinnerId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
