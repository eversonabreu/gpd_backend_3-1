using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GPD.Backend.Domain.Services.Implementations
{
    public class IndicadorLancamentosService : IIndicadorLancamentosService
    {
        private readonly IIndicadorLancamentoRepository indicadorLancamentoRepository;
        private readonly IIndicadorRepository indicadorRepository;

        public IndicadorLancamentosService(IIndicadorLancamentoRepository indicadorLancamentoRepository,
            IIndicadorRepository indicadorRepository)
        {
            this.indicadorLancamentoRepository = indicadorLancamentoRepository;
            this.indicadorRepository = indicadorRepository;
        }

        private IEnumerable<Lancamentos> ObterLancamentos(long idProjeto, long idIndicador, DateTime dataInicial, DateTime dataFinal)
        {
            var lancamentos = indicadorLancamentoRepository.Filter(item => item.IdProjeto == idProjeto && item.IdIndicador == idIndicador);
            if (lancamentos?.Any() ?? false)
            {
                var listaLancamentos = new List<Lancamentos>();
                foreach (var lanc in lancamentos)
                {
                    var dataReferencia = new DateTime(lanc.Ano, lanc.Mes, 1);
                    if (dataReferencia >= dataInicial && dataReferencia <= dataFinal)
                    {
                        listaLancamentos.Add(new Lancamentos
                        {
                            ValorMeta = lanc.ValorMeta,
                            ValorRealizado = lanc.ValorRealizado
                        });
                    }
                }

                if (listaLancamentos.Any())
                {
                    return listaLancamentos;
                }
            }

            return null;
        }

        private string ObterValor(IEnumerable<Lancamentos> lancamentos, bool realizado, bool acumula)
        {
            if (lancamentos is null)
            {
                return "0";
            }

            decimal retorno = 0m;

            if (realizado)
            {
                if (acumula)
                {
                    retorno = lancamentos.Sum(item => item.ValorRealizado);
                }
                else
                {
                    retorno = lancamentos.Average(item => item.ValorRealizado);
                    retorno = decimal.Round(retorno, 2);
                }
            }
            else
            {
                if (acumula)
                {
                    retorno = lancamentos.Sum(item => item.ValorMeta);
                }
                else
                {
                    retorno = lancamentos.Average(item => item.ValorMeta);
                    retorno = decimal.Round(retorno, 2);
                }
            }

            string resultado = retorno.ToString(CultureInfo.GetCultureInfo("en-US"));
            return resultado;
        }

        private decimal ObterValorCalculado(long idProjeto, Indicador indicador, DateTime dataInicial, DateTime dataFinal, bool realizado)
        {
            string formula = indicador.Formula;
            int posicao = 0;

            while ((posicao = formula.IndexOf('[')) > -1)
            {
                var builder = new StringBuilder();
                while (true)
                {
                    posicao++;
                    char ch = formula[posicao];
                    if (ch == ']') break;
                    builder.Append(ch);
                }

                string identificador = builder.ToString();
                var novoIndicador = indicadorRepository.FirstOrDefault(item => item.Identificador == identificador, loadDependencies: false);
                if (novoIndicador is null)
                {
                    throw new Exception($"A fórmula: '{indicador.Formula}' não contem indicadores válidos.");
                }

                var lancamentos = ObterLancamentos(idProjeto, novoIndicador.Id, dataInicial, dataFinal);
                string valor = ObterValor(lancamentos, realizado, realizado ? indicador.AcumulaRealizado : indicador.AcumulaMeta);
                formula = formula.Replace($"[{identificador}]", valor);
            }

            try
            {
                var valorFormula = new DataTable().Compute(formula, string.Empty);
                decimal valorDecimal = Convert.ToDecimal(valorFormula);
                return decimal.Round(valorDecimal, 2);
            }
            catch
            {
                return 0m;
            }
        }

        public IndicadorLancamentosResultado ObterResultadoPorIndicador(long idProjeto, long idIndicador, int mesInicial, int anoInicial, int mesFinal, int anoFinal)
        {
            var dataInicial = new DateTime(anoInicial, mesInicial, 1);
            var dataFinal = Utils.ObterDataNoUltimoDiaDoMes(mesFinal, anoFinal);
            var indicador = indicadorRepository.GetById(idIndicador);
            decimal valorMeta = 0m;
            decimal valorRealizado = 0m;
            decimal valorAtingimento = 0m;

            if (indicador.TipoCalculo == TipoCalculo.NaoCalculado)
            {
                var lancamentos = ObterLancamentos(idProjeto, idIndicador, dataInicial, dataFinal);
                if (lancamentos != null)
                {
                    if (indicador.AcumulaMeta)
                    {
                        valorMeta = lancamentos.Sum(item => item.ValorMeta);
                    }
                    else
                    {
                        valorMeta = lancamentos.Average(item => item.ValorMeta);
                        valorMeta = decimal.Round(valorMeta, 2);
                    }

                    if (indicador.AcumulaRealizado)
                    {
                        valorRealizado = lancamentos.Sum(item => item.ValorRealizado);
                    }
                    else
                    {
                        valorRealizado = lancamentos.Average(item => item.ValorRealizado);
                        valorRealizado = decimal.Round(valorRealizado, 2);
                    }
                }
            }
            else if (indicador.TipoCalculo == TipoCalculo.SomenteMeta)
            {
                valorMeta = ObterValorCalculado(idProjeto, indicador, dataInicial, dataFinal, false);
            }
            else if (indicador.TipoCalculo == TipoCalculo.SomenteRealizado)
            {
                valorRealizado = ObterValorCalculado(idProjeto, indicador, dataInicial, dataFinal, true);
            }
            else
            {
                valorMeta = ObterValorCalculado(idProjeto, indicador, dataInicial, dataFinal, false);
                valorRealizado = ObterValorCalculado(idProjeto, indicador, dataInicial, dataFinal, true);
            }

            if (valorMeta != 0m && indicador.ValorPercentualCriterio != 0m)
            {
                decimal valor1 = (valorRealizado - valorMeta) / valorMeta;
                decimal valor2 = 20m / indicador.ValorPercentualCriterio;
                decimal valor3 = valor1 * valor2;

                if (indicador.PossuiCardinalidade)
                {
                    valor3 = (1m + valor3) * 100m;
                }
                else
                {
                    valor3 = (1m - valor3) * 100m;
                }

                //falta colocar as travas inferiores e superiores
                valorAtingimento = decimal.Round(valor3, 2);
            }

            return new IndicadorLancamentosResultado
            {
                NomeIndicador = indicador.Nome,
                UnidadeMedida = indicador.UnidadeMedida.Sigla,
                ValorAtingimento = valorAtingimento,
                ValorMeta = valorMeta,
                ValorRealizado = valorRealizado
            };
        }
    }

    internal class Lancamentos
    {
        public decimal ValorMeta { get; set; }

        public decimal ValorRealizado { get; set; }
    }
}
