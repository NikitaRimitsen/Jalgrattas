using System.ComponentModel.DataAnnotations;
namespace Jalgratta.Models
{
    public class Kasutaja
    {
        [Key]
        public int KasutajaId { set; get; }
        public string Nimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Email { get; set; }
        public int Vanus { get; set; }
        public int telnumber { get; set; }
    }
}
