using System.Net.Http;
using GpdFrontEnd.Infraestructure;
using GpdFrontEnd.Services.System;

namespace GpdFrontEnd.Services
{
    public class UsuarioService : HttpService
    {
        public UsuarioService(HttpClient httpClient, Session session) : base (httpClient, session, "usuario") {}

        
    }
}