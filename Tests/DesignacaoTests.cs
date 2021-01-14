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
                Data = new DateTime(2021,01,14)
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
    }
}
