using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Entities;

namespace TekstilTakipSistemiCore.DATA.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
        public ProjectContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("server=MERVE;database=TekstilTakipDB;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UygulamaKullanici>().HasData(
                new UygulamaKullanici { Id=1, Ad = "Admin", Soyad = "Admin", KullaniciAdi = "admin.admin", Sifre = "123", AktifMi = true, OlusturmaZamani = DateTime.Now }
                );

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UygulamaKullanici> UygulamaKullanicilar { get; set; }
        public DbSet<Mefrusat> Mefrusatlar { get; set; }
        public DbSet<Kimliklendirme> Kimliklendirmeler { get; set; }
        public DbSet<PaketAlani> PaketAlanlari { get; set; }
        public DbSet<TemizDepo> TemizDepolari { get; set; }
        public DbSet<KirliDepo> KirliDepolari { get; set; }
    }
}
