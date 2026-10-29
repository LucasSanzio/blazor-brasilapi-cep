namespace BlazorWasmBrasilApiCep.Models;

public sealed class CepResponse
{
    public string Cep { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? Neighborhood { get; set; }
    public string? Street { get; set; }
    public string? Service { get; set; }
    public IbgeResponse? Ibge { get; set; }
}

public sealed class IbgeResponse
{
    public string? City { get; set; }
    public string? State { get; set; }
}
