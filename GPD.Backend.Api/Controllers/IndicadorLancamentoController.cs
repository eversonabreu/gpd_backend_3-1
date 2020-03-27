using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPD.Backend.Api.Controllers
{
    [Route("indicador-lancamento")]
    public class IndicadorLancamentoController : Controller<IndicadorLancamentoModel, IndicadorLancamento>
    {
        public IndicadorLancamentoController(IIndicadorLancamentoRepository indicadorLancamentoRepository,
            IHttpContextAccessor httpContextAccessor) : base(indicadorLancamentoRepository, httpContextAccessor) 
        {
        }
    }
}
