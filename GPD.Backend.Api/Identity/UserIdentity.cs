using GPD.Backend.Domain.Exceptions;
using GPD.Backend.Domain.Services.Contracts;
using GPD.Commom.UserInformations;
using GPD.Commom.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPD.Backend.Api.Identity
{
    public class UserIdentity
    {
        public enum ActionPermission
        {
            Post,
            Put,
            Delete
        }

        private readonly HttpContext httpContext;
        private readonly string controllerName;

        public UserIdentity(HttpContext httpContext, string controllerName)
        {
            this.httpContext = httpContext;
            this.controllerName = controllerName;
        }

        public User GetUser()
        {
            if (httpContext.Request.Path.Value.Contains("login-account"))
            {
                return null!;
            }

            try
            {
                const int limitHoursToken = 3;
                string bearer = httpContext.Request.Headers["Authorization"];
                bearer = bearer.Replace("Bearer ", string.Empty);
                string jsonUser = bearer.Decrypt();
                var result = System.Text.Json.JsonSerializer.Deserialize<User>(jsonUser);

                if (result.DateTimeLogin.AddHours(limitHoursToken) > DateTime.UtcNow)
                {
                    throw new ExpirationTokenException("401 - Token expirado! Faça login novamente.");
                }

                if (result.Administrador)
                {
                    return result;
                }

                foreach (var item in result.UserFunctions)
                {
                    if (item.Controlador == controllerName)
                    {
                        return result;
                    }
                }

                throw new Exception();
            }
            catch (ExpirationTokenException ete)
            {
                throw ete;
            }
            catch (Exception)
            {
                throw new Exception("401 - Sem autorização de acesso.");
            }
        }

        public string GetToken(ILoginService loginService)
        {
            try
            {
                string credentials = httpContext.Request.Headers["Authorization"];
                string json = credentials.Decrypt();
                var jsonValue = System.Text.Json.JsonSerializer.Deserialize<Login>(json);
                var user = loginService.ObterUsuario(jsonValue.LoginValue);
                if (user.Senha.Decrypt() != jsonValue.Password)
                {
                    throw new Exception();
                }

                var jsonUser = new User
                {
                    Id = user.Id,
                    Administrador = user.Administrador,
                    DateTimeLogin = DateTime.UtcNow,
                    UserFunctions = new List<UserFunction>(),
                    UserProfiles = new List<UserProfile>()
                };

                var functions = loginService.ObterFuncionalides(user.Id);
                if (functions?.Any() ?? false)
                {
                    foreach (var item in functions)
                    {
                        jsonUser.UserFunctions.Add(new UserFunction
                        {
                            Controlador = item.Funcionalidade.Controlador,
                            Nome = item.Funcionalidade.Nome,
                            PermiteInserir = item.PermiteInserir,
                            PermiteEditar = item.PermiteEditar,
                            PermiteExcluir = item.PermiteExcluir
                        });
                    }
                }

                var profiles = loginService.ObterPerfis(user.Id);
                if (profiles?.Any() ?? false)
                {
                    foreach (var item in profiles)
                    {
                        jsonUser.UserProfiles.Add(new UserProfile
                        {
                            Codigo = item.Perfil.Codigo,
                            Nome = item.Perfil.Nome
                        });
                    }
                }

                string jsonUserSerialize = System.Text.Json.JsonSerializer.Serialize(jsonUser);
                string result = jsonUserSerialize.Encrypt();
                return result;
            }
            catch
            {
                throw new Exception("401 - Usuário ou senha inválidos.");
            }
        }

        public void VerifyPermission(ActionPermission actionPermission, User user)
        {
            try
            {
                if (user.Administrador) return;

                var function = user.UserFunctions.First(item => item.Controlador == controllerName);
                switch (actionPermission)
                {
                    case ActionPermission.Post:
                        {
                            if (!function.PermiteInserir)
                            {
                                throw new Exception();
                            }

                            break;
                        }
                    case ActionPermission.Put:
                        {
                            if (!function.PermiteEditar)
                            {
                                throw new Exception();
                            }

                            break;
                        }
                    case ActionPermission.Delete:
                        {
                            if (!function.PermiteExcluir)
                            {
                                throw new Exception();
                            }

                            break;
                        }
                    default: throw new Exception();
                }
            }
            catch
            {
                throw new Exception("403 - Usuário não possui permissão para executar esta ação.");
            }
        }
    }
}
