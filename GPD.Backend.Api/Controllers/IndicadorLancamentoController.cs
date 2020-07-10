using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GPD.Backend.Api.Controllers
{
    [Route("indicador-lancamento")]
    public class IndicadorLancamentoController : Controller<IndicadorLancamentoModel, IndicadorLancamento>
    {
        private readonly IIndicadorLancamentoRepository indicadorLancamentoRepository;

        public IndicadorLancamentoController(IIndicadorLancamentoRepository indicadorLancamentoRepository,
            IHttpContextAccessor httpContextAccessor) : base(indicadorLancamentoRepository, httpContextAccessor) 
        {
            this.indicadorLancamentoRepository = indicadorLancamentoRepository;
        }

        [Route("gerar-lancamentos"), HttpPost]
        public ImpotacaoLancamentosDto GerarLancamentos([FromBody] IEnumerable<ImpotacaoLancamentos> lancamentos) => indicadorLancamentoRepository.GerarLancamentos(lancamentos);
    }
}
