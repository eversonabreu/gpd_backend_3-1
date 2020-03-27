using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Infrastructure.Database.Repositories.Base;
using System;

namespace GPD.Backend.Infrastructure.Database.Repositories
{
    public sealed class NivelOrganizacionalRepository : BaseRepository<NivelOrganizacional>, INivelOrganizacionalRepository
    {
        public NivelOrganizacionalRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
