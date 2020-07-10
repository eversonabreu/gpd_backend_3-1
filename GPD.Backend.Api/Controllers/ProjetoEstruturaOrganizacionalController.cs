using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using GPD.Backend.Domain.Services.Implementations;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GPD.Backend.Api.Controllers
{
    [Route("projeto-estrutura-organizacional")]
    public class ProjetoEstruturaOrganizacionalController : Controller<ProjetoEstruturaOrganizacionalModel, ProjetoEstruturaOrganizacional>
    {
        private readonly IProjetoEstruturaOrganizacionalService projetoEstruturaOrganizacionalService;
        private readonly IIndicadorRepository indicadorRepository;
        private readonly IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository;

        public ProjetoEstruturaOrganizacionalController(IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository,
            IHttpContextAccessor httpContextAccessor,
            IProjetoEstruturaOrganizacionalService projetoEstruturaOrganizacionalService,
            IIndicadorRepository indicadorRepository) : base(projetoEstruturaOrganizacionalRepository, httpContextAccessor) 
        {
            this.projetoEstruturaOrganizacionalService = projetoEstruturaOrganizacionalService;
            this.indicadorRepository = indicadorRepository;
            this.projetoEstruturaOrganizacionalRepository = projetoEstruturaOrganizacionalRepository;
        }

        [Route("obter-arvore/{idProjeto:long}"), HttpGet]
        public IEnumerable<ProjetoEstruturaOrganizacionalArvore> ObterArvore(long idProjeto)
        {
            //System.Diagnostics.Debugger.Launch();
            return projetoEstruturaOrganizacionalService.ObterArvore(idProjeto);
        }

        [Route("obter-indicadores-corporativos/{idProjeto:long}"), HttpGet]
        public IEnumerable<IndicadoresCorporativos> ObterIndicadoresCorporativos(long idProjeto)
        {
            //System.Diagnostics.Debugger.Launch();
            return indicadorRepository.ObterIndicadoresCorporativos(idProjeto);
        }

        [Route("exclui-item/{id:long}/{idSuperior:long}/{idProjeto:long}"), HttpGet]
        public IEnumerable<ProjetoEstruturaOrganizacionalArvore> ExcluirItem(long id, long idSuperior, long idProjeto)
        {
            //System.Diagnostics.Debugger.Launch();
            projetoEstruturaOrganizacionalRepository.DeleteById(id);
            //return projetoEstruturaOrganizacionalService.ObterArvore(idProjeto, idSuperior);
            return new List<ProjetoEstruturaOrganizacionalArvore>();
        }

        [Route("avancar-nivel/{id:long}/{idSuperior:long}/{idProjeto:long}"), HttpGet]
        public IEnumerable<ProjetoEstruturaOrganizacionalArvore> AvancarNivel(long id, long idSuperior, long idProjeto)
        {
            projetoEstruturaOrganizacionalRepository.AvancarNivel(id, idSuperior);
            //return projetoEstruturaOrganizacionalService.ObterArvore(idProjeto, id);
            return new List<ProjetoEstruturaOrganizacionalArvore>();
        }

        [Route("retroceder-nivel/{id:long}/{idSuperior:long}/{idProjeto:long}"), HttpGet]
        public IEnumerable<ProjetoEstruturaOrganizacionalArvore> RetrocederNivel(long id, long idSuperior, long idProjeto)
        {
            //System.Diagnostics.Debugger.Launch();
            projetoEstruturaOrganizacionalRepository.RetrocederNivel(id, idSuperior);
            //return projetoEstruturaOrganizacionalService.ObterArvore(idProjeto, id);
            return new List<ProjetoEstruturaOrganizacionalArvore>();
        }

        [Route("adicionar-item"), HttpPost]
        public void AdicionarItem([FromBody] ArvoreAddUpdate arvore)
        {
            int ordem = 1;
            var ultimaOrdem = projetoEstruturaOrganizacionalRepository.Filter(item => item.IdSuperior == arvore.IdSuperior);
            if (ultimaOrdem.Any())
            {
                ordem = ultimaOrdem.Max(it => it.Ordem) + 1;
            }

            var projetoEstruturaOrganizacionalReferencia = new ProjetoEstruturaOrganizacional();

            if (arvore.Tipo == 3 || arvore.Tipo == 4 || arvore.Tipo == 5)
            {
                projetoEstruturaOrganizacionalReferencia.IdNivelOrganizacional = arvore.IdReferencia;
            }
            else if (arvore.Tipo == 6)
            {
                projetoEstruturaOrganizacionalReferencia.IdUsuario = arvore.IdReferencia;
            }
            else
            {
                projetoEstruturaOrganizacionalReferencia.IdIndicador = arvore.IdReferencia;
            }

            projetoEstruturaOrganizacionalReferencia.Ordem = (short) ordem;
            projetoEstruturaOrganizacionalReferencia.Tipo = (TipoProjetoEstruturaOrganizacional) arvore.Tipo;
            projetoEstruturaOrganizacionalReferencia.IdSuperior = arvore.IdSuperior;
            projetoEstruturaOrganizacionalReferencia.IdProjeto = arvore.IdProjeto;
            projetoEstruturaOrganizacionalRepository.Add(projetoEstruturaOrganizacionalReferencia);
        }

        [Route("alterar-item/{id:long}/{idReferencia:long}/{tipo:int}"), HttpGet]
        public int AlterarItem(long id, long idReferencia, int tipo)
        {
            var arvore = projetoEstruturaOrganizacionalRepository.GetById(id, false);

            if (tipo == 3 || tipo == 4 || tipo == 5)
            {
                arvore.IdNivelOrganizacional = idReferencia;
            }
            else if (tipo == 6)
            {
                arvore.IdUsuario = idReferencia;
            }
            else
            {
                arvore.IdIndicador = idReferencia;
            }

            projetoEstruturaOrganizacionalRepository.Update(arvore);
            return 1;
        }
    }

    public class ArvoreAddUpdate
    {
        public long? Id { get; set; }

        public long IdProjeto { get; set; }

        public int Tipo { get; set; }

        public long IdReferencia { get; set; }

        public long IdSuperior { get; set; }
    }
}
