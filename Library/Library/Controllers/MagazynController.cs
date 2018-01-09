using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class MagazynController : Controller
    {

        PGDbContext DB = new PGDbContext();
        // GET: Magazyn
        public ActionResult Index()
        {
            string query = @"SELECT * FROM dbo.""Magazyn""";
            List<Magazyn> elementyMagazynu = new List<Magazyn>();
            try
            {
                elementyMagazynu = DB.Magazyn.SqlQuery(query).ToList();
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(elementyMagazynu);
        }

        public ActionResult Create()
        {
            string query = @"SELECT * FROM dbo.""Ksiazka""";
            List<Ksiazka> wszystkieKsiazki = DB.Ksiazki.SqlQuery(query).ToList();
            ViewBag.wszystkieKsiazki = wszystkieKsiazki;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Magazyn elementMagazynu)
        {
            string query = @"INSERT INTO dbo.""Magazyn"" (""DostepnaIlosc"", ""KsiazkaId"") VALUES (" + elementMagazynu.DostepnaIlosc + ", " + elementMagazynu.Ksiazka + ");";

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
            string query = @"SELECT * FROM dbo.""Magazyn"" WHERE ""KsiazkaId"" = " + id + ";";
            Magazyn elementMagazynuDoEdycji = new Magazyn();
            try
            {
                elementMagazynuDoEdycji = DB.Magazyn.SqlQuery(query).ToList().ElementAt(0);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(elementMagazynuDoEdycji);
        }

        [HttpPost]
        public ActionResult Edit(Magazyn elementMagazynu)
        {
            string query = @"UPDATE dbo.""Magazyn"" SET  ""DostepnaIlosc"" = " + elementMagazynu.DostepnaIlosc + " WHERE \"KsiazkaId\"= " + elementMagazynu.KsiazkaId + ";";
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
            string query = @"DELETE from dbo.""Magazyn""  WHERE ""KsiazkaId"" = " + id + ";";
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