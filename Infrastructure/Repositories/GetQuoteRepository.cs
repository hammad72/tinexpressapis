using Domain.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GetQuoteRepository : IGetQuoteRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OrderDbContext _orderDbContext;
        public GetQuoteRepository(IHttpClientFactory httpClientFactory, OrderDbContext orderDbContext)
        {
            _httpClientFactory = httpClientFactory;
            _orderDbContext = orderDbContext;
        }
        public async Task<string> getQuoteCourierPlease(object data)
        {
            string userName = "";
            string password = "";
            string apiUrl = "";
            //var UnPw = await _orderDbContext.courierapisetting.Where(x => x.courier_id == 2).Select(x => new { x.username, x.password }).FirstOrDefaultAsync();
            var cs = await _orderDbContext.courierapisetting.Where(x => x.courier_id == 2 && x.api_type == 1).FirstOrDefaultAsync();
            if(cs!=null)
            {
                userName = cs.username;
                password = cs.password;
                apiUrl = cs.api_url;
            }
            try
            {
                var client = _httpClientFactory.CreateClient();
                var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);
                var jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                else
                {
                    throw new Exception($"Failed to retrieve quotes. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving quotes", ex);
            }
        }

        public async Task<object> getQuoteZoom2u(object data)
        {
            string token = "";
            string apiUrl = "";
            var cs = await _orderDbContext.courierapisetting.Where(x => x.courier_id == 5 && x.api_type == 1).FirstOrDefaultAsync(); 
            if (cs != null)
            {
                token = cs.bearer_token;
                apiUrl = cs.api_url;
            }
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                else
                {
                    throw new Exception($"Failed to retrieve quotes. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving quotes", ex);
            }
        }
    }
}
