using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
namespace Library.Controllers
{
    public class PGDbContextController : Controller
    {
        PGDbContext DB = new PGDbContext();
        public ActionResult ResetDB()
        {
            try
            {
                string query = @"DELETE from dbo.""Transakcja"";";
                DB.Database.ExecuteSqlCommand(query);

                query = @"DELETE from dbo.""Ksiazka"";";
                DB.Database.ExecuteSqlCommand(query);

                query = @"DELETE from dbo.""Czytelnik"";";
                DB.Database.ExecuteSqlCommand(query);

                query = @"DELETE from dbo.""Miasto"";";
                DB.Database.ExecuteSqlCommand(query);

                query = @"DELETE from dbo.""Autor"";";
                DB.Database.ExecuteSqlCommand(query);

                query = @"DELETE from dbo.""Gatunek"";";
                DB.Database.ExecuteSqlCommand(query);

                query = @"DELETE from dbo.""Wydawnictwo"";";
                DB.Database.ExecuteSqlCommand(query);
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return RedirectToAction("Index", "Home", null);
        }
    }
}