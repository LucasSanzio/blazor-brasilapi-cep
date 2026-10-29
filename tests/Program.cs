using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using BlazorWasmBrasilApiCep.Models;

using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
var address = await http.GetFromJsonAsync<CepResponse>("https://brasilapi.com.br/api/cep/v1/30130000");
Require(address?.Cep == "30130000", "CEP não foi desserializado.");
Require(address?.City == "Belo Horizonte" && address.State == "MG", "Cidade/estado inesperados.");
Require(address?.Street == "Avenida Afonso Pena" && address.Neighborhood == "Centro", "Endereço inesperado.");
Require(!string.IsNullOrWhiteSpace(address?.Service), "Serviço ausente.");
Require(address?.Ibge?.City == "3106200" && address.Ibge.State == "31", "IBGE inesperado.");
using var missing = await http.GetAsync("https://brasilapi.com.br/api/cep/v1/00000000");
Require(missing.StatusCode == HttpStatusCode.NotFound, "CEP inexistente deveria retornar 404.");
var partial = JsonSerializer.Deserialize<CepResponse>("{\"cep\":\"12345678\",\"city\":\"Cidade\"}", new JsonSerializerOptions(JsonSerializerDefaults.Web));
Require(partial?.Ibge is null && partial?.Street is null, "Campos opcionais não devem ser inventados.");
Console.WriteLine("OK: CEP real, endereço completo, serviço, códigos IBGE, HTTP 404 e campos opcionais.");

static void Require(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}
