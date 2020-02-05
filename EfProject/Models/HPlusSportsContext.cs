using Microsoft.EntityFrameworkCore;

namespace EfProject.Models
{
    public partial class HPlusSportsContext : DbContext
    {
        public HPlusSportsContext()
        {
        }

        public HPlusSportsContext(DbContextOptions<HPlusSportsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SalesGroup> SalesGroup { get; set; }
        public virtual DbSet<Salesperson> Salesperson { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LG;Initial Catalog=HPlusSports;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Customer
            modelBuilder.Entity<Customer>(entity =>
               {
                   entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                   entity.Property(e => e.CmpLastFirst)
                       .HasColumnName("cmp_LastFirst")
                       .HasMaxLength(102)
                       .IsUnicode(false)
                       .HasComputedColumnSql("(([str_fld_LastName]+', ')+[str_fld_FirstName])");

                   entity.Property(e => e.Address)
                       .HasColumnName("str_fld_Address")
                       .HasMaxLength(50)
                       .IsUnicode(false);

                   entity.Property(e => e.City)
                       .HasColumnName("str_fld_City")
                       .HasMaxLength(50)
                       .IsUnicode(false);

                   entity.Property(e => e.Email)
                       .HasColumnName("str_fld_Email")
                       .HasMaxLength(50)
                       .IsUnicode(false);

                   entity.Property(e => e.FirstName)
                       .HasColumnName("str_fld_FirstName")
                       .HasMaxLength(50)
                       .IsUnicode(false);

                   entity.Property(e => e.LastName)
                       .HasColumnName("str_fld_LastName")
                       .HasMaxLength(50)
                       .IsUnicode(false);

                   entity.Property(e => e.Phone)
                       .HasColumnName("str_fld_Phone")
                       .HasMaxLength(50)
                       .IsUnicode(false);

                   entity.Property(e => e.State)
                       .HasColumnName("str_fld_State")
                       .HasMaxLength(50)
                       .IsUnicode(false);

                   entity.Property(e => e.Zipcode)
                       .HasColumnName("str_fld_Zipcode")
                       .HasMaxLength(50)
                       .IsUnicode(false);
               });
            #endregion

            #region Order

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderDate)
                    .HasName("IX_Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LastUpdate)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.SalespersonId).HasColumnName("SalespersonID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('none')");

                entity.Property(e => e.TotalDue).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.Salesperson)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.SalespersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Salesperson");
            });
            #endregion

            #region OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
    {
        entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");

        entity.Property(e => e.OrderId).HasColumnName("OrderID");

        entity.Property(e => e.ProductId)
            .IsRequired()
            .HasColumnName("ProductID")
            .HasMaxLength(10);

        entity.HasOne(d => d.Order)
            .WithMany(p => p.OrderItem)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OrderItem_Order");

        entity.HasOne(d => d.Product)
            .WithMany(p => p.OrderItem)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OrderItem_Product1");
    });

            #endregion

            #region Product
            modelBuilder.Entity<Product>(entity =>
               {
                   entity.Property(e => e.ProductId)
                       .HasColumnName("ProductID")
                       .HasMaxLength(10);

                   entity.Property(e => e.Price).HasColumnType("money");

                   entity.Property(e => e.ProductName)
                       .HasMaxLength(50)
                       .IsUnicode(false);

                   entity.Property(e => e.Status)
                       .HasMaxLength(50)
                       .IsUnicode(false);

                   entity.Property(e => e.Variety)
                       .HasMaxLength(50)
                       .IsUnicode(false);
               });
            #endregion

            #region SalesGroup
            modelBuilder.Entity<SalesGroup>(entity =>
              {
                  entity.HasIndex(e => new { e.State, e.Type })
                      .HasName("IX_StateType")
                      .IsUnique();

                  entity.Property(e => e.State)
                      .IsRequired()
                      .HasMaxLength(2);
              });

            #endregion

            #region Salesperson
            modelBuilder.Entity<Salesperson>(entity =>
             {
                 entity.Property(e => e.SalespersonId).HasColumnName("SalespersonID");

                 entity.Property(e => e.Address)
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 entity.Property(e => e.City)
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 entity.Property(e => e.Email)
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 entity.Property(e => e.FirstName)
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 entity.Property(e => e.LastName)
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 entity.Property(e => e.Phone)
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 entity.Property(e => e.SalesGroupState)
                     .IsRequired()
                     .HasMaxLength(2)
                     .HasDefaultValueSql("(N'CA')");

                 entity.Property(e => e.SalesGroupType).HasDefaultValueSql("((1))");

                 entity.Property(e => e.State)
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 entity.Property(e => e.Zipcode)
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 entity.Ignore(e => e.FullName);
             });
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
