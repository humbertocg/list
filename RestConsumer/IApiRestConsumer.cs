using System;
using System.Threading.Tasks;

namespace RestConsumer
{
	public interface IApiRestConsumer
	{
        void SetUrl(string apiUrl);
		Task<T> GetAsync<T>(string url, string bearerToken = null) where T : class, new();
        Task<T> PostAsync<T, U>(string url, U data,  string bearerToken = null) where T : class, new() where U : class, new();
        Task<bool> DeleteAsync(string url, string bearerToken = null);
        Task<T> PutAsync<T, U>(string url, U data, string bearerToken = null) where T : class, new() where U : class, new();
    }
}

