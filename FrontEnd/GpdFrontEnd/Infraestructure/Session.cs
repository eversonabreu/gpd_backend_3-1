using Blazored.SessionStorage;
using GPD.Commom.UserInformations;
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

        public async void SetTokenAsync(string token) => await sessionStorage.SetItemAsync("V_USER", token);

        public async Task<string> GetTokenAsync()
        {
            string result = await sessionStorage.GetItemAsync<string>("V_USER");
            if (result is null)
            {
                navigation.NavigateTo("/login");
            }

            return result;
        }
    
        public async Task<User> GetUserAsync(string page = null)
        {
            string dataUser = await sessionStorage.GetItemAsync<string>("V_USER");

            if (dataUser is null)
            {
                navigation.NavigateTo("/login");
                return null;
            }

            string jsonUser = GPD.Commom.Utils.Cryptography.Decrypt(dataUser);
            User user = System.Text.Json.JsonSerializer.Deserialize<User>(jsonUser);

            if (page != null)
            {
                // validar o acesso a página...

                navigation.NavigateTo("/sem");
                return null;
            }

            return user;
        }

        public async Task LogoutAsync()
        {
            await sessionStorage.SetItemAsync("V_USER", null);
            navigation.NavigateTo("/login", true);
        }
    }
}
