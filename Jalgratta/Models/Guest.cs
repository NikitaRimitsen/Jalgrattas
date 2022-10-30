using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Jalgratta.Models
{
    public class Guest
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "On vaja sisesta oma nime!!!")]
        public string Nimi { get; set; }
        [Required(ErrorMessage = "On vaja sisesta oma perekonanimi!!!")]
        public string Perekonanimi { get; set; }
        [Required(ErrorMessage = "On vaja sisestada email!!!")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Viga emaili sisestamiseks")]
        public string Email { get; set; }
        public int vanus { get; set; }
        [Required(ErrorMessage = "Sisesta oma tel. number!!!")]
        [RegularExpression(@"\+372.+", ErrorMessage = "Vale telefoni number. Alguses on +372..")]
        public string Phone { get; set; }
    }
}
