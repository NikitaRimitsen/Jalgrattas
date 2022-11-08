using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Jalgratta.Models
{
    public class Tootajad
    {
        [Key]
        public int TootajadId { set; get; }
        public string Nimi { get; set; }
        public int Vanus { get; set; }
        public int Staaz { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Sisesta oma tel. number!!!")]
        [RegularExpression(@"\+372.+", ErrorMessage = "Vale telefoni number. Alguses on +372..")]
        public string Telefon { get; set; }

    }
}
