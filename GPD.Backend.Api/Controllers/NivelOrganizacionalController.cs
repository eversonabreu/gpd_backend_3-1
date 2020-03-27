using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPD.Backend.Api.Controllers
{
    [Route("nivel-organizacional")]
    public class NivelOrganizacionalController : Controller<NivelOrganizacionalModel, NivelOrganizacional>
    {
        public NivelOrganizacionalController(INivelOrganizacionalRepository nivelOrganizacionalRepository,
            IHttpContextAccessor httpContextAccessor) : base(nivelOrganizacionalRepository, httpContextAccessor) 
        {
        }
    }
}
