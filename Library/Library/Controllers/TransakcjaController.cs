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

        private string NazwaParametruDlaIndeksu(int indeks)
        {
            switch(indeks)
            {
                case 0:
                    return "tr.\"KsiazkaId\"";
                case 1:
                    return "gat.\"GatunekId\"";
                case 2:
                    return "ka.\"Autor_AutorId\"";
                case 3:
                    return "wyd.\"WydawnictwoId\"";
                case 4:
                    return "cz.\"CzytelnikId\"";
                case 5:
                    return "ms.\"MiastoId\"";
                default:
                    return "";
            }

        }

        private string TransakcjaFiltrujaceQuery(string ksiazkaId, string gatunekId, string autorId, string wydawnictwoId, string czytelnikId, string miastoId)
        {
            string queryFiltrujace = "select tr.\"TransakcjaId\", tr.\"CzytelnikId\", tr.\"KsiazkaId\", tr.\"DataWypozyczenia\", tr.\"DataOddania\" from dbo.\"Transakcja\" tr "+
                                    " join dbo.\"KsiazkaAutors\" ka on tr.\"KsiazkaId\" = ka.\"Ksiazka_KsiazkaId\""+
                                    " join dbo.\"Ksiazka\" ks on ks.\"KsiazkaId\" = tr.\"KsiazkaId\""+
                                    " join dbo.\"Wydawnictwo\" wyd on wyd.\"WydawnictwoId\" = ks.\"WydawnictwoId\""+
                                    " join dbo.\"Gatunek\" gat on gat.\"GatunekId\" = ks.\"GatunekId\""+
                                    " join dbo.\"Czytelnik\" cz on cz.\"CzytelnikId\" = tr.\"CzytelnikId\""+
                                    " join dbo.\"Miasto\" ms on ms.\"MiastoId\" = cz.\"MiastoId\" ";

            int licznikParametrowNieNull = 0;
            if (ksiazkaId != null || gatunekId != null || autorId != null || wydawnictwoId != null)
            {
                queryFiltrujace += " WHERE ";
                List<string> parametryFiltru = new List<string>();
                parametryFiltru.Add(ksiazkaId);
                parametryFiltru.Add(gatunekId);
                parametryFiltru.Add(autorId);
                parametryFiltru.Add(wydawnictwoId);
                parametryFiltru.Add(czytelnikId);
                parametryFiltru.Add(miastoId);

                for(int i=0; i<parametryFiltru.Count();i++)
                {
                    if(parametryFiltru[i]!= null)
                    {
                        if(licznikParametrowNieNull > 0)
                        {
                            queryFiltrujace += " AND ";
                        }
                        queryFiltrujace += NazwaParametruDlaIndeksu(i) + " = " + parametryFiltru[i];
                        licznikParametrowNieNull++;
                    }
                }
            }

            queryFiltrujace += ";";
            return queryFiltrujace;
        }

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

        public ActionResult Details(string ksiazkaId, string gatunekId, string autorId, string wydawnictwoId, string czytelnikId, string miastoId)
        {
            try
            {
                string tmpQuery = @"SELECT * FROM dbo.""Autor"";";
                List<Autor> wszyscyAutorzy = DB.Autorzy.SqlQuery(tmpQuery).ToList();
                ViewBag.wszyscyAutorzy = wszyscyAutorzy;

                tmpQuery = @"SELECT * FROM dbo.""Gatunek"";";
                List<Gatunek> wszystkieGatunki = DB.Gatunki.SqlQuery(tmpQuery).ToList();
                ViewBag.wszystkieGatunki = wszystkieGatunki;

                tmpQuery = @"SELECT * FROM dbo.""Wydawnictwo"";";
                List<Wydawnictwo> wszystkieWydawnictwa = DB.Wydawnictwa.SqlQuery(tmpQuery).ToList();
                ViewBag.wszystkieWydawnictwa = wszystkieWydawnictwa;

                tmpQuery = @"SELECT * FROM dbo.""Czytelnik"";";
                List<Czytelnik> wszyscyCzytelnicy = DB.Czytelnicy.SqlQuery(tmpQuery).ToList();
                ViewBag.wszyscyCzytelnicy = wszyscyCzytelnicy;

                tmpQuery = @"SELECT * FROM dbo.""Miasto"";";
                List<Miasto> wszystkieMiasta = DB.Miasta.SqlQuery(tmpQuery).ToList();
                ViewBag.wszystkieMiasta = wszystkieMiasta;

                tmpQuery = @"SELECT * FROM dbo.""Ksiazka"";";
                List<Ksiazka> wszystkieKsiazki = DB.Ksiazki.SqlQuery(tmpQuery).ToList();
                ViewBag.wszystkieKsiazki = wszystkieKsiazki;

                string queryFiltrujace = TransakcjaFiltrujaceQuery(ksiazkaId, gatunekId, autorId, wydawnictwoId, czytelnikId, miastoId);

                List <Transakcja> przefiltrowaneTransakcje = DB.Transakcje.SqlQuery(queryFiltrujace).ToList();

                return View(przefiltrowaneTransakcje);
            }
            catch(Exception e)
            {
                return View("Error");
            }
            return View();
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