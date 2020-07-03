using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using GPD.Backend.Domain.Services.Implementations;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GPD.Backend.Api.Controllers
{
    [Route("projeto-estrutura-organizacional")]
    public class ProjetoEstruturaOrganizacionalController : Controller<ProjetoEstruturaOrganizacionalModel, ProjetoEstruturaOrganizacional>
    {
        private readonly IProjetoEstruturaOrganizacionalService projetoEstruturaOrganizacionalService;

        public ProjetoEstruturaOrganizacionalController(IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository,
            IHttpContextAccessor httpContextAccessor,
            IProjetoEstruturaOrganizacionalService projetoEstruturaOrganizacionalService) : base(projetoEstruturaOrganizacionalRepository, httpContextAccessor) 
        {
            this.projetoEstruturaOrganizacionalService = projetoEstruturaOrganizacionalService;
        }

        [Route("obter-arvore/{idProjeto:long}"), HttpGet]
        public IEnumerable<ProjetoEstruturaOrganizacionalArvore> ObterArvore(long idProjeto)
        {
            //System.Diagnostics.Debugger.Launch();
            return projetoEstruturaOrganizacionalService.ObterArvore(idProjeto);
        }
    }
}
