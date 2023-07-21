using Microsoft.EntityFrameworkCore;
using WebServiceDomain.Models;

public partial class JsondataContext : DbContext
{
    public JsondataContext()
    {
    }

    public JsondataContext(DbContextOptions<JsondataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Jsontable> Jsontables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=jsondata;Username=postgres;Password=x0x0xaxaB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Jsontable>(entity =>
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
