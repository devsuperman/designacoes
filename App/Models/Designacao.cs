using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Designacao
    {
        public int Id { get; set; }
        
        [Required]
        public string Tipo { get; set; }                

        [InverseProperty("Designacoes")]
        public Publicador Designado { get; set; }

        [Required]
        public int DesignadoId { get; set; }

        [InverseProperty("DesignacoesComoAjudante")]
        public Publicador Ajudante { get; set; }
        
        public int? AjudanteId { get; set; }

        public string Observacao { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public DateTime DataDeRegistro { get; set; } = DateTime.Now;        

        public bool SemanaAtual(DateTime hoje)
        {
            var segundaDessaSemana = this.Data.AddDays(-3);
            var domingoDessaSemana = this.Data.AddDays(3);
            
            return (hoje >= segundaDessaSemana && hoje <= domingoDessaSemana);  
        } 
    }
}