using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioFuncionalidadeRepository usuarioFuncionalidadeRepository;
        private readonly IPerfilUsuarioRepository perfilUsuarioRepository;
        private readonly IUsuarioRepository usuarioRepository;

        public LoginService(IUsuarioFuncionalidadeRepository usuarioFuncionalidadeRepository,
                            IPerfilUsuarioRepository perfilUsuarioRepository,
                            IUsuarioRepository usuarioRepository)
        {
            this.usuarioFuncionalidadeRepository = usuarioFuncionalidadeRepository;
            this.perfilUsuarioRepository = perfilUsuarioRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public IEnumerable<UsuarioFuncionalidade> ObterFuncionalides(long idUsuario) => usuarioFuncionalidadeRepository.Filter(item => item.IdUsuario == idUsuario, loadDependencies: true);

        public IEnumerable<PerfilUsuario> ObterPerfis(long idUsuario) => perfilUsuarioRepository.Filter(item => item.IdUsuario == idUsuario, loadDependencies: true);

        public Usuario ObterUsuario(string login) => usuarioRepository.FirstOrDefault(item => item.Login.Trim().ToUpper() == login.Trim().ToUpper() && item.Ativo, loadDependencies: false);
    }
}
