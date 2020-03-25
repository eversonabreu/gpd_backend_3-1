using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Infrastructure.Database.Repositories.Base;
using System;

namespace GPD.Backend.Infrastructure.Database.Repositories
{
    public sealed class EstadoRepository : BaseRepository<Estado>, IEstadoRepository
    {
        public EstadoRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
