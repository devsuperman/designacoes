namespace App.Models
{
    public class ListarPublicador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Designacao UltimaDesignacao { get; set; }        
        public string DataUltimaDesigacao => UltimaDesignacao != null ? UltimaDesignacao.Data.ToShortDateString(): "";
        public string NomeComData => $"{Nome} - {DataUltimaDesigacao}";
    }
}