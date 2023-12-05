using Companies.Client.Models;
using Companies.Shared.Dtos.CompaniesDtos;
using Companies.Shared.Dtos.EmployeesDtos;
using Microsoft.AspNetCore.JsonPatch;
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
            //var res = await SimpleGetAsync();
            //var res2 = await SimpleGetAsync2();
            //var res3 = await GetWithRequestMessageAsync();
            //var res4 = await PostWithRequestMessageAsync();
            await PatchWithRequestMessageAsync();

            return View();
        }

        private async Task PatchWithRequestMessageAsync()
        {
            var patchDocument = new JsonPatchDocument<EmployeesForUpdateDto>();
            patchDocument.Replace(e => e.Age, 75);
            patchDocument.Replace(e => e.Name, "Kalle Anka");

            var serializedPatchDoc = Newtonsoft.Json.JsonConvert.SerializeObject(patchDocument);

            var request = new HttpRequestMessage(HttpMethod.Patch, "api/companies/96bbaba3-2503-4973-a4eb-08dbf5e33284/employees/f0327c87-2d2e-4dd4-ac88-08dbf5e33286");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(json);
            request.Content = new StringContent(serializedPatchDoc);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private async Task<CompanyDto> PostWithRequestMessageAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/companies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var companyToCreate = new CompanyForCreationDto
            {
                Name = "Apelsinkompaniet",
                Address = "Sveavägen 34",
                Country = "Sweden"
            };

            var jsonCompany = JsonSerializer.Serialize(companyToCreate);

            request.Content = new StringContent(jsonCompany);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(json);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var res = await response.Content.ReadAsStringAsync();
            var companyDto = JsonSerializer.Deserialize<CompanyDto>(res,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var location = response.Headers.Location;
            return companyDto!;
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
