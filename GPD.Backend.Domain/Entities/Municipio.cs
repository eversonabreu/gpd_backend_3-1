using GPD.Backend.Domain.Entities.Base;

namespace GPD.Backend.Domain.Entities
{
    public sealed class Municipio : Entity
    {
        public int CodigoMunicipioIbge { get; set; }

        public string Nome { get; set; }

        public long IdEstado { get; set; }

        public Estado Estado { get; set; }
    }
}
