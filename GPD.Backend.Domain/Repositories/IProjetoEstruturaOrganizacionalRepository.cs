using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories.Base;

namespace GPD.Backend.Domain.Repositories
{
    public interface IProjetoEstruturaOrganizacionalRepository : IBaseRepository<ProjetoEstruturaOrganizacional>
    {
        void RetrocederNivel(long id, long idSuperior);

        void AvancarNivel(long id, long idSuperior);
    }
}
