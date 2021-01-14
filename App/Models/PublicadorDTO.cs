using System;

namespace App.Models
{
    public class PublicadorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime? DataDaUltimaDesignacao { get; set; }        
        public DateTime? DataDaUltimaDesignacaoComoAjudante { get; set; }
    }
}