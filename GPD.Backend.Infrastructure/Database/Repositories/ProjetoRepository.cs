using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Infrastructure.Database.Repositories.Base;
using System;

namespace GPD.Backend.Infrastructure.Database.Repositories
{
    public sealed class ProjetoRepository : BaseRepository<Projeto>, IProjetoRepository
    {
        private readonly IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository;

        public ProjetoRepository(IServiceProvider serviceProvider,
            IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository) : base(serviceProvider) 
        {
            this.projetoEstruturaOrganizacionalRepository = projetoEstruturaOrganizacionalRepository;
        }

        protected override void AfterAdd(Projeto entity)
        {
            projetoEstruturaOrganizacionalRepository.Add(new ProjetoEstruturaOrganizacional
            {
                IdProjeto = entity.Id,
                Tipo = TipoProjetoEstruturaOrganizacional.Projeto,
                Ordem = 1
            });
        }
    }
}
