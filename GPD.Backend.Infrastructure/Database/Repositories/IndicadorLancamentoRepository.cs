using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Infrastructure.Database.Repositories.Base;
using System;

namespace GPD.Backend.Infrastructure.Database.Repositories
{
    public sealed class IndicadorLancamentoRepository : BaseRepository<IndicadorLancamento>, IIndicadorLancamentoRepository
    {
        public IndicadorLancamentoRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
