using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPD.Backend.Api.Controllers
{
    [Route("unidade-medida")]
    public class UnidadeMedidaController : Controller<UnidadeMedidaModel, UnidadeMedida>
    {
        public UnidadeMedidaController(IUnidadeMedidaRepository unidadeMedidaRepository,
            IHttpContextAccessor httpContextAccessor) : base(unidadeMedidaRepository, httpContextAccessor) 
        {
        }
    }
}
