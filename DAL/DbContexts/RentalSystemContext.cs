using System;
using System.Collections.Generic;
using DAL.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbContexts;

public partial class RentalSystemContext : DbContext
{
    public RentalSystemContext()
    {
    }

    public RentalSystemContext(DbContextOptions<RentalSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<BookingCancel> BookingCancels { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<IdProof> IdProofs { get; set; }

    public virtual DbSet<MoneyTransaction> MoneyTransactions { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<UserBooking> UserBookings { get; set; }

    public virtual DbSet<UserRegister> UserRegisters { get; set; }

    public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CIPL1309_DOTNET\\MSSQLSERVER19;Initial Catalog=RentalSystem;User ID=sa;Password=Colan123;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK_a");

            entity.ToTable("Admin");

            entity.Property(e => e.AdminName).IsUnicode(false);
            entity.Property(e => e.EmailId).IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
        });

        modelBuilder.Entity<BookingCancel>(entity =>
        {
            entity.HasKey(e => e.CancelId).HasName("PK_id");

            entity.ToTable("BookingCancel");

            entity.Property(e => e.CancelId).HasColumnName("Cancel_Id");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasColumnName("Modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Modified_On");
            entity.Property(e => e.Reason).IsUnicode(false);
            entity.Property(e => e.UserName).IsUnicode(false);
            entity.Property(e => e.VehicleName).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.BookingCancels)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_bc");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK_cit");

            entity.ToTable("City");

            entity.Property(e => e.CityName).IsUnicode(false);

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_ci");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK_ci");

            entity.Property(e => e.Comment1)
                .IsUnicode(false)
                .HasColumnName("Comment");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasColumnName("Modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Modified_On");
            entity.Property(e => e.UserName).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_c");
        });

        modelBuilder.Entity<IdProof>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_at");

            entity.ToTable("IdProof");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.IdProof1)
                .IsUnicode(false)
                .HasColumnName("Id_Proof");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasColumnName("Modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Modified_On");
            entity.Property(e => e.PhotoId)
                .IsUnicode(false)
                .HasColumnName("Photo_Id");
            entity.Property(e => e.PhotoName)
                .IsUnicode(false)
                .HasColumnName("Photo_Name");
            entity.Property(e => e.ProofName)
                .IsUnicode(false)
                .HasColumnName("Proof_Name");

            entity.HasOne(d => d.User).WithMany(p => p.IdProofs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ip");
        });

        modelBuilder.Entity<MoneyTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_ti");

            entity.ToTable("MoneyTransaction");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasColumnName("Modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Modified_On");
            entity.Property(e => e.TotalDays).IsUnicode(false);
            entity.Property(e => e.TransactionAmount).IsUnicode(false);
            entity.Property(e => e.UserName).IsUnicode(false);
            entity.Property(e => e.VehicleName).IsUnicode(false);

            entity.HasOne(d => d.Booking).WithMany(p => p.MoneyTransactions)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_mt");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK_si");

            entity.ToTable("State");

            entity.Property(e => e.StateName).IsUnicode(false);
        });

        modelBuilder.Entity<UserBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK_bi");

            entity.ToTable("UserBooking");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.DropDate).HasColumnType("date");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasColumnName("Modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Modified_On");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.PickUpDate).HasColumnType("date");
            entity.Property(e => e.VehicleName).IsUnicode(false);
            entity.Property(e => e.VehicleNo).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserBookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRegister");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.UserBookings)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_ub");
        });

        modelBuilder.Entity<UserRegister>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_ui");

            entity.ToTable("UserRegister");

            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.EmailId)
                .IsUnicode(false)
                .HasColumnName("Email_Id");
            entity.Property(e => e.Gender).IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasColumnName("Modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Modified_On");
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.RollId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UserName).IsUnicode(false);
        });

        modelBuilder.Entity<VehicleDetail>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK_vi");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasColumnName("Modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Modified_On");
            entity.Property(e => e.NoOfVehicle).HasDefaultValueSql("((5))");
            entity.Property(e => e.VehicleName).IsUnicode(false);
            entity.Property(e => e.VehicleNo).IsUnicode(false);
            entity.Property(e => e.VehiclePhoto).IsUnicode(false);
            entity.Property(e => e.VehicleType).IsUnicode(false);

            entity.HasOne(d => d.Admin).WithMany(p => p.VehicleDetails)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK_vd");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
