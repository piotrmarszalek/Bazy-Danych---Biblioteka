using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Library.Models
{
    [Table("Transakcja")]
    public class Transakcja
    {
        public int TransakcjaId { get; set; }
        [Required]
        public int CzytelnikId { get; set; }
        [Required]
        public virtual Czytelnik Czytelnik { get; set; }
        [Required]
        public int KsiazkaId { get; set; }
        [Required]
        public virtual Ksiazka Ksiazka { get; set; }
        [Required]
        public DateTime DataWypozyczenia { get; set; }
        public DateTime DataOddania { get; set; }
    }
}