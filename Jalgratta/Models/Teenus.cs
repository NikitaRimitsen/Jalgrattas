using System.ComponentModel.DataAnnotations;

namespace Jalgratta.Models
{
    public class Teenus
    {
        [Key]
        public int TeenusId { set; get; }
        public string Info { get; set; }
        public int Hind { get; set; }
        public DateTime Aeg { get; set; }
    }
}
