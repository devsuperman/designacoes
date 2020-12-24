using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Designacao
    {
        public int Id { get; set; }
        
        [Required]
        public string Tipo { get; set; }                

        public Publicador Designado { get; set; }
        [Required]
        public int DesignadoId { get; set; }

        public Publicador Ajudante { get; set; }
        
        public int? AjudanteId { get; set; }

        public string Observacao { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public DateTime DataDeRegistro { get; set; } = DateTime.Now;        
    }
}