using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Library.Models
{
    public class PGDbContext : DbContext
    {
        public PGDbContext(): base(nameOrConnectionString: "PGDbContext") { }
        public virtual DbSet <Ksiazka> Ksiazki { get; set; }
        public virtual DbSet<Gatunek> Gatunki { get; set; }
        public virtual DbSet<Autor> Autorzy { get; set; }
        public virtual DbSet<Wydawnictwo> Wydawnictwa { get; set; }
        public virtual DbSet <Magazyn> Magazyn { get; set; }
        public virtual DbSet<Transakcja> Transakcje { get; set; }
        public virtual DbSet<Czytelnik> Czytelnicy { get; set; }
        public virtual DbSet<Miasto> Miasta { get; set; }
    }
}