using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Designacao
    {
        public Designacao()
        {
            
        }
        public Designacao(Designacao designacao)
        {
            this.Observacao=designacao.Observacao;
            this.DesignadoId = designacao.DesignadoId;
            this.AjudanteId = designacao.AjudanteId;
            this.Data = designacao.Data;
            this.Tipo = designacao.Tipo;
            this.EhSubstituicao = true;
        }

        public int Id { get; set; }

        [Required]
        public string Tipo { get; set; }

        [InverseProperty("Designacoes")]
        public Publicador Designado { get; set; }

        [Required, Display(Name = "Designado")]
        public int DesignadoId { get; set; }

        [InverseProperty("DesignacoesComoAjudante")]
        public Publicador Ajudante { get; set; }

        [Display(Name = "Ajudante")]
        public int? AjudanteId { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Data { get; set; }
        
        public int? SubstituicaoId { get; set; }
        public Designacao Substituicao { get; set; }

        public bool EhSubstituicao { get; set; }
        public bool FoiSubstituida => (Substituicao != null);
        public string MotivoDaSubstituicao { get; set; }

        public DateTime DataDeRegistro { get; set; } = DateTime.Now;
        public string Situacao { get; set; } = SituacoesDaDesignacao.AguardandoAprovacao;

        public bool SemanaAtual(DateTime hoje)
        {
            var segundaDessaSemana = this.Data.AddDays(-3);
            var domingoDessaSemana = this.Data.AddDays(3);

            return (hoje >= segundaDessaSemana && hoje <= domingoDessaSemana);
        }

        internal void Atualizar(DateTime data, int designadoId, int? ajudanteId, string tipo, string observacao)
        {
            this.Data = data;
            this.DesignadoId = designadoId;
            this.AjudanteId = ajudanteId;
            this.Tipo = tipo;
            this.Observacao = observacao;
        }

        public void Avancar()
        {
            var situacoes = SituacoesDaDesignacao.SituacoesDeFluxoNormal;
            var indiceSituacaoAtual = 0;

            for (int i = 0; i < situacoes.Length; i++)
            {
                if (this.Situacao == situacoes[i])
                {
                    indiceSituacaoAtual = i;
                    break;
                }
            }

            var proximoIndice = indiceSituacaoAtual + 1;

            if (proximoIndice < situacoes.Length)
                this.Situacao = situacoes[proximoIndice];
        }

        public void Substituir(Designacao substituicao, string motivo)
        {
            this.Situacao = SituacoesDaDesignacao.Cancelada;
            this.MotivoDaSubstituicao = motivo;            
            substituicao.MarcarComoSubstituicao();
            substituicao.Id = 0; //XGH in veins!
            this.Substituicao = substituicao;
        }

        private void MarcarComoSubstituicao() => this.EhSubstituicao = true;
    }

}