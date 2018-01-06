﻿using System;
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
        
        public void Seed(PGDbContext context)
        {
            context.Database.ExecuteSqlCommand("CREATE FUNCTION aktualizuj_magazyn() RETURNS trigger AS $aktualizuj_magazyn$ BEGIN INSERT INTO dbo.\"Magazyn\" (\"DostepnaIlosc\", \"KsiazkaId\") VALUES (0, NEW.\"KsiazkaId\"); RETURN NEW; END $aktualizuj_magazyn$ LANGUAGE plpgsql; ");
            context.Database.ExecuteSqlCommand("CREATE TRIGGER aktualizuj_magazyn AFTER INSERT ON dbo.\"Ksiazka\" FOR EACH ROW EXECUTE PROCEDURE aktualizuj_magazyn();");

            context.Database.ExecuteSqlCommand("CREATE FUNCTION usun_ksiazke() RETURNS trigger AS $usun_ksiazke$ BEGIN DELETE FROM dbo.\"Ksiazka\" WHERE dbo.\"Ksiazka\".\"KsiazkaId\" = old.\"Ksiazka_KsiazkaId\"; RETURN null; END $usun_ksiazke$ LANGUAGE plpgsql; ");
            context.Database.ExecuteSqlCommand("CREATE TRIGGER usun_ksiazke AFTER DELETE ON dbo.\"KsiazkaAutors\" FOR EACH ROW EXECUTE PROCEDURE usun_ksiazke();");

            context.Database.ExecuteSqlCommand("CREATE FUNCTION usun_ksiazke_z_tabeli_laczacej() RETURNS trigger AS $usun_ksiazke_z_tabeli_laczacej$ BEGIN DELETE FROM dbo.\"KsiazkaAutors\" WHERE dbo.\"KsiazkaAutors\".\"Autor_AutorId\" = old.\"AutorId\"; RETURN old; END $usun_ksiazke_z_tabeli_laczacej$ LANGUAGE plpgsql; ");
            context.Database.ExecuteSqlCommand("CREATE TRIGGER usun_ksiazke_z_tabeli_laczacej BEFORE DELETE ON dbo.\"Autor\" FOR EACH ROW EXECUTE PROCEDURE usun_ksiazke_z_tabeli_laczacej();");

            context.Database.ExecuteSqlCommand("CREATE FUNCTION usun_magazyn() RETURNS trigger AS $usun_magazyn$ BEGIN DELETE FROM dbo.\"Magazyn\" WHERE \"KsiazkaId\" = old.\"KsiazkaId\"; RETURN old; END $usun_magazyn$ LANGUAGE plpgsql; ");
            context.Database.ExecuteSqlCommand("CREATE TRIGGER usun_magazyn BEFORE DELETE ON dbo.\"Ksiazka\" FOR EACH ROW EXECUTE PROCEDURE usun_magazyn();");


        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<PGDbContext>
        {
            protected override void Seed(PGDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<PGDbContext>
        {
            protected override void Seed(PGDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        static PGDbContext()
        {
#if DEBUG
            Database.SetInitializer<PGDbContext>(new DropCreateIfChangeInitializer());
#else
        Database.SetInitializer<MyDbContext> (new CreateInitializer ());
#endif
        }

    }
}