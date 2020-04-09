using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Infrastructure.Database.Repositories.Base;
using System;
using System.Collections.Generic;

namespace GPD.Backend.Infrastructure.Database.Repositories
{
    public sealed class IndicadorLancamentoRepository : BaseRepository<IndicadorLancamento>, IIndicadorLancamentoRepository
    {
        private readonly DatabaseContext databaseContext;

        public IndicadorLancamentoRepository(IServiceProvider serviceProvider,
            DatabaseContext databaseContext) : base(serviceProvider) 
        {
            this.databaseContext = databaseContext;
        }

        public void GerarLancamentos(IList<Tuple<long, long, int, int, decimal, decimal>> dados)
        {
            using var context = DatabaseContext.CreateContext(databaseContext.Database);
            using var transaction = context.Database.BeginTransaction();
            foreach (var item in dados)
            {
                var lancamento = FirstOrDefault(it => it.IdProjeto == item.Item1 && it.IdIndicador == item.Item2 && it.Ano == item.Item3 && it.Mes == item.Item4, loadDependencies: false);
                if (lancamento is null)
                {
                    context.IndicadorLancamentos.Add(new IndicadorLancamento
                                                    {
                                                        IdProjeto = item.Item1,
                                                        IdIndicador = item.Item2,
                                                        Ano = item.Item3,
                                                        Mes = item.Item4,
                                                        ValorMeta = item.Item5,
                                                        ValorRealizado = item.Item6
                                                    });
                }
                else
                {
                    lancamento.ValorMeta = item.Item5;
                    lancamento.ValorRealizado = item.Item6;
                    context.IndicadorLancamentos.Update(lancamento);
                }

                context.SaveChanges();
            }
            transaction.Commit();
        }
    }
}
