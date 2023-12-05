using Companies.Client.Models;
using Companies.Shared.Dtos.CompaniesDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Companies.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient httpClient;
        private const string json = "application/json";
        public HomeController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7259");
        }

        public async Task<IActionResult> Index()
        {
            var res = await SimpleGetAsync();
            var res2 = await SimpleGetAsync2();
            var res3 = await GetWithRequestMessageAsync();
            var res4 = await PostWithRequestMessageAsync();

            return View();
        }

        private async Task<CompanyDto> PostWithRequestMessageAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<CompanyDto>> GetWithRequestMessageAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/companies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var res = await response.Content.ReadAsStringAsync();
            var companies = JsonSerializer.Deserialize<IEnumerable<CompanyDto>>(res,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return companies!;
        }

        private async Task<IEnumerable<CompanyDto>> SimpleGetAsync()
        {
            var response = await httpClient.GetAsync("api/companies");
            response.EnsureSuccessStatusCode();

            var res = await response.Content.ReadAsStringAsync();
            var companies = JsonSerializer.Deserialize<IEnumerable<CompanyDto>>(res,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return companies!;
        }

        private async Task<IEnumerable<CompanyDto>?> SimpleGetAsync2()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<CompanyDto>>("api/companies",
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
