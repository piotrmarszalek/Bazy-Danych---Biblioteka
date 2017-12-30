using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
namespace Library.Controllers
{
    public class GatunekController : Controller
    {
        PGDbContext DB = new PGDbContext();
        // GET: Autor
        public ActionResult Index()
        {
            string query = @"SELECT * FROM dbo.""Gatunek""";
            List<Gatunek> wszystkieGatunki = new List<Gatunek>();
            try
            {
                wszystkieGatunki = DB.Gatunki.SqlQuery(query).ToList();
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(wszystkieGatunki);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Gatunek nowyGatunek)
        {
            string query = @"INSERT INTO dbo.""Gatunek"" (""Nazwa"") VALUES ('" + nowyGatunek.Nazwa + "');";

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
            string query = @"SELECT * FROM dbo.""Gatunek"" WHERE ""GatunekId"" = " + id + ";";
            Gatunek gatunekDoEdycji = new Gatunek();
            try
            {
                gatunekDoEdycji = DB.Gatunki.SqlQuery(query).ToList().ElementAt(0);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(gatunekDoEdycji);
        }

        [HttpPost]
        public ActionResult Edit(Gatunek gatunek)
        {
            string query = @"UPDATE dbo.""Gatunek"" SET ""Nazwa"" = '" + gatunek.Nazwa + "' WHERE \"GatunekId\" = " + gatunek.GatunekId + ";";
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
            string query = @"DELETE from dbo.""Gatunek""  WHERE ""GatunekId"" = " + id + ";";
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