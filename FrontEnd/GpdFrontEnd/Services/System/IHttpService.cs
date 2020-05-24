using System;
using System.Threading.Tasks;

namespace GpdFrontEnd.Services.System
{
    public interface IHttpService
    {
        Task GetAsync<TResult>(string controller, Action<TResult> success, Action<Exception> error, Action complete = null);
    }
}
