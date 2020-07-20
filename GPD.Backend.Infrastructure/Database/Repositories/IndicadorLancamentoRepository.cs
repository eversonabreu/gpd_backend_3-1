using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Infrastructure.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPD.Backend.Infrastructure.Database.Repositories
{
    public sealed class IndicadorLancamentoRepository : BaseRepository<IndicadorLancamento>, IIndicadorLancamentoRepository
    {
        private readonly new DatabaseContext databaseContext;

        public IndicadorLancamentoRepository(IServiceProvider serviceProvider,
            DatabaseContext databaseContext) : base(serviceProvider) 
        {
            this.databaseContext = databaseContext;
        }

        public ImpotacaoLancamentosDto GerarLancamentos(IEnumerable<ImpotacaoLancamentos> lancamentos)
        {
            var builderErros = new System.Text.StringBuilder();
            var builderStatus = new System.Text.StringBuilder();
            var comandos = new List<Tuple<string, decimal, decimal, long?>>();
            int numeroLinha = 0;
            foreach (var item in lancamentos)
            {
                numeroLinha++;
                var erros = new System.Text.StringBuilder();
                var projeto = databaseContext.Projeto.FirstOrDefault(it => it.Id == item.IdProjeto);

                if (projeto is null)
                {
                    erros.Append(" O ID de projeto não existe. ");
                }

                var dataAtual = DateTime.UtcNow.Date;
                if (!projeto.Ativo || projeto.DataInicio.Date > dataAtual || projeto.DataTermino < dataAtual)
                {
                    erros.Append(" O projeto deve estar ativo e dentro da vigência atual. ");
                }

                var indicador = databaseContext.Indicador.FirstOrDefault(it => it.Identificador == item.Identificador);
                if (indicador is null)
                {
                    erros.Append(" O identificador não existe. ");
                }

                if (erros.Length > 1)
                {
                    builderErros.AppendLine($"A linha {numeroLinha} está com problema(s): '{erros.ToString()}'.");
                }
                else if (builderErros.Length == 0)
                {
                    var lancamento = databaseContext.IndicadorLancamentos.FirstOrDefault(it => it.IdProjeto == item.IdProjeto && it.IdIndicador == indicador.Id && it.Ano == item.Ano && it.Mes == item.Mes);
                    decimal valorMeta = 0.00m;
                    decimal valorRealizado = 0.00m;
                    string status = "Valor da meta e valor do realizado foram zerados porque o tipo de cálculo do indicador está definido como 'Meta e realizado calculados'.";

                    if (indicador.TipoCalculo == TipoCalculo.NaoCalculado)
                    {
                        valorMeta = item.ValorMeta;
                        valorRealizado = item.ValorRealizado;
                        status = string.Empty;
                    }
                    else if (indicador.TipoCalculo == TipoCalculo.SomenteMeta)
                    {
                        valorRealizado = item.ValorRealizado;
                        status = "Valor da meta foi zerado porque o tipo de cálculo do indicador está definido como 'Meta calculada'.";
                    }
                    else if (indicador.TipoCalculo == TipoCalculo.SomenteRealizado)
                    {
                        valorMeta = item.ValorMeta;
                        status = "Valor do realizado foi zerado porque o tipo de cálculo do indicador está definido como 'Realizado calculado'.";
                    }

                    if (lancamento is null)
                    {
                        comandos.Add(new Tuple<string, decimal, decimal, long?>($"insert into IndicadorLancamento (IdProjeto, IdIndicador, Ano, Mes, ValorMeta, ValorRealizado) values ({item.IdProjeto}, {indicador.Id}, {item.Ano}, {item.Mes}, @p1, @p2);", valorMeta, valorRealizado, null));
                        builderStatus.Append($"<p style='color:green'>A linha {numeroLinha} foi <b>inserida</b> com sucesso. (<i style='color:orange'>{status}</i>)</p><br/>").Replace("(<i style='color:orange'></i>)", string.Empty);
                    }
                    else
                    {
                        comandos.Add(new Tuple<string, decimal, decimal, long?>("update IndicadorLancamento set ValorMeta = @p1, ValorRealizado = @p2 where Id = @p3;", valorMeta, valorRealizado, lancamento.Id));
                        builderStatus.Append($"<p style='color:blue'>A linha {numeroLinha} foi <b>atualizada</b> com sucesso. (<i style='color:orange'>{status}</i>)</p><br/>").Replace("(<i style='color:orange'></i>)", string.Empty);
                    }
                }
            }

            if (builderErros.Length > 1)
            {
                return new ImpotacaoLancamentosDto
                {
                    Sucesso = false,
                    Mensagem = builderErros.ToString().Replace(Environment.NewLine, "<br/>")
                };
            }

            string erro = null;
            using var context = new Microsoft.Data.SqlClient.SqlConnection(databaseContext.Database.GetDbConnection().ConnectionString);
            context.Open();
            using var transaction = context.BeginTransaction();
            foreach (var item in comandos)
            {
                using var cmd = new Microsoft.Data.SqlClient.SqlCommand(item.Item1, context, transaction);
                try
                {
                    cmd.Parameters.AddWithValue("p1", item.Item2);
                    cmd.Parameters.AddWithValue("p2", item.Item3);

                    if (item.Item4.HasValue)
                    {
                        cmd.Parameters.AddWithValue("p3", item.Item4.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    erro = exc.Message;
                    break;
                }
            }

            if (erro is null)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            context.Close();

            if (erro != null)
            {
                return new ImpotacaoLancamentosDto
                {
                    Sucesso = false,
                    Mensagem = erro
                };
            }

            return new ImpotacaoLancamentosDto
            {
                Sucesso = true,
                Mensagem = builderStatus.ToString()
            };
        }
    }
}
