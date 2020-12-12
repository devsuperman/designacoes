using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Designacao
    {
        public int Id { get; set; }
        
        [Required]
        public string Tipo { get; set; }                

        public Pessoa Publicador { get; set; }

        public Pessoa Ajudante { get; set; }
        
        [Required]
        public DateTime Data { get; set; }
        public DateTime DataDeRegistro { get; set; } = DateTime.Now;
        
    }
}