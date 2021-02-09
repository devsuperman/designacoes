namespace App.Models
{
    public static class SituacoesDaDesignacao
    {
        public static string[] SituacoesDeFluxoNormal => new[] { AguardandoAprovacao, AguardandoEnvio, Enviada, Confirmada };
        public const string AguardandoAprovacao = "Aguardando Aprovação";
        public const string AguardandoEnvio = "Aguardando Envio";
        public const string Enviada = "Enviada";
        public const string Confirmada = "Confirmada";
        public const string Cancelada = "Cancelada";
    }
}