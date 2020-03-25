using GPD.Backend.Domain.Entities;
using System.Collections.Generic;

namespace GPD.Backend.Domain.Services.Contracts
{
    public interface ILoginService
    {
        IEnumerable<UsuarioFuncionalidade> ObterFuncionalides(long idUsuario);

        IEnumerable<PerfilUsuario> ObterPerfis(long idUsuario);

        Usuario ObterUsuario(string login);
    }
}
