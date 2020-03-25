using System;
using System.Collections.Generic;

namespace GPD.Commom.UserInformations
{
    public class User
    {
        public long Id { get; set; }

        public DateTime DateTimeLogin { get; set; }

        public bool Administrador { get; set; }

        public IList<UserFunction> UserFunctions { get; set; }

        public IList<UserProfile> UserProfiles { get; set; }
    }

    public struct UserFunction
    {
        public string Controlador { get; set; }

        public string Nome { get; set; }

        public bool PermiteInserir { get; set; }

        public bool PermiteEditar { get; set; }

        public bool PermiteExcluir { get; set; }
    }

    public struct UserProfile
    {
        public uint Codigo { get; set; }

        public string Nome { get; set; }
    }
}
