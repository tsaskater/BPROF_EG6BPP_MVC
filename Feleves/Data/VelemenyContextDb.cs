using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace Data
{
    public class VelemenyContextDb : IdentityDbContext<IdentityUser>
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
                    //UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Velemeny.mdf;integrated security=True;MultipleActiveResultSets=True", b => b.MigrationsAssembly("Feleves"));
                    //UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = KnifeStoreDb; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False", b => b.MigrationsAssembly("Feleves"));
                    UseSqlServer(@"Server = tcp:smartbelldb.database.windows.net,1433; Initial Catalog = knifestoredb; Persist Security Info = False; User ID = sbadmin; Password =Passw0rd; MultipleActiveResultSets = True; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;", b => b.MigrationsAssembly("Feleves"));
                    
            }
        }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<IdentityRole>().HasData(
                 new { Id = "341743f0-asd2–42de-afbf-59kmkkmk72cf6", Name = "Admin", NormalizedName = "ADMIN" },
                 new { Id = "341743f0-dee2–42de-bbbb-59kmkkmk72cf6", Name = "User", NormalizedName = "USER" }
             );

            var appUser = new IdentityUser
            {
                Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                SecurityStamp = string.Empty
            };
            var appUser2 = new IdentityUser
            {
                Id = "e2174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "user@user.com",
                NormalizedEmail = "USER@USER.COM",
                EmailConfirmed = true,
                UserName = "user",
                NormalizedUserName = "USER",
                SecurityStamp = string.Empty
            };

            appUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "admin123");
            appUser2.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "user123");


            modelbuilder.Entity<IdentityUser>().HasData(appUser);
            modelbuilder.Entity<IdentityUser>().HasData(appUser2);

            modelbuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6"
            });
            modelbuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "341743f0-dee2–42de-bbbb-59kmkkmk72cf6",
                UserId = "e2174cf0–9412–4cfe-afbf-59f706d72cf6"
            });


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
