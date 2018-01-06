using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class KsiazkaController : Controller
    {
        PGDbContext DB = new PGDbContext();
        // GET: Autor


        private void DodajAutorowDoKsiazek(List<Ksiazka> ksiazki)
        {
            try
            {
                foreach (var ksiazka in ksiazki)
                {
                    DodajAutorowDoKsiazki(ksiazka);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private void DodajAutorowDoKsiazki(Ksiazka ksiazka)
        {
            try
            {
                string queryWszyscyAutorzy = @"SELECT * from dbo.""Autor"" JOIN dbo.""KsiazkaAutors"" on dbo.""KsiazkaAutors"".""Autor_AutorId"" = dbo.""Autor"".""AutorId"" WHERE dbo.""KsiazkaAutors"".""Ksiazka_KsiazkaId"" = ";
                ksiazka.Autorzy = DB.Autorzy.SqlQuery(queryWszyscyAutorzy + ksiazka.KsiazkaId + " ;").ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private void ZaktualizujAutorowKsiazki(Ksiazka ksiazka, string[] wybraniAutorzy)
        {
            try
            {
                UsunPoprzednichAutorowKsiazki(ksiazka);
                if (wybraniAutorzy != null)
                {
                    foreach (var autorId in wybraniAutorzy)
                    {
                        string query = @"INSERT INTO dbo.""KsiazkaAutors"" (""Ksiazka_KsiazkaId"", ""Autor_AutorId"") VALUES ('" + ksiazka.KsiazkaId +
                                       "', '" + autorId + "');";
                        DB.Database.ExecuteSqlCommand(query);
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private void UsunPoprzednichAutorowKsiazki(Ksiazka ksiazka)
        {
            try
            {
                string query = @"DELETE from dbo.""KsiazkaAutors""  WHERE ""Ksiazka_KsiazkaId"" = " + ksiazka.KsiazkaId + ";";
                DB.Database.ExecuteSqlCommand(query);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public ActionResult Index()
        {
            string query = @"SELECT * FROM dbo.""Ksiazka""";
            List<Ksiazka> wszystkieKsiazki = new List<Ksiazka>();
            try
            {
                wszystkieKsiazki = DB.Ksiazki.SqlQuery(query).ToList();
                DodajAutorowDoKsiazek(wszystkieKsiazki);

            }
            catch (Exception e)
            {
                return View("Error");
            }

            return View(wszystkieKsiazki);
        }

        public ActionResult Create()
        {
            string query = @"SELECT * FROM dbo.""Autor""";
            List<Autor> wszyscyAutorzy = DB.Autorzy.SqlQuery(query).ToList();
            ViewBag.wszyscyAutorzy = wszyscyAutorzy;

            query = @"SELECT * FROM dbo.""Gatunek""";
            List<Gatunek> wszystkieGatunki = DB.Gatunki.SqlQuery(query).ToList();
            ViewBag.wszystkieGatunki = wszystkieGatunki;

            query = @"SELECT * FROM dbo.""Wydawnictwo""";
            List<Wydawnictwo> wszystkieWydawnictwa = DB.Wydawnictwa.SqlQuery(query).ToList();
            ViewBag.wszystkieWydawnictwa = wszystkieWydawnictwa;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ksiazka nowaKsiazka, string[] wybraniAutorzy)
        {
            string query = @"INSERT INTO dbo.""Ksiazka"" (""Nazwa"", ""WydawnictwoId"", ""GatunekId"") VALUES ('" + nowaKsiazka.Nazwa + 
                            "', '" + nowaKsiazka.WydawnictwoId + "', ' " + nowaKsiazka.GatunekId +  "');";
            try
            {
                DB.Database.ExecuteSqlCommand(query);

                string queryId = @"SELECT * FROM dbo.""Ksiazka"" WHERE ""Nazwa"" = '" + nowaKsiazka.Nazwa + "';";
                Ksiazka dodanaKsiazka = DB.Ksiazki.SqlQuery(queryId).ToList().ElementAt(0);
                ZaktualizujAutorowKsiazki(dodanaKsiazka, wybraniAutorzy);

            }
            catch (Exception e)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            string query = @"SELECT * FROM dbo.""Ksiazka"" WHERE ""KsiazkaId"" = " + id + ";";
            Ksiazka ksiazkaDoEdycji = new Ksiazka();
            try
            {
                ksiazkaDoEdycji = DB.Ksiazki.SqlQuery(query).ToList().ElementAt(0);
                DodajAutorowDoKsiazki(ksiazkaDoEdycji);
                string tmpQuery = @"SELECT * FROM dbo.""Autor""";
                List<Autor> wszyscyAutorzy = DB.Autorzy.SqlQuery(tmpQuery).ToList();
                ViewBag.wszyscyAutorzy = wszyscyAutorzy;

                tmpQuery = @"SELECT * FROM dbo.""Gatunek""";
                List<Gatunek> wszystkieGatunki = DB.Gatunki.SqlQuery(tmpQuery).ToList();
                ViewBag.wszystkieGatunki = wszystkieGatunki;

                tmpQuery = @"SELECT * FROM dbo.""Wydawnictwo""";
                List<Wydawnictwo> wszystkieWydawnictwa = DB.Wydawnictwa.SqlQuery(tmpQuery).ToList();
                ViewBag.wszystkieWydawnictwa = wszystkieWydawnictwa;
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(ksiazkaDoEdycji);
        }

        [HttpPost]
        public ActionResult Edit(Ksiazka ksiazka, string[] wybraniAutorzy)
        {
            string query = @"UPDATE dbo.""Ksiazka"" SET ""Nazwa"" = '" + ksiazka.Nazwa + "' , \"WydawnictwoId\" = " 
                            + ksiazka.WydawnictwoId + ", \"GatunekId\" = "  + ksiazka.GatunekId + " WHERE \"KsiazkaId\" = " + ksiazka.KsiazkaId + ";";
            try
            {
                DB.Database.ExecuteSqlCommand(query);
                ZaktualizujAutorowKsiazki(ksiazka, wybraniAutorzy);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            string query = @"DELETE from dbo.""Ksiazka""  WHERE ""KsiazkaId"" = " + id + ";";
            try
            {
                DB.Database.ExecuteSqlCommand(query);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return RedirectToAction("Index");

        }
    }
}