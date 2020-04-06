using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPD.Backend.Api.Controllers
{
    [Route("projeto-estrutura-organizacional")]
    public class ProjetoEstruturaOrganizacionalController : Controller<ProjetoEstruturaOrganizacionalModel, ProjetoEstruturaOrganizacional>
    {
        public ProjetoEstruturaOrganizacionalController(IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository,
            IHttpContextAccessor httpContextAccessor) : base(projetoEstruturaOrganizacionalRepository, httpContextAccessor) 
        {
        }
    }
}
