using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace GPD.Backend.Domain.Services.Implementations
{
    public class ProjetoEstruturaOrganizacionalService : IProjetoEstruturaOrganizacionalService
    {
        private readonly IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository;
        private readonly INivelOrganizacionalRepository nivelOrganizacionalRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IIndicadorRepository indicadorRepository;
        private readonly IProjetoRepository projetoRepository;

        public ProjetoEstruturaOrganizacionalService(IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository,
            IProjetoRepository projetoRepository,
            IIndicadorRepository indicadorRepository,
            IUsuarioRepository usuarioRepository,
            INivelOrganizacionalRepository nivelOrganizacionalRepository)
        {
            this.projetoEstruturaOrganizacionalRepository = projetoEstruturaOrganizacionalRepository;
            this.projetoRepository = projetoRepository;
            this.indicadorRepository = indicadorRepository;
            this.nivelOrganizacionalRepository = nivelOrganizacionalRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public IList<ProjetoEstruturaOrganizacionalArvore> ObterArvore(long idProjeto)
        {
            var arvore = projetoEstruturaOrganizacionalRepository.FirstOrDefault(item => item.IdProjeto == idProjeto && item.Tipo == Entities.TipoProjetoEstruturaOrganizacional.Projeto, loadDependencies: false);
            var itens = new List<ProjetoEstruturaOrganizacionalArvore>();
            LoadArvoreByDatabase(new ProjetoEstruturaOrganizacionalArvore
            {
                Id = arvore.Id,
                IdProjeto = idProjeto,
                Tipo = (int)arvore.Tipo,
                Descricao = projetoRepository.GetById(idProjeto, false).Nome,
                Ordem = arvore.Ordem
            }, itens);

            var result = new List<ProjetoEstruturaOrganizacionalArvore>();
            var resultItem = itens.First(arv => arv.Id == arvore.Id);
            result.Add(resultItem);
            LoadArvoreByMemory(resultItem, itens);

            return result;
        }

        private void LoadArvoreByMemory(ProjetoEstruturaOrganizacionalArvore arvore, IList<ProjetoEstruturaOrganizacionalArvore> listaArvore)
        {
            var filhos = listaArvore.Where(item => item.IdSuperior == arvore.Id)?.ToList();
            if (filhos?.Any() ?? false)
            {
                arvore.Filhos = filhos.OrderBy(item => item.Ordem).ToList();
                foreach (var arv in filhos)
                {
                    LoadArvoreByMemory(arv, listaArvore);
                }
            }
        }

        private void LoadArvoreByDatabase(ProjetoEstruturaOrganizacionalArvore arvore, IList<ProjetoEstruturaOrganizacionalArvore> listaArvore)
        {
            var itens = projetoEstruturaOrganizacionalRepository.Filter(item => item.IdSuperior == arvore.Id && item.Tipo != Entities.TipoProjetoEstruturaOrganizacional.Corporativo);
            if (itens?.Any() ?? false)
            {
                foreach (var arv in itens)
                {
                    string descricao = null;
                    switch (arv.Tipo)
                    {
                        case Entities.TipoProjetoEstruturaOrganizacional.Grupo:
                        case Entities.TipoProjetoEstruturaOrganizacional.Departamento:
                        case Entities.TipoProjetoEstruturaOrganizacional.Cargo: descricao = nivelOrganizacionalRepository.GetById(arv.IdNivelOrganizacional.Value, false).Nome; break;

                        case Entities.TipoProjetoEstruturaOrganizacional.Indicador: descricao = indicadorRepository.GetById(arv.IdIndicador.Value, false).Identificador;  break;

                        case Entities.TipoProjetoEstruturaOrganizacional.Usuario: descricao = usuarioRepository.GetById(arv.IdUsuario.Value, false).Nome; break;
                    }

                    LoadArvoreByDatabase(new ProjetoEstruturaOrganizacionalArvore 
                    { 
                        Id = arv.Id,
                        IdProjeto = arv.IdProjeto,
                        Tipo = (int) arv.Tipo,
                        IdSuperior = arv.IdSuperior,
                        IdIndicador = arv.IdIndicador,
                        IdNivelOrganizacional = arv.IdNivelOrganizacional,
                        IdUsuario = arv.IdUsuario,
                        Descricao = descricao,
                        Ordem = arv.Ordem
                    }, listaArvore);
                }
            }

            listaArvore.Add(arvore);
        }
    }

    public class ProjetoEstruturaOrganizacionalArvore
    {
        public long Id { get; set; }

        public string Descricao { get; set; }

        public bool Expanded { get; set; }

        public long? IdSuperior { get; set; }

        public long? IdNivelOrganizacional { get; set; }

        public long? IdIndicador { get; set; }

        public long? IdUsuario { get; set; }

        public long IdProjeto { get; set; }

        public int Tipo { get; set; }

        public short Ordem { get; set; }

        public IList<ProjetoEstruturaOrganizacionalArvore> Filhos { get; set; }
    }
}
