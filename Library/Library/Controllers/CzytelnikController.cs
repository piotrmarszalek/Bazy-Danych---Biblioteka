using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class CzytelnikController : Controller
    {

        PGDbContext DB = new PGDbContext();
        // GET: Czytelnik
        public ActionResult Index()
        {
            string query = @"SELECT * FROM dbo.""Czytelnik""";
            List<Czytelnik> wszyscyCzytelnicy = new List<Czytelnik>();
            try
            {
                wszyscyCzytelnicy = DB.Czytelnicy.SqlQuery(query).ToList();
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(wszyscyCzytelnicy);
        }

        public ActionResult Create()
        {
            string query = @"SELECT * FROM dbo.""Miasto""";
            List<Miasto> wszystkieMiasta = DB.Miasta.SqlQuery(query).ToList();
            ViewBag.wszystkieMiasta = wszystkieMiasta;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Czytelnik nowyCzytelnik)
        {
            string query = @"INSERT INTO dbo.""Czytelnik"" (""Imie"", ""Nazwisko"", ""MiastoId"", ""Login"") VALUES ('" + nowyCzytelnik.Imie + "', '" + nowyCzytelnik.Nazwisko + "', " + nowyCzytelnik.MiastoId + ", '" + nowyCzytelnik.Login + "');";

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
            string query = @"SELECT * FROM dbo.""Czytelnik"" WHERE ""CzytelnikId"" = " + id + ";";
            Czytelnik czytelnikDoEdycji = new Czytelnik();
            try
            {
                string miastaQuery = @"SELECT * FROM dbo.""Miasto""";
                List<Miasto> wszystkieMiasta = DB.Miasta.SqlQuery(miastaQuery).ToList();
                ViewBag.wszystkieMiasta = wszystkieMiasta;

                czytelnikDoEdycji = DB.Czytelnicy.SqlQuery(query).ToList().ElementAt(0);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(czytelnikDoEdycji);
        }

        [HttpPost]
        public ActionResult Edit(Czytelnik czytelnik)
        {
            string query = @"UPDATE dbo.""Czytelnik"" SET ""Imie"" = '" + czytelnik.Imie + "', \"Nazwisko\" = '" + czytelnik.Nazwisko + "', \"MiastoId\" = " + czytelnik.MiastoId +" WHERE \"CzytelnikId\" = " + czytelnik.CzytelnikId + ";";
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
            string query = @"DELETE from dbo.""Czytelnik""  WHERE ""CzytelnikId"" = " + id + ";";
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