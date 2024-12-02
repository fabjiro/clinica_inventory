using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using Domain.Repository;

namespace Infrastructure.Repository;

public class UploaderRepositoryImpl : IUploaderRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    private const string uuidRegex = @"[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}";

    public UploaderRepositoryImpl(string token, string baseUrl)
    {
        _httpClient = new HttpClient();

          // Agregar el encabezado Authorization
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        _baseUrl = baseUrl;
    }

    public async Task<string> Delete(string url)
    {
        var id = Regex.Match(url, uuidRegex);

        if (id.Success)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/file/{id.Value}");            
            response.EnsureSuccessStatusCode();
            return id.Value;
        }

        return string.Empty;
    }

    public async Task<string> Upload(string base64)
    {
        var response = await _httpClient.PostAsync($"{_baseUrl}/file", JsonContent.Create(new
        {
            File = base64
        }));

        response.EnsureSuccessStatusCode(); // Lanza una excepción si el código de estado no es exitoso

        // Leer y deserializar el campo "link"
        var jsonResponse = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(jsonResponse);

        // Obtener el valor del campo "link"
        if (document.RootElement.TryGetProperty("link", out var linkElement))
        {
            return linkElement.ToString();
        }

        return string.Empty; // Retorna null si el campo no existe
    }
}