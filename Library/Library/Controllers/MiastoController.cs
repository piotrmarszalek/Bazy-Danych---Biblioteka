using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class MiastoController : Controller
    {
        PGDbContext DB = new PGDbContext();
        // GET: Miasto
        public ActionResult Index()
        {
            string query = @"SELECT * FROM dbo.""Miasto""";
            List<Miasto> wszystkieMiasta = new List<Miasto>();
            try
            {
                wszystkieMiasta = DB.Miasta.SqlQuery(query).ToList();
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(wszystkieMiasta);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Miasto noweMiasto)
        {
            string query = @"INSERT INTO dbo.""Miasto"" (""Nazwa"") VALUES ('" + noweMiasto.Nazwa + "');";

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
            string query = @"SELECT * FROM dbo.""Miasto"" WHERE ""MiastoId"" = " + id + ";";
            Miasto miastoDoEdycji = new Miasto();
            try
            {
                miastoDoEdycji = DB.Miasta.SqlQuery(query).ToList().ElementAt(0);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(miastoDoEdycji);
        }

        [HttpPost]
        public ActionResult Edit(Miasto miasto)
        {
            string query = @"UPDATE dbo.""Miasto"" SET ""Nazwa"" = '" + miasto.Nazwa + "' WHERE \"MiastoId\" = " + miasto.MiastoId + ";";
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
            string query = @"DELETE from dbo.""Miasto""  WHERE ""MiastoId"" = " + id + ";";
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