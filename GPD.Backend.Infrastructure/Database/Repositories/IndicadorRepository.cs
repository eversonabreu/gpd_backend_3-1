using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Exceptions;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Infrastructure.Database.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPD.Backend.Infrastructure.Database.Repositories
{
    public sealed class IndicadorRepository : BaseRepository<Indicador>, IIndicadorRepository
    {
        private readonly IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository;

        public IndicadorRepository(IServiceProvider serviceProvider,
            IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository) : base(serviceProvider) 
        {
            this.projetoEstruturaOrganizacionalRepository = projetoEstruturaOrganizacionalRepository;
        }

        protected override void BeforeUpdate(Indicador oldValue, Indicador newValue)
        {
            newValue.Identificador = oldValue.Identificador;

            if (oldValue.TipoCalculo == TipoCalculo.NaoCalculado && newValue.TipoCalculo != TipoCalculo.NaoCalculado)
            {
                string termoPesquisa = $"[{newValue.Identificador}]";
                if (FirstOrDefault(item => item.TipoCalculo != TipoCalculo.NaoCalculado && item.Formula.Contains(termoPesquisa), loadDependencies: false) != null)
                {
                    throw new BusinessException("Não é possível alterar o tipo de cálculo deste indicador porque ele é usado como indicador base em outro(s) indicador(es).");
                }
            }
        }

        protected override void AfterUpdate(Indicador oldValue, Indicador newValue)
        {
            if (oldValue.Corporativo && !newValue.Corporativo)
            {
                var lista = projetoEstruturaOrganizacionalRepository.Filter(item => item.Tipo == TipoProjetoEstruturaOrganizacional.Corporativo && item.IdIndicador == newValue.Id);
                var listaIds = new List<long>();
                lista?.ToList()?.ForEach(item => listaIds.Add(item.Id));
                projetoEstruturaOrganizacionalRepository.DeleteMany(listaIds);
            }
        }
    }
}
