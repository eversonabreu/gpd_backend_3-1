using GpdFrontEnd.Infraestructure;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GpdFrontEnd.Services.System
{
    public class HttpService : ComponentBase
    {
        private readonly HttpClient httpClient;
        private readonly Session session;
        private readonly string controllerName;

        public HttpService(HttpClient httpClient, Session session, string controllerName)
        {
            this.httpClient = httpClient;
            this.session = session;
            this.controllerName = controllerName;
        }

        public async Task<HttpClient> GetHttpAsync()
        {
            string token = await session.GetTokenAsync();
            var http = new HttpClient();
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("authorization", $"Bearer {token}");
            http.BaseAddress = new Uri($"{httpClient.BaseAddress.AbsoluteUri}{controllerName}");
            return http;
        }

        public async Task GetAsync<TResult>(Action<TResult> success, Action<Exception> error, Action complete = null)
        {
            await GetAsync<TResult>(string.Empty, success, error, complete);
        }

        public async Task GetAsync<TResult>(string uri, Action<TResult> success, Action<Exception> error, Action complete = null)
        {
            TResult result = default;
            Exception errorRequest = null;
            Exception errorSuccess = null;
            HttpResponseMessage httpResponseMessage = null;

            using (var http = await GetHttpAsync())
            {
                try
                {
                    httpResponseMessage = await http.GetAsync(uri);
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string resultValue = await httpResponseMessage.Content.ReadAsStringAsync();
                        if (typeof(TResult) == typeof(string))
                        {
                            result = (TResult)Convert.ChangeType(resultValue, typeof(TResult));
                        }
                        else
                        {
                            result = JsonSerializer.Deserialize<TResult>(resultValue, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                    }
                    else
                    {
                        string message = await httpResponseMessage.Content.ReadAsStringAsync();
                        throw new Exception(message);
                    }
                }
                catch (Exception exc)
                {
                    string message = exc.Message;
                    if (httpResponseMessage != null)
                    {
                        string reasonPhrase = httpResponseMessage.ReasonPhrase;
                        int statusCode = (int)httpResponseMessage.StatusCode;
                        errorRequest = new Exception($"ReasonPhrase: '{reasonPhrase}'.  StatusCode: '{statusCode}'. Message: '{message}'.");
                    }
                    else
                    {
                        errorRequest = new Exception($"Message: '{message}'.");
                    }
                }
            }

            if (errorRequest is null)
            {
                try
                {
                    success(result);
                }
                catch (Exception exSuccess)
                {
                    errorSuccess = exSuccess;
                }
            }
            else
            {
                try
                {
                    error(errorRequest);
                    errorRequest = null;
                }
                catch (Exception excError)
                {
                    errorRequest = excError;
                }
            }

            try
            {
                complete?.Invoke();
            }
            finally
            {
                StateHasChanged();
            }

            if (errorSuccess != null)
            {
                RenderAfterError();
                throw errorSuccess;
            }
            else if (errorRequest != null)
            {
                RenderAfterError();
                throw errorRequest;
            }
        }

        private void RenderAfterError()
        {
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                StateHasChanged();
            });
        }

    }
}
