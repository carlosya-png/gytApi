using System.Collections.Generic;
using System.Text.Json.Serialization;

public class CotizacionResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public CotizacionSaldoData Data { get; set; } = new();

    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; } = new();
}

public class CotizacionSaldoData
{
    [JsonPropertyName("prSaldo")]
    public string PrSaldo { get; set; } = string.Empty;

    [JsonPropertyName("prUbicacion")]
    public string PrUbicacion { get; set; } = string.Empty;

    [JsonPropertyName("prSDPLCAE")]
    public string PrSDPLCAE { get; set; } = string.Empty;

    [JsonPropertyName("prMarca")]
    public string PrMarca { get; set; } = string.Empty;

    [JsonPropertyName("prTipo")]
    public string PrTipo { get; set; } = string.Empty;

    [JsonPropertyName("prMode")]
    public string PrMode { get; set; } = string.Empty;

    [JsonPropertyName("prColor")]
    public string PrColor { get; set; } = string.Empty;

    [JsonPropertyName("prClave")]
    public string PrClave { get; set; } = string.Empty;

    [JsonPropertyName("prDesc")]
    public string PrDesc { get; set; } = string.Empty;
}