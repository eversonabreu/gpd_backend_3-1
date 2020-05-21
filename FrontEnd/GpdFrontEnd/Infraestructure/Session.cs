using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GpdFrontEnd.Infraestructure
{
    public class Session
    {
        private readonly ISessionStorageService sessionStorage;
        private readonly NavigationManager navigation;

        public Session(ISessionStorageService sessionStorage,
            NavigationManager navigation)
        {
            this.sessionStorage = sessionStorage;
            this.navigation = navigation;
        }

        public async Task<string> GetUserAsync(string page = null)
        {
            string dataUser = await sessionStorage.GetItemAsync<string>("V_USER");

            if (dataUser is null)
            {
                navigation.NavigateTo("/login");
                return null;
            }

            if (page != null)
            {
                // validar o acesso a página...

                navigation.NavigateTo("/sem");
                return null;
            }

            return dataUser;
        }

        public async Task LogoutAsync()
        {
            await sessionStorage.SetItemAsync("V_USER", null);
            navigation.NavigateTo("/login", true);
        }
    }
}
