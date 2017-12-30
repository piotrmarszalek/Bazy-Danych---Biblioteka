using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Czytelnik")]
    public class Czytelnik
    {
        public int CzytelnikId { get; set; }
        [Index("ImieNazwiskoMiasto", 1, IsUnique = true)]
        public string Imie { get; set; }
        [Index("ImieNazwiskoMiasto", 2, IsUnique = true)]
        public string Nazwisko { get; set; }
        [Index("ImieNazwiskoMiasto", 3, IsUnique = true)]
        public int MiastoId { get; set; }
        public virtual Miasto Miasto { get; set; }
    }
}