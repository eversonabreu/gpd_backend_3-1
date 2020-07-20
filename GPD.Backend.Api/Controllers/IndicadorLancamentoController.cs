using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GPD.Backend.Api.Controllers
{
    [Route("indicador-lancamento")]
    public class IndicadorLancamentoController : Controller<IndicadorLancamentoModel, IndicadorLancamento>
    {
        private readonly IIndicadorLancamentoRepository indicadorLancamentoRepository;
        private readonly IIndicadorLancamentosService indicadorLancamentosService;

        public IndicadorLancamentoController(IIndicadorLancamentoRepository indicadorLancamentoRepository,
            IHttpContextAccessor httpContextAccessor, IIndicadorLancamentosService indicadorLancamentosService) : base(indicadorLancamentoRepository, httpContextAccessor) 
        {
            this.indicadorLancamentoRepository = indicadorLancamentoRepository;
            this.indicadorLancamentosService = indicadorLancamentosService;
        }

        [Route("gerar-lancamentos"), HttpPost]
        public ImpotacaoLancamentosDto GerarLancamentos([FromBody] IEnumerable<ImpotacaoLancamentos> lancamentos) => indicadorLancamentoRepository.GerarLancamentos(lancamentos);

        [Route("por-projeto/{idProjeto:long}/{mesInicial:int}/{anoInicial:int}/{mesFinal:int}/{anoFinal:int}"), HttpGet]
        public IEnumerable<IndicadorLancamentosResultado> PorProjeto(long idProjeto, int mesInicial, int anoInicial, int mesFinal, int anoFinal)
        {
            var result = indicadorLancamentosService.ObterResultadosPorProjeto(idProjeto, mesInicial, anoInicial, mesFinal, anoFinal);

            for (int index = 0; index < result.Count; index++)
            {
                var item = result[index];
                item.Script = ObterScriptAtingimento(item.ValorAtingimento, index.ToString());
            }

            return result;
        }

        [Route("por-indicador/{idProjeto:long}/{idIndicador:long}/{mesInicial:int}/{anoInicial:int}/{mesFinal:int}/{anoFinal:int}"), HttpGet]
        public IndicadorLancamentosResultado PorIndicador(long idProjeto, long idIndicador, int mesInicial, int anoInicial, int mesFinal, int anoFinal)
        {
            var result = indicadorLancamentosService.ObterResultadosPorIndicador(idProjeto, idIndicador, mesInicial, anoInicial, mesFinal, anoFinal);
            result.Script = ObterScriptAtingimento(result.ValorAtingimento, "0");
            return result;
        }

        [Route("por-usuario/{idArvore:long}/{idUsuario:long}/{mesInicial:int}/{anoInicial:int}/{mesFinal:int}/{anoFinal:int}"), HttpGet]
        public UsuarioIndicadorLancamentosResultado PorUsuario(long idArvore, long idUsuario, int mesInicial, int anoInicial, int mesFinal, int anoFinal)
        {
            var result = indicadorLancamentosService.ObterResultadosParaUsuario(idArvore, idUsuario, mesInicial, anoInicial, mesFinal, anoFinal);
            for (int index = 0; index < result.Indicadores.Count; index++)
            {
                var item = result.Indicadores[index];
                item.Script = ObterScriptAtingimento(item.ValorAtingimento, index.ToString());
            }

            result.Script1 = ObterScriptAtingimento(result.ValorPonderadoIndividual, "script1");
            result.Script2 = ObterScriptAtingimento(result.ValorPonderadoCorporativo, "script2");
            result.Script3 = ObterScriptAtingimento(result.ValorPonderadoFinal, "script3");
            return result;
        }

        private string ObterScriptAtingimento(decimal valorAtingimento, string indexIndicador)
        {
            var range = new List<string>();
            decimal valorInicial = 0m;
            while (valorInicial < valorAtingimento)
            {
                range.Add(valorInicial.ToString());
                valorInicial += 20m;
            }

            if (valorInicial > valorAtingimento)
            {
                range.Add(valorInicial.ToString());
            }
            else if (valorAtingimento == 0m)
            {
                range.Add("0");
                range.Add("20");
                range.Add("40");
                range.Add("60");
                range.Add("80");
                range.Add("100");
            }

            string rangeValores = string.Join(',', range);
            string maxValue = range.Last();

            string valor = valorAtingimento.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")).Replace(",", string.Empty);
            var builder = new System.Text.StringBuilder();
            builder.AppendLine("google.charts.load('current', {'packages':['gauge'], 'language':'pt-BR'}); ");
            builder.AppendLine("google.charts.setOnLoadCallback(drawChart); ");
            builder.AppendLine("function drawChart() { ");
            builder.AppendLine("var data = google.visualization.arrayToDataTable([ ");
            builder.AppendLine("['Label', 'Value'], ");
            builder.AppendLine($"['', {valor}]]); ");
            builder.AppendLine("var options = { ");
            builder.AppendLine("redFrom: 0, redTo: 40, yellowFrom:40, yellowTo: 75, ");
            builder.AppendLine($"greenFrom:75, greenTo: {maxValue}, ");
            builder.AppendLine($"majorTicks: [{rangeValores}], ");
            builder.AppendLine($"minorTicks: 2, max:{maxValue} ");
            builder.AppendLine("}; ");
            builder.AppendLine($"var chart = new google.visualization.Gauge(document.getElementById('div_indicador_{indexIndicador}')); ");
            builder.AppendLine("var formatter = new google.visualization.NumberFormat({ ");
            builder.AppendLine("suffix: '%' ");
            builder.AppendLine("}); ");
            builder.AppendLine("formatter.format(data, 1); ");
            builder.AppendLine("chart.draw(data, options);}");
            return builder.ToString();
        }
    }
}
