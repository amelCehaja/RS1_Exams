using Microsoft.EntityFrameworkCore;
using RS1_Ispit_2017_06_21_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.EF
{
    public class MojContext: DbContext
    {
        public MojContext(DbContextOptions<MojContext> options): base(options)
        {
    
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MaturskiIspit>()
                .HasOne(x => x.Ispitivac)
                .WithMany()
                .HasForeignKey(x => x.IspitivacID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaturskiIspit>()
                .HasOne(x => x.Odjeljenje)
                .WithMany()
                .HasForeignKey(x => x.OdjeljenjeID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaturskiIspitStavka>()
                .HasOne(x => x.MaturskiIspit)
                .WithMany()
                .HasForeignKey(x => x.MaturskiIspitID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaturskiIspitStavka>()
                .HasOne(x => x.UpisUOdjeljenje)
                .WithMany()
                .HasForeignKey(x => x.UpisUOdjeljenjeID)
                .OnDelete(DeleteBehavior.Restrict);
        }
       
        public DbSet<Ucenik> Ucenik { get; set; }
        public DbSet<UpisUOdjeljenje> UpisUOdjeljenje { get; set; }
        public DbSet<Odjeljenje> Odjeljenje { get; set; }
        public DbSet<Nastavnik> Nastavnik { get; set; }
        public DbSet<MaturskiIspit> MaturskiIspit { get; set; }
        public DbSet<MaturskiIspitStavka> MaturskiIspitStavka { get; set; }
    }
}
