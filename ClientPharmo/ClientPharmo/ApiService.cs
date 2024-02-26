using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7252/")
        };
    }

    public async Task<List<Medicine>> GetMedicines()
    {
        // Выполняем GET-запрос к "api/medicines"
        var response = await _client.GetAsync("api/medicines");
        response.EnsureSuccessStatusCode();

        // Читаем содержимое ответа
        var content = await response.Content.ReadAsStringAsync();

        // Переносим содержимое ответа в список объектов Medicine
        return JsonConvert.DeserializeObject<List<Medicine>>(content);
    }

    public async Task<List<Pharmacy>> GetPharmacies(int medicineCode)
    {
        // Выполняем GET-запрос к "api/pharmacies/{medicineCode}", где medicineCode - код лекарства
        var response = await _client.GetAsync($"api/pharmacies/{medicineCode}");
        response.EnsureSuccessStatusCode();

        // Читаем содержимое ответа
        var content = await response.Content.ReadAsStringAsync();

        // Переносим содержимое ответа в список объектов Pharmacy
        return JsonConvert.DeserializeObject<List<Pharmacy>>(content);
    }

    public async Task DeleteExpiredMedicines()
    {
        // Выполняем DELETE-запрос к "api/medicines/expired"
        var response = await _client.DeleteAsync("api/medicines/expired");
        response.EnsureSuccessStatusCode();
    }
}
