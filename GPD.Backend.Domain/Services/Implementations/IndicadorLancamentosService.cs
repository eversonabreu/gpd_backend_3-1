using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Exceptions;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPD.Backend.Domain.Services.Implementations
{
    public class IndicadorLancamentosService : IIndicadorLancamentosService
    {
        private readonly IIndicadorLancamentoRepository indicadorLancamentoRepository;
        private readonly IIndicadorRepository indicadorRepository;
        private readonly IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly EnvironmentService environmentService;
        private readonly IProjetoRepository projetoRepository;

        public IndicadorLancamentosService(IIndicadorLancamentoRepository indicadorLancamentoRepository,
            IIndicadorRepository indicadorRepository,
            IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository,
            IUsuarioRepository usuarioRepository,
            EnvironmentService environmentService,
            IProjetoRepository projetoRepository)
        {
            this.indicadorLancamentoRepository = indicadorLancamentoRepository;
            this.indicadorRepository = indicadorRepository;
            this.projetoEstruturaOrganizacionalRepository = projetoEstruturaOrganizacionalRepository;
            this.usuarioRepository = usuarioRepository;
            this.environmentService = environmentService;
            this.projetoRepository = projetoRepository;
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

        public IndicadorLancamentosResultado ObterResultadosPorIndicador(long idProjeto, long idIndicador, int mesInicial, int anoInicial, int mesFinal, int anoFinal)
        {
            var dataInicial = new DateTime(anoInicial, mesInicial, 1);
            var dataFinal = Utils.ObterDataNoUltimoDiaDoMes(mesFinal, anoFinal);

            if (dataInicial > dataFinal)
            {
                throw new Exception($"Ano e mês de início superam o ano e mês final.");
            }

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

                valorAtingimento = decimal.Round(valor3, 2);
            }

            return new IndicadorLancamentosResultado
            {
                NomeIndicador = indicador.Nome,
                UnidadeMedida = indicador.UnidadeMedida.Sigla,
                ValorAtingimento = valorAtingimento,
                ValorMeta = valorMeta,
                ValorRealizado = valorRealizado,
                Peso = indicador.ValorPercentualPeso
            };
        }

        private decimal ObterValorPonderadoCorporativo(long idProjeto, int mesInicial, int anoInicial, int mesFinal, int anoFinal)
        {
            var indicadoresCorporativos = projetoEstruturaOrganizacionalRepository.Filter(item => item.IdProjeto == idProjeto && item.Tipo == TipoProjetoEstruturaOrganizacional.Corporativo);
            if (indicadoresCorporativos?.Any() ?? false)
            {
                var pesosAtingimentos = new List<decimal>();
                decimal pesos = 0m;
                var listaIndicadoresCorporativos = new List<Task>();
                foreach (var ind in indicadoresCorporativos)
                {
                    long idIndicador = ind.IdIndicador.Value;
                    listaIndicadoresCorporativos.Add(Task.Run(async () =>
                    {
                        if (indicadorRepository.GetById(idIndicador, false).Corporativo)
                        {
                            var resultado = ObterResultadosPorIndicador(idProjeto, idIndicador, mesInicial, anoInicial, mesFinal, anoFinal);
                            pesosAtingimentos.Add(resultado.ValorAtingimento * resultado.Peso);
                            pesos += resultado.Peso;
                        }
                        await Task.CompletedTask;
                    }));
                }

                Task.WaitAll(listaIndicadoresCorporativos.ToArray());

                if (pesosAtingimentos.Any() && pesos != 0m)
                {
                    decimal pesosAtingimentosTotal = pesosAtingimentos.Sum();
                    decimal resultadoFinal = decimal.Round(pesosAtingimentosTotal / pesos, 2);
                    return resultadoFinal;
                }
            }

            return 0m;
        }

        public UsuarioIndicadorLancamentosResultado ObterResultadosParaUsuario(long idProjetoEstruturaOrganizacional, long idUsuario, int mesInicial, int anoInicial, int mesFinal, int anoFinal)
        {
            var retorno = new UsuarioIndicadorLancamentosResultado { Indicadores = new List<IndicadorLancamentosResultado>() };
            var indicadores = projetoEstruturaOrganizacionalRepository.Filter(item => item.IdSuperior == idProjetoEstruturaOrganizacional && item.Tipo == TipoProjetoEstruturaOrganizacional.Indicador);
            if (indicadores?.Any() ?? false)
            {
                long idProjetoCorporativo = indicadores.First().IdProjeto;
                var listaIndicadores = new List<Task>();

                decimal valorPonderadoCorporativo = 0m;

                listaIndicadores.Add(Task.Run(async () =>
                {
                    valorPonderadoCorporativo = ObterValorPonderadoCorporativo(idProjetoCorporativo, mesInicial, anoInicial, mesFinal, anoFinal);
                    await Task.CompletedTask;
                }));

                foreach (var ind in indicadores)
                {
                    long idProjeto = ind.IdProjeto;
                    long idIndicador = ind.IdIndicador.Value;
                    listaIndicadores.Add(Task.Run(async () =>
                    {
                        var resultado = ObterResultadosPorIndicador(idProjeto, idIndicador, mesInicial, anoInicial, mesFinal, anoFinal);

                        if (environmentService.PossuiLimiteSuperiorAtingimento && resultado.ValorAtingimento > environmentService.ValorLimiteSuperiorAtingimento)
                        {
                            resultado.ValorAtingimento = environmentService.ValorLimiteSuperiorAtingimento;
                        }
                        else if (environmentService.PossuiLimiteInferiorAtingimento && resultado.ValorAtingimento < environmentService.ValorLimiteInferiorAtingimento)
                        {
                            resultado.ValorAtingimento = environmentService.ValorLimiteInferiorAtingimento;
                        }

                        retorno.Indicadores.Add(resultado);
                        await Task.CompletedTask;
                    }));
                }

                Task.WaitAll(listaIndicadores.ToArray());

                decimal somaPesos = 0m;
                decimal valorPonderadoIndividual = 0m;
                foreach (var indResultado in retorno.Indicadores)
                {
                    var pesoAtingimento = indResultado.Peso * indResultado.ValorAtingimento;
                    valorPonderadoIndividual += pesoAtingimento;
                    somaPesos += indResultado.Peso;
                }

                if (somaPesos != 0m)
                {
                    if (environmentService.PossuiLimiteSuperiorPonderadoIndividual && valorPonderadoIndividual > environmentService.ValorLimiteSuperiorPonderadoIndividual)
                    {
                        valorPonderadoIndividual = environmentService.ValorLimiteSuperiorPonderadoIndividual;
                    }
                    else if (environmentService.PossuiLimiteInferiorPonderadoIndividual && valorPonderadoIndividual < environmentService.ValorLimiteInferiorPonderadoIndividual)
                    {
                        valorPonderadoIndividual = environmentService.ValorLimiteInferiorPonderadoIndividual;
                    }

                    valorPonderadoIndividual = decimal.Round(valorPonderadoIndividual / somaPesos, 2);
                }

                retorno.ValorPonderadoIndividual = valorPonderadoIndividual;
                retorno.ValorPonderadoCorporativo = valorPonderadoCorporativo;

                var usuario = usuarioRepository.GetById(idUsuario, false);
                decimal percentualPesoIndividual = usuario.ValorPesoIndividual / 100m;
                decimal percentualPesoCorporativo = usuario.ValorPesoCorporativo / 100m;
                decimal valorPonderadoFinal = decimal.Round((percentualPesoIndividual * valorPonderadoIndividual) + (percentualPesoCorporativo * valorPonderadoCorporativo), 2);
                retorno.ValorPonderadoFinal = valorPonderadoFinal;
            }

            return retorno;
        }

        public IndicadorLancamentosEvolucaoMensal ObterResultadosPorIndicadorEvolucaoMensalSimples(long idProjeto, long idIndicador, int mesInicial, int anoInicial, int mesFinal, int anoFinal)
        {
            var retorno = new IndicadorLancamentosEvolucaoMensal { IndicadorEvolucaoMensalSimples = new List<IndicadorLancamentosEvolucaoMensalSimples>() };
            var dataInicial = new DateTime(anoInicial, mesInicial, 1);
            var dataFinal = Utils.ObterDataNoUltimoDiaDoMes(mesFinal, anoFinal);
            var listaResultados = new List<Tuple<string, IndicadorLancamentosResultado>>();
            var resultados = new List<Task>();

            while (dataInicial <= dataFinal)
            {
                int mesReferencia = dataInicial.Month;
                int anoReferencia = dataInicial.Year;
                resultados.Add(Task.Run(async () =>
                {
                    var valores = ObterResultadosPorIndicador(idProjeto, idIndicador, mesReferencia, anoReferencia, mesReferencia, anoReferencia);
                    string descricaoMesAbreviada = Utils.ObterAbreviacaoMes(mesReferencia);
                    var tupla = new Tuple<string, IndicadorLancamentosResultado>($"{descricaoMesAbreviada}/{anoReferencia}", valores);
                    listaResultados.Add(tupla);
                    await Task.CompletedTask;
                }));

                dataInicial = dataInicial.AddMonths(1);
            }

            Task.WaitAll(resultados.ToArray());

            if (listaResultados.Any())
            {
                foreach (var ret in listaResultados)
                {
                    retorno.IndicadorEvolucaoMensalSimples.Add(new IndicadorLancamentosEvolucaoMensalSimples
                    {
                        Ocorrencia = ret.Item1,
                        ValorAtingimento = ret.Item2.ValorAtingimento,
                        ValorMeta = ret.Item2.ValorMeta,
                        ValorRealizado = ret.Item2.ValorRealizado
                    });
                }

                var primeiroRetorno = listaResultados.First();
                retorno.NomeIndicador = primeiroRetorno.Item2.NomeIndicador;
                retorno.UnidadeMedida = primeiroRetorno.Item2.UnidadeMedida;
            }

            return retorno;
        }

        public (bool Exito, string Erros) GerarLancamentos(long idProjeto, byte[] dados)
        {
            if (dados is null)
            {
                throw new ArgumentNullException(nameof(dados));
            }

            var projeto = projetoRepository.GetById(idProjeto, false);
            var dataAtual = DateTime.UtcNow.Date;
            if (!projeto.Ativo || projeto.DataInicio.Date > dataAtual || projeto.DataTermino < dataAtual)
            {
                return (false, "O projeto selecionado deve estar ativo e dentro da vigência atual.");
            }

            var listaLancamentos = new List<ImportacaoLancamentos>();
            using var memoryStream = new MemoryStream(dados);
            using var streamReader = new StreamReader(memoryStream, encoding: Encoding.UTF8);
            int numeroLinha = 0;
            var builder = new StringBuilder();
            string linha = string.Empty;
            while ((linha = streamReader.ReadLine()) != null)
            {
                numeroLinha++;
                try
                {
                    string[] colunas = linha.Split(';');
                    var importacao = new ImportacaoLancamentos
                    {
                        Identificador = colunas[0],
                        Ano = int.Parse(colunas[1]),
                        Mes = int.Parse(colunas[2]),
                        ValorMeta = decimal.Parse(colunas[3]),
                        ValorRealizado = decimal.Parse(colunas[4])
                    };

                    if (importacao.Ano > 2050 || importacao.Ano < 2015)
                    {
                        builder.AppendLine($"Ano inválido! Número da linha: {numeroLinha}. Erro: 'Ano deve estar entre 2015 e 2050'.");
                        continue;
                    }

                    if (importacao.Mes > 12 || importacao.Mes < 1)
                    {
                        builder.AppendLine($"Mês inválido! Número da linha: {numeroLinha}. Erro: 'Mês deve estar entre 1 e 12'.");
                        continue;
                    }

                    var indicador = indicadorRepository.FirstOrDefault(item => item.Identificador == importacao.Identificador, loadDependencies: false);
                    if (indicador is null)
                    {
                        builder.AppendLine($"Identificador de indicador inválido! Número da linha: {numeroLinha}.");
                        continue;
                    }

                    importacao.IdIndicador = indicador.Id;
                    listaLancamentos.Add(importacao);
                }
                catch (Exception exc)
                {
                    builder.AppendLine($"Linha inválida! Número da linha: {numeroLinha}. Erro: '{exc.Message}'.");
                    continue;
                }
            }

            if (builder.Length > 0)
            {
                return (false, builder.ToString());
            }


            var listaLancamentosFinal = new List<Tuple<long, long, int, int, decimal, decimal>>();
            listaLancamentos.ForEach(item => listaLancamentosFinal.Add(new Tuple<long, long, int, int, decimal, decimal>(idProjeto, item.IdIndicador, item.Ano, item.Mes, item.ValorMeta, item.ValorRealizado)));

            try
            {
                indicadorLancamentoRepository.GerarLancamentos(listaLancamentosFinal);
                return (true, null);
            }
            catch (Exception exc)
            {
                return (false, $"Erro ao importar os lançamentos. '{exc.Message}'.");
            }
        }
    }

    internal class Lancamentos
    {
        public decimal ValorMeta { get; set; }

        public decimal ValorRealizado { get; set; }
    }

    internal struct ImportacaoLancamentos
    {
        public long IdIndicador { get; set; }

        public string Identificador { get; set; }

        public int Ano { get; set; }

        public int Mes { get; set; }

        public decimal ValorMeta { get; set; }

        public decimal ValorRealizado { get; set; }
    }
}
