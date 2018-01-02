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
        [Required]
        public int KsiazkaId { get; set; }
        [Required]
        public virtual Ksiazka Ksiazka { get; set; }
        [Required]
        public int DostepnaIlosc { get; set; }
    }
}