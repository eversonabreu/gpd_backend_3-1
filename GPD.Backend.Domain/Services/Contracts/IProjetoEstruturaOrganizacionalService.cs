using GPD.Backend.Domain.Services.Implementations;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Services.Contracts
{
    public interface IProjetoEstruturaOrganizacionalService
    {
        IList<ProjetoEstruturaOrganizacionalArvore> ObterArvore(long idProjeto);
    }
}
