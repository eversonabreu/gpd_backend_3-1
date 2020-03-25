using GPD.Backend.Api.Controllers.Base;
using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Domain.Services.Contracts;
using GPD.Commom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPD.Backend.Api.Controllers
{
    [Route("usuario")]
    public class UsuarioController : Controller<UsuarioModel, Usuario>
    {
        private readonly ILoginService loginService;

        public UsuarioController(IUsuarioRepository usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            ILoginService loginService) : base(usuarioRepository, httpContextAccessor) 
        {
            this.loginService = loginService;
        }

        [Route("login-account"), HttpPost, AllowAnonymous]
        public void Login() => UserIdentity.GetToken(loginService);
    }
}
