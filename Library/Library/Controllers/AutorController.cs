using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class AutorController : Controller
    {
        PGDbContext DB = new PGDbContext();
        // GET: Autor
        public ActionResult Index()
        {
            string query = @"SELECT * FROM dbo.""Autor""";
            List<Autor> wszyscyAutorzy = new List<Autor>();
            try
            {
                wszyscyAutorzy = DB.Autorzy.SqlQuery(query).ToList();
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(wszyscyAutorzy);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Autor nowyAutor)
        {
            string query = @"INSERT INTO dbo.""Autor"" (""Imie"", ""Nazwisko"")  VALUES ('" + nowyAutor.Imie + "', '" + nowyAutor.Nazwisko + "');";

            try
            {
                DB.Database.ExecuteSqlCommand(query);
            }
            catch(Exception e)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            string query = @"SELECT * FROM dbo.""Autor"" WHERE ""AutorId"" = " + id + ";";
            Autor autorDoEdycji = new Autor();
            try
            {
                autorDoEdycji = DB.Autorzy.SqlQuery(query).ToList().ElementAt(0);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View(autorDoEdycji);
        }

        [HttpPost]
        public ActionResult Edit(Autor autor)
        {
            string query = @"UPDATE dbo.""Autor"" SET ""Imie"" = '" + autor.Imie + "' ,\"Nazwisko\" = '" + autor.Nazwisko + "' WHERE \"AutorId\" = " + autor.AutorId + ";";
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