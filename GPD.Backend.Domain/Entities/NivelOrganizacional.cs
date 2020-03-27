﻿using GPD.Backend.Domain.Entities.Base;

namespace GPD.Backend.Domain.Entities
{
    public enum TipoNivelOrganizacional
    {
        Cargo = 1,
        Departamento = 2,
        Grupo = 3
    }

    public sealed class NivelOrganizacional : Entity
    {
        public string Nome { get; set; }

        public TipoNivelOrganizacional Tipo { get; set; }

        public string Descricao { get; set; }
    }
}
