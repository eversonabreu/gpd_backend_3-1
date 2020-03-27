using GPD.Backend.Domain.Attributes;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories;
using System;

namespace GPD.Backend.Domain.Entities
{
    public enum TipoAuditoria
    {
        Insercao = 1,
        Alteracao = 2,
        Exclusao = 3
    }

    public sealed class Auditoria : Entity
    {
        public string NomeTabela { get; set; }

        public long IdRegistro { get; set; }

        public DateTime DataRegistro { get; set; }

        public TipoAuditoria Tipo { get; set; }

        public long IdUsuario { get; set; }

        public string Objeto { get; set; }
    }
}
