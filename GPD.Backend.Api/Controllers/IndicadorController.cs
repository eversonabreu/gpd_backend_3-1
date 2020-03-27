using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPD.Backend.Api.Controllers
{
    [Route("indicador")]
    public class IndicadorController : Controller<IndicadorModel, Indicador>
    {
        public IndicadorController(IIndicadorRepository indicadorRepository,
            IHttpContextAccessor httpContextAccessor) : base(indicadorRepository, httpContextAccessor) 
        {
        }
    }
}
