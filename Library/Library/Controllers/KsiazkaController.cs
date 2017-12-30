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
        public ActionResult Index()
        {
            string query = @"SELECT * FROM dbo.""Ksiazka""";
            List<Ksiazka> wszystkieKsiazki = new List<Ksiazka>();
            try
            {
                wszystkieKsiazki = DB.Ksiazki.SqlQuery(query).ToList();
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
            foreach(var id in wybraniAutorzy)
            {
                string autorQuery = @"SELECT * FROM dbo.""Autor"" WHERE ""AutorId"" = " + id + ";";
                Autor autorKsiazki = DB.Autorzy.SqlQuery(autorQuery).ToList().ElementAt(0);
                nowaKsiazka.Autorzy.Add(autorKsiazki);
            }

            string query = @"INSERT INTO dbo.""Ksiazka"" (""Nazwa"", ""WydawnictwoId"", ""GatunekId"") VALUES ('" + nowaKsiazka.Nazwa + 
                            "', '" + nowaKsiazka.WydawnictwoId + "', ' " + nowaKsiazka.GatunekId +  "');";
            try
            {
                DB.Database.ExecuteSqlCommand(query);

                string queryId = @"SELECT * FROM dbo.""Ksiazka"" WHERE ""Nazwa"" = '" + nowaKsiazka.Nazwa + "';";
                Ksiazka dodanaKsiazka = DB.Ksiazki.SqlQuery(queryId).ToList().ElementAt(0);
                foreach(var autorId in wybraniAutorzy)
                {
                    string query2 = @"INSERT INTO dbo.""KsiazkaAutors"" (""Ksiazka_KsiazkaId"", ""Autor_AutorId"") VALUES ('" + dodanaKsiazka.KsiazkaId +
                                    "', '" + autorId + "');";
                    DB.Database.ExecuteSqlCommand(query2);
                }

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
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(ksiazkaDoEdycji);
        }

        [HttpPost]
        public ActionResult Edit(Wydawnictwo wydawnictwo)
        {
            string query = @"UPDATE dbo.""Wydawnictwo"" SET ""Nazwa"" = '" + wydawnictwo.Nazwa + "' WHERE \"WydawnictwoId\" = " + wydawnictwo.WydawnictwoId + ";";
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

        public ActionResult Delete(int id)
        {
            string query = @"DELETE from dbo.""Wydawnictwo""  WHERE ""WydawnictwoId"" = " + id + ";";
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