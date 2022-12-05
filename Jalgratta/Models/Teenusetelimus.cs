using System.ComponentModel.DataAnnotations;
namespace Jalgratta.Models
{
    public class Teenusetelimus
    {
        [Key]
        public int TelimusId { get; set; }
        public int TootajadId { get; set; }
        public Tootajad? Tootajad { get; set; }
        public int TeenusId { get; set; }
        public Teenus? Teenus { get; set; }
        public int KasutajaId { get; set; }
        public Kasutaja? Kasutaja { get; set; }

        public DateTime Kuupaev { get; set; }
    }
}
