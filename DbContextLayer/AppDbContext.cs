using Demo.Connection;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DbContextLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            optionsBuilder.UseSqlServer(Connectionstrings.Intances().GetDataSourceSever());

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //https://www.entityframeworktutorial.net/efcore/configure-one-to-one-relationship-using-fluent-api-in-ef-core.aspx

            // Nếu id mang giá trị là GUID
            //modelBuilder.Entity<Student>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            //modelBuilder.Entity<Student>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Student>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Teacher>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Student>()
                .Property(x => x.Name)
                .HasMaxLength(50)
                .HasColumnType("nvarchar");
            modelBuilder.Entity<Teacher>()
               .Property(x => x.Name)
               .HasMaxLength(50)
               .HasColumnType("nvarchar");
            
            // Đánh dấu chỉ mục
            modelBuilder.Entity<Student>().HasIndex(x => new { x.Name }).IsUnique();
            modelBuilder.Entity<Teacher>().HasIndex(x => new { x.Name }).IsUnique();
            
            //Mối quan hệ nhiều nhiều
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);
            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

        }
    }
}
