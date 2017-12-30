using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Magazyn")]
    public class Magazyn
    {
        [Key]
        [ForeignKey("Ksiazka")]
        public int KsiazkaId { get; set; }
        public virtual Ksiazka Ksiazka { get; set; }
        public int DostepnaIlosc { get; set; }
    }
}