using Xunit;
using System;
using App.Models;

namespace Tests
{
    public class DesignacaoTests
    {
        [Theory]
        [InlineData("2021-01-11")]
        [InlineData("2021-01-12")]
        [InlineData("2021-01-13")]
        [InlineData("2021-01-14")]
        [InlineData("2021-01-15")]
        [InlineData("2021-01-16")]
        [InlineData("2021-01-17")]
        public void DeveRetornarVerdadeiroSeHojeForDaMesmaSemanaDaDesignacao(string dataString)
        {
            var hoje = Convert.ToDateTime(dataString);

            var designacao = new Designacao
            {
                Data = new DateTime(2021, 01, 14)
            };

            Assert.True(designacao.SemanaAtual(hoje));
        }

        [Theory]
        [InlineData("2021-01-10")]
        [InlineData("2021-01-09")]
        [InlineData("2021-01-08")]
        [InlineData("2021-01-18")]
        [InlineData("2021-01-19")]
        [InlineData("2021-01-20")]
        public void DeveRetornarFalsoSeHojeNaoForDaMesmaSemanaDaDesignacao(string dataString)
        {
            var hoje = Convert.ToDateTime(dataString);

            var designacao = new Designacao
            {
                Data = new DateTime(2021, 01, 14)
            };

            Assert.False(designacao.SemanaAtual(hoje));
        }

        [Fact]
        public void AoCriarNovaDesignacaoDeveEstarAguardandoAprovacao()
        {
            var designacao = new Designacao();
            Assert.Equal(SituacoesDaDesignacao.AguardandoAprovacao, designacao.Situacao);
        }


        [Fact]
        public void SeASituacaoEstiverVaziaIrParaAguardandoEnvio()
        {
            var designacao = new Designacao();
            designacao.Situacao = "";
            designacao.Avancar();

            Assert.Equal(SituacoesDaDesignacao.AguardandoEnvio, designacao.Situacao);
        }

        [Fact]
        public void AoAvancarDeveIrDeAguardandoAprovacaoParaAguardandoEnvio()
        {
            var designacao = new Designacao();
            designacao.Avancar();

            Assert.Equal(SituacoesDaDesignacao.AguardandoEnvio, designacao.Situacao);
        }

        [Fact]
        public void AoAvancarDeveIrDeAguardandoEnvioParaEnviada()
        {
            var designacao = new Designacao();
            designacao.Avancar();
            designacao.Avancar();

            Assert.Equal(SituacoesDaDesignacao.Enviada, designacao.Situacao);
        }

        [Fact]
        public void AoAvancarDeveIrDeEnviadaParaConfirmada()
        {
            var designacao = new Designacao();
            designacao.Avancar();
            designacao.Avancar();
            designacao.Avancar();

            Assert.Equal(SituacoesDaDesignacao.Confirmada, designacao.Situacao);
        }

        [Fact]
        public void SeJaEstiverConfirmadaEAvancarNadaMuda()
        {
            var designacao = new Designacao();
            designacao.Avancar();
            designacao.Avancar();
            designacao.Avancar();
            designacao.Avancar();
            designacao.Avancar();
            designacao.Avancar();
            designacao.Avancar();

            Assert.Equal(SituacoesDaDesignacao.Confirmada, designacao.Situacao);
        }

        [Fact]
        public void QuandoSubstituirDeveRegistrarOMotivoDaSubstituicao()
        {
            var designacao = new Designacao();
            var substituicao = new Designacao();
            var motivo = "O designado ficou doente";

            designacao.Substituir(substituicao, motivo);

            Assert.Equal(designacao.MotivoDaSubstituicao, motivo);
        }

        [Fact]
        public void QuandoSubstituirDeveRegistrarASubstituicao()
        {
            var designacao = new Designacao();
            var substituicao = new Designacao();
            var motivo = "O designado ficou doente";

            designacao.Substituir(substituicao, motivo);

            Assert.Equal(designacao.Substituicao, substituicao);
        }

        [Fact]
        public void QuandoSubstituirDeveRetornarVerdadeiro()
        {
            var designacao = new Designacao();
            var substituicao = new Designacao();
            var motivo = "O designado ficou doente";

            designacao.Substituir(substituicao, motivo);

            Assert.True(designacao.FoiSubstituida);
        }

        
        [Fact]
        public void QuandoNaoForSubstituidaDeveRetornarFalso()
        {
            var designacao = new Designacao();
            Assert.False(designacao.FoiSubstituida);
        }

        [Fact]
        public void QuandoEhUmaSubstituicaoDeveRetornarVerdadeiro()
        {
            var designacao = new Designacao();
            var substituicao = new Designacao();
            var motivo = "O designado ficou doente";

            designacao.Substituir(substituicao, motivo);

            Assert.True(substituicao.EhSubstituicao);
        }

         [Fact]
        public void QuandoNaoEhUmaSubstituicaoDeveRetornarFalso()
        {
            var designacao = new Designacao();
            Assert.False(designacao.EhSubstituicao);
        }


    }
}
