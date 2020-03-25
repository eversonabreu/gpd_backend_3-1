using GPD.Backend.Domain.Attributes;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories;

namespace GPD.Backend.Domain.Entities
{
    public sealed class Municipio : Entity
    {
        public int CodigoMunicipioIbge { get; set; }

        public string Nome { get; set; }

        public long IdEstado { get; set; }

        [LoadEntity(NameForeignKey = nameof(IdEstado), TypeRepository = typeof(IEstadoRepository))]
        public Estado Estado { get; set; }
    }
}
