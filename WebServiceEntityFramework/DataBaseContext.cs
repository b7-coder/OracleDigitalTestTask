using Microsoft.EntityFrameworkCore;
using WebServiceDomain.Models;

namespace WebServiceEntityFramework;

public partial class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
        Database.EnsureCreated();
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<DataModel> DataModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jsontable_pkey");

            entity.ToTable("jsontable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Jsonkey)
                .HasColumnType("character varying")
                .HasColumnName("jsonkey");
            entity.Property(e => e.Jsonvalue)
                .HasColumnType("character varying")
                .HasColumnName("jsonvalue");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
