using Miachyna.Domain.Entities;
using Miachyna.Domain.Models;
using System.Text.Json;

namespace Miachyna.UI.Services.CosmeticServices
{
    public class ApiCosmeticService(HttpClient httpClient) : ICosmeticService
    {
        public async Task<ResponseData<Cosmetic>> CreateCosmeticAsync(Cosmetic product, IFormFile? formFile)
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var responseData = new ResponseData<Cosmetic>();

            var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, product);
            if (!response.IsSuccessStatusCode)
            {
                responseData.Success = false;
                responseData.ErrorMessage = $"Failed to create object: {response.StatusCode}";
                return responseData;
            }

            if (formFile != null)
            {
                var cosmetic = await response.Content.ReadFromJsonAsync<Cosmetic>();

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/{cosmetic.Id}")
                };

                var content = new MultipartFormDataContent();

                var streamContent = new StreamContent(formFile.OpenReadStream());

                content.Add(streamContent, "image", formFile.FileName);

                request.Content = content;

                response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    responseData.Success = false;
                    responseData.ErrorMessage = $"Failed to save image: {response.StatusCode}";
                }
            }
            return responseData;
        }

        public Task DeleteCosmeticAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<Cosmetic>> GetCosmeticByIdAsync(int id)
        {
            var apiUrl = $"{httpClient.BaseAddress.AbsoluteUri}{id}";
            var response = await httpClient.GetFromJsonAsync<Cosmetic>(apiUrl);
            return new ResponseData<Cosmetic>() { Data = response };
        }

        public async Task<ResponseData<ListModel<Cosmetic>>> GetCosmeticListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var uri = httpClient.BaseAddress;

            var queryData = new Dictionary<string, string>()
            {
                { "pageNo", pageNo.ToString() }
            };

            if (!String.IsNullOrEmpty(categoryNormalizedName))
            {
                queryData.Add("category", categoryNormalizedName);
            }
            var query = QueryString.Create(queryData);
            var result = await httpClient.GetAsync(uri + query.Value);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<ListModel<Cosmetic>>>();
            };
            var response = new ResponseData<ListModel<Cosmetic>>
            { Success = false, ErrorMessage = "API reading error!" };
            return response;
        }

        public Task UpdateCosmeticAsync(int id, Cosmetic product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}