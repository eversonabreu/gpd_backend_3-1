using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPD.Backend.Domain.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioFuncionalidadeRepository usuarioFuncionalidadeRepository;
        private readonly IPerfilUsuarioRepository perfilUsuarioRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IEmailService emailService;

        public LoginService(IUsuarioFuncionalidadeRepository usuarioFuncionalidadeRepository,
                            IPerfilUsuarioRepository perfilUsuarioRepository,
                            IUsuarioRepository usuarioRepository,
                            IEmailService emailService)
        {
            this.usuarioFuncionalidadeRepository = usuarioFuncionalidadeRepository;
            this.perfilUsuarioRepository = perfilUsuarioRepository;
            this.usuarioRepository = usuarioRepository;
            this.emailService = emailService;
        }

        public IEnumerable<UsuarioFuncionalidade> ObterFuncionalides(long idUsuario) => usuarioFuncionalidadeRepository.Filter(item => item.IdUsuario == idUsuario, loadDependencies: true);

        public IEnumerable<PerfilUsuario> ObterPerfis(long idUsuario) => perfilUsuarioRepository.Filter(item => item.IdUsuario == idUsuario, loadDependencies: true);

        public Usuario ObterUsuario(string cpf) => usuarioRepository.FirstOrDefault(item => item.Cpf == cpf && item.Ativo, loadDependencies: false);

        public string GerarSenhaTemporaria()
        {
            string guid = Guid.NewGuid().ToString();
            string parteGuid = guid.Split('-').First();
            string senha = $"[*TEMP${parteGuid}$*]";
            return senha;
        }

        public void EnviarEmailSenha(string remetente, string destinatario, string nome, string senha)
        {
            string body = $"Prezado(a) {nome},\n    está a sua senha temporária de acesso ao sistema GPD:\n\n      {senha}\n\n\n\n***Altere sua senha após o primeiro acesso.";
            emailService.EnviarComCopiaOcultaRemetente("Acesso ao sistema GPD", remetente, destinatario, body);
        }
    }
}
