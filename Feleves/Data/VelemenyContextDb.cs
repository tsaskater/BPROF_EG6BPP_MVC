using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace Data
{
    public class VelemenyContextDb : DbContext
    {
        public DbSet<Velemeny> Velemenyek { get; set; }
        public DbSet<Kes> Kesek { get; set; }
        public DbSet<Kes_Bolt> Kes_Bolt { get; set; }
        public VelemenyContextDb(DbContextOptions<VelemenyContextDb> opt) : base(opt)
        {

        }

        public VelemenyContextDb()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Velemeny.mdf;integrated security=True;MultipleActiveResultSets=True");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Velemeny>(entity =>
            {
                entity
                .HasOne(Velemeny => Velemeny.Kes_Termek)
                .WithMany(Kes => Kes.Velemenyek)
                .HasForeignKey(Velemeny => Velemeny.Gyartasi_Cikkszam)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelbuilder.Entity<Kes>(entity =>
            {
                entity
                .HasOne(Kes => Kes.Kes_Bolt_Keszlet_Info)
                .WithMany(Kes_Bolt_Keszlet_Info => Kes_Bolt_Keszlet_Info.Kesek)
                .HasForeignKey(Kes => Kes.Raktar_Id)
                .OnDelete(DeleteBehavior.Cascade); 
            });
        }

    }
}
