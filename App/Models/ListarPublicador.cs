namespace App.Models
{
    public class ListarPublicador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Designacao UltimaDesignacao { get; set; }        
        public string DataUltimaDesigacao => UltimaDesignacao != null ? $"Última designacao em {UltimaDesignacao.Data.ToShortDateString()}": "Nunca Designado";
        public string NomeComData => $"{Nome} - {DataUltimaDesigacao}";
    }
}