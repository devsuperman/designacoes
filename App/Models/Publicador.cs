using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Publicador
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sexo { get; set; }
    }
}