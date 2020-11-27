using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Digital_nomads.Models;

namespace Digital_nomads.Data
{
    public class MojDbContext : DbContext
    {
        public MojDbContext(DbContextOptions<MojDbContext> options)
            : base(options)
        {
        }
        public DbSet<ChatPoruka> ChatPoruka { get; set; }
        public DbSet<Korisnik> Korsinik { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<LoginRola> LoginRola { get; set; }
        public DbSet<Projekt> Projekt { get; set; }
        public DbSet<ProjektniTim> ProjektniTim { get; set; }
        public DbSet<RoleNaProjektu> RoleNaProjektu { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Trofej> Trofej { get; set; }
        public DbSet<TrofejKorisnik>TrofejKorisnik  { get; set; }
        public DbSet<Vjestina>Vjestina  { get; set; }
        public DbSet<Photos>Photos{ get; set; }
        public DbSet<VjestinaKorisnik>VjestinaKorisnik { get; set; }
    }
}
