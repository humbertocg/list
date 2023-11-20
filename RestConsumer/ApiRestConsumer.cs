using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestConsumer.Exceptions;

namespace RestConsumer
{
    public class ApiRestConsumer : IApiRestConsumer
    {
        private HttpClient _httpClient = new HttpClient();
        private string _apiUrl = "";

        public void SetUrl(string apiUrl)
        {
            this._apiUrl = apiUrl;
            _httpClient.BaseAddress = new Uri(_apiUrl);
        }

        public async Task<T> GetAsync<T>(string url, string bearerToken = null) where T : class, new()
        {
            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url)
                };
                if (bearerToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (HttpRequestException e)
            {
                GetException(e);
            }
            catch (Exception e)
            {
                GetException(e);
            }
            return null;
        }

        private void GetException(Exception e)
        {
            Debug.WriteLine("\nException Caught!");
            Debug.WriteLine("Message :{0} ", e.Message);
            throw new HttpGetException("HttGetException", e);
        }

        public async Task<T> PostAsync<T, U>(string url, U data, string bearerToken = null) where T : class, new() where U : class, new()
        {
            try
            {
                var content = JsonConvert.SerializeObject(data);
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
                if (bearerToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (HttpRequestException e)
            {
                PostException(e);
            }
            catch (Exception e)
            {
                PostException(e);
            }
            return null;
        }

        private void PostException(Exception e)
        {
            Debug.WriteLine("\nException Caught!");
            Debug.WriteLine("Message :{0} ", e.Message);
            throw new HttpPostException("HttPostException", e);
        }

        public async Task<bool> DeleteAsync(string url, string bearerToken = null)
        {
            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(url),
                };
                if (bearerToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (HttpRequestException e)
            {
                DeleteException(e);
            }
            catch (Exception e)
            {
                DeleteException(e);
            }
            return false;
        }

        private void DeleteException(Exception e)
        {
            Debug.WriteLine("\nException Caught!");
            Debug.WriteLine("Message :{0} ", e.Message);
            throw new HttpDeleteException("HttDeleteException", e);
        }

        public async Task<T> PutAsync<T, U>(string url, U data, string bearerToken = null) where T : class, new() where U : class, new()
        {
            try
            {
                var content = JsonConvert.SerializeObject(data);
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(url),
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
                if (bearerToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (HttpRequestException e)
            {
                PutException(e);
            }
            catch (Exception e)
            {
                PutException(e);
            }
            return null;
        }

        private void PutException(Exception e)
        {
            Debug.WriteLine("\nException Caught!");
            Debug.WriteLine("Message :{0} ", e.Message);
            throw new HttpPutException("HttPutException", e);
        }
    }
}

