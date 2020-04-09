using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPD.Backend.Api.Controllers
{
    [Route("indicador-lancamento")]
    public class IndicadorLancamentoController : Controller<IndicadorLancamentoModel, IndicadorLancamento>
    {
        private readonly IIndicadorLancamentosService indicadorLancamentosService;

        public IndicadorLancamentoController(IIndicadorLancamentoRepository indicadorLancamentoRepository,
            IHttpContextAccessor httpContextAccessor,
            IIndicadorLancamentosService indicadorLancamentosService) : base(indicadorLancamentoRepository, httpContextAccessor) 
        {
            this.indicadorLancamentosService = indicadorLancamentosService;
        }

        [Route("gerar-lancamentos"), HttpPost]
        public (bool Exito, string Erros) GerarLancamentos(long idProjeto, byte[] dados) => indicadorLancamentosService.GerarLancamentos(idProjeto, dados);
    }
}
