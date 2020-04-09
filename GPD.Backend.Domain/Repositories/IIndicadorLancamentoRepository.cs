using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories.Base;
using System;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Repositories
{
    public interface IIndicadorLancamentoRepository : IBaseRepository<IndicadorLancamento>
    {
        void GerarLancamentos(IList<Tuple<long, long, int, int, decimal, decimal>> dados);
    }
}
