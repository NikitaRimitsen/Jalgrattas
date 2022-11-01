
using System.ComponentModel.DataAnnotations;
namespace Jalgratta.Models
{
    public class Teenusetelimus
    {
        [Key]
        public int TelimusId { get; set; }
        public int TootajaId { get; set; }
        public Tootajad Tootajad { get; set; }
        public int TeenusId { get; set; }
        public Teenus Teenus { get; set; }
        public int KasutajaId { get; set; }
        public Kasutaja Kasutaja { get; set; }

        public List<Kasutaja> KasutajaList { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Kuupaev { get; set; }
    }
}
