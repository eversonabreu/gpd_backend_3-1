using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPD.Backend.Api.Controllers
{
    [Route("projeto")]
    public class ProjetoController : Controller<ProjetoModel, Projeto>
    {
        public ProjetoController(IProjetoRepository projetoRepository,
            IHttpContextAccessor httpContextAccessor) : base(projetoRepository, httpContextAccessor) 
        {
        }
    }
}
