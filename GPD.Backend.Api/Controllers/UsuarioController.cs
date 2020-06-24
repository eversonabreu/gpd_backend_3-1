using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using GPD.Commom.Models;
using GPD.Commom.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GPD.Backend.Api.Controllers
{
    [Route("usuario")]
    public class UsuarioController : Controller<UsuarioModel, Usuario>
    {
        private readonly ILoginService loginService;
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            ILoginService loginService) : base(usuarioRepository, httpContextAccessor) 
        {
            this.loginService = loginService;
            this.usuarioRepository = usuarioRepository;
        }

        [Route("login-account"), HttpPost, AllowAnonymous]
        public string Login() => UserIdentity.GetToken(loginService);

        public override long Post([FromBody] UsuarioModel model)
        {
            model.Validate();
            model.Senha = Commom.Utils.Cryptography.Encrypt(loginService.GerarSenhaTemporaria());
            var retorno = base.Post(model);
			string remetente = usuarioRepository.GetById(User.Id, false).Email;
			Task.Run(() => 
			{
				string senha = Commom.Utils.Cryptography.Decrypt(model.Senha);
				loginService.EnviarEmailSenha(remetente, model.Email, model.Nome, senha);	
			});
            return retorno;
        }

        public override void Put([FromBody] UsuarioModel model)
        {
            model.Validate(validateId: true);
            model.Senha = usuarioRepository.GetById(model.Id!.Value, false).Senha;
            base.Put(model);
        }

        [Route("resetar-senha"), HttpPost]
        public void ResetarSenha(long idUsuario)
        {
            var usuario = usuarioRepository.GetById(idUsuario, false);
            usuario.Senha = Commom.Utils.Cryptography.Encrypt(loginService.GerarSenhaTemporaria());
            usuarioRepository.Update(usuario);
            string senha = Commom.Utils.Cryptography.Decrypt(usuario.Senha);
            string remetente = usuarioRepository.GetById(User.Id, false).Email;
            loginService.EnviarEmailSenha(remetente, usuario.Email, usuario.Nome, senha);
        }
    }
}
