using System;

namespace App.Models
{
    public class DesignacaoDTO
    {
        public int Id { get; set; }
        
        public string Tipo { get; set; }                

        public string Designado { get; set; }

        public string Ajudante { get; set; }
        
        public string Observacao { get; set; }

        public DateTime Data { get; set; }
        public bool SemanaAtual { get; set; }
        public string Situacao { get; set; }

    }
}