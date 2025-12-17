using FinanceControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(builder =>
            {
                builder.ToTable("Transactions", tableBuilder =>
                {
                    tableBuilder.HasCheckConstraint(
                        "CK_Value_NotNegative",
                        sql: $"\"{nameof(Transaction.Value)}\" > 0");
                });
                builder.HasKey(t => t.Id);
                builder.Property(t => t.Value);
                builder.Property(t => t.Type)
                        .HasConversion<string>()
                        .IsRequired();
                builder.Property(t => t.Category);
                builder.Property(t => t.Description);
                builder.Property(t => t.TransactionDate);
                builder.Property(t => t.PaymentType)
                        .HasConversion<string>()
                        .IsRequired();
                builder.Property(t => t.Reccurence)
                        .HasConversion<string>()
                        .IsRequired();
                builder.Property(t => t.CreatedAt)
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("timezone('America/Sao_Paulo', now())")
                        .IsRequired()
                        .ValueGeneratedOnAdd(); ;
            });
        }
    }
}
