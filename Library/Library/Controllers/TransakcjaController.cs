using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class TransakcjaController : Controller
    {
        PGDbContext DB = new PGDbContext();
        // GET: Transakcja
        public ActionResult Index()
        {
            string query = @"SELECT * FROM dbo.""Transakcja""";
            List<Transakcja> wszystkieTransakcje = new List<Transakcja>();
            try
            {
                wszystkieTransakcje = DB.Transakcje.SqlQuery(query).ToList();

            }
            catch (Exception e)
            {
                return View("Error");
            }

            return View(wszystkieTransakcje);
        }

        public ActionResult Create()
        {
            string query = @"SELECT * FROM dbo.""Czytelnik""";
            List<Czytelnik> wszyscyCzytelnicy = DB.Czytelnicy.SqlQuery(query).ToList();
            ViewBag.wszyscyCzytelnicy = wszyscyCzytelnicy;

            query = @"SELECT * FROM dbo.""Ksiazka""";
            List<Ksiazka> wszystkieKsiazki = DB.Ksiazki.SqlQuery(query).ToList();
            ViewBag.wszystkieKsiazki = wszystkieKsiazki;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Transakcja nowaTransakcja)
        {
            string query = @"INSERT INTO dbo.""Transakcja"" (""DataWypozyczenia"", ""CzytelnikId"", ""KsiazkaId"") VALUES ('" + nowaTransakcja.DataWypozyczenia +
                            "', '" + nowaTransakcja.CzytelnikId + "', ' " + nowaTransakcja.KsiazkaId + "');";
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

        public ActionResult Edit(int id)
        {
            string query = @"SELECT * FROM dbo.""Transakcja"" WHERE ""TransakcjaId"" = " + id + ";";
            Transakcja transakcjaDoEdycji = new Transakcja();
            try
            {
                transakcjaDoEdycji = DB.Transakcje.SqlQuery(query).ToList().ElementAt(0);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(transakcjaDoEdycji);
        }

        [HttpPost]
        public ActionResult Edit(Transakcja transakcja)
        {
            string query = @"UPDATE dbo.""Transakcja"" SET ""DataOddania"" = '" + transakcja.DataOddania + "' WHERE \"TransakcjaId\" = " + transakcja.TransakcjaId + ";";
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
            string query = @"DELETE from dbo.""Transakcja""  WHERE ""TransakcjaId"" = " + id + ";";
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