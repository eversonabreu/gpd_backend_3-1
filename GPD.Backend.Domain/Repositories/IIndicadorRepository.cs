using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories.Base;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Repositories
{
    public interface IIndicadorRepository : IBaseRepository<Indicador>
    {
        IEnumerable<IndicadoresCorporativos> ObterIndicadoresCorporativos(long idProjeto);
    }

    public class IndicadoresCorporativos
    {
        public long Id { get; set; }

        public long? IdProjetoEstruturaOrganizacional { get; set; }

        public string Identificador { get; set; }

        public string Nome { get; set; }

        public int TipoCalculo { get; set; }

        public bool Vinculado { get; set; }
    }
}
