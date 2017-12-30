using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class WydawnictwoController : Controller
    {
        PGDbContext DB = new PGDbContext();
        // GET: Autor
        public ActionResult Index()
        {
            string query = @"SELECT * FROM dbo.""Wydawnictwo""";
            List<Wydawnictwo> wszystkieWydawnictwa = new List<Wydawnictwo>();
            try
            {
                wszystkieWydawnictwa = DB.Wydawnictwa.SqlQuery(query).ToList();
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(wszystkieWydawnictwa);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Wydawnictwo noweWydawnictwo)
        {
            string query = @"INSERT INTO dbo.""Wydawnictwo"" (""Nazwa"") VALUES ('" + noweWydawnictwo.Nazwa + "');";

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
            string query = @"SELECT * FROM dbo.""Wydawnictwo"" WHERE ""WydawnictwoId"" = " + id + ";";
            Wydawnictwo wydawnictwoDoEdycji = new Wydawnictwo();
            try
            {
                wydawnictwoDoEdycji = DB.Wydawnictwa.SqlQuery(query).ToList().ElementAt(0);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(wydawnictwoDoEdycji);
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