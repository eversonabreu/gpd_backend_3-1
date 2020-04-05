namespace GPD.Backend.Domain.Services.Contracts
{
    public interface IIndicadorLancamentosService
    {
        IndicadorLancamentosResultado ObterResultadoPorIndicador(long idProjeto, long idIndicador, int mesInicial, int anoInicial, int mesFinal, int anoFinal);
    }

    public struct IndicadorLancamentosResultado
    {
        public string NomeIndicador { get; set; }

        public string UnidadeMedida { get; set; }

        public decimal ValorMeta { get; set; }

        public decimal ValorRealizado { get; set; }

        public decimal ValorAtingimento { get; set; }
    }
}
