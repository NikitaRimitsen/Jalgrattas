using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Jalgratta.Models
{
    public class Teenusetelimuse
    {
        [Key]
        public int TelimusId { get; set; }
        public string Nimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string telnumber { get; set; }
        public int TeenusId { get; set; }
        public Teenus? Teenus { get; set; }
        public int TootajadId { get; set; }
        public Tootajad? Tootajad { get; set; }
        
        public DateTime Kuupaev { get; set; }
    }
}
