using System.Collections.Generic;
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

        public bool ImpedidoDeFazerPartes { get; set; } = false;

        public List<Designacao> Designacoes { get; set; }
        public List<Designacao> DesignacoesComoAjudante { get; set; }
    }
}