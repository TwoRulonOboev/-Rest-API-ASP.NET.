using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pharmo.Models
{
    public partial class DbApiContext : DbContext
    {
        public DbApiContext()
        {
            SeedData();
        }

        public DbApiContext(DbContextOptions<DbApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=DbApi;Integrated Security=True;Encrypt=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Medicine__3214EC071367DBD5");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CostOfMedicine).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ExpiryDate).HasColumnType("date");
                entity.Property(e => e.ManufacturingDate).HasColumnType("date");
                entity.Property(e => e.MedicineName).HasMaxLength(100);
            });

            modelBuilder.Entity<Pharmacy>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Pharmaci__3214EC0731A73F68");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.MedicineName).HasMaxLength(100);
                entity.Property(e => e.PricePerUnit).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        //добавляем 7 записей в таблицу "Аптеки" и 15 записей в таблицу "Лекарства"
        public void SeedData()
        {
            if (Medicines.Any() || Pharmacies.Any())
            {
                return;
            }

            // Добавляем данные в таблицу "Аптеки"
            for (int i = 1; i <= 7; i++)
            {
                Pharmacies.Add(new Pharmacy
                {
                    Id = i,
                    MedicineCode = i,
                    MedicineName = $"Medicine {i}",
                    PricePerUnit = 10 + i
                });
            }

            // Добавляем данные в таблицу "Лекарства"
            for (int i = 1; i <= 15; i++)
            {
                Medicines.Add(new Medicine
                {
                    Id = i,
                    MedicineCode = i % 7 + 1, // Для разнообразия кодов лекарств
                    MedicineName = $"Medicine {i % 7 + 1}",
                    ManufacturingDate = DateTime.Now.AddDays(-i),
                    ExpiryDate = DateTime.Now.AddDays(i * 2),
                    NumberOfPackages = i,
                    PharmacyNumber = i % 7 + 1,
                    CostOfMedicine = 10 + i * 2
                });
            }

            SaveChanges();
        }
    }
}
