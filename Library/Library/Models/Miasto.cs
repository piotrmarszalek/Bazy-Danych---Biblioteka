using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    [Table("Miasto")]
    public class Miasto
    {
        public int MiastoId { get; set; }
        [Index(IsUnique = true)]
        [Required]
        public string Nazwa { get; set; }
    }
}