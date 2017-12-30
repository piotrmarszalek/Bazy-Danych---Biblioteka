using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Transakcja")]
    public class Transakcja
    {
        public int TransakcjaId { get; set; }
        public int CzytelnikId { get; set; }
        public virtual Czytelnik Czytelnik { get; set; }
        public int KsiazkaId { get; set; }
        public virtual Ksiazka Ksiazka { get; set; }
        public DateTime DataWypozyczenia { get; set; }
        public DateTime DataOddania { get; set; }
    }
}