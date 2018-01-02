using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Library.Models
{
    [Table("Gatunek")]
    public class Gatunek
    {
        public int GatunekId { get; set; }
        [Index(IsUnique = true)]
        [Required]
        public string Nazwa { get; set; }
    }
}