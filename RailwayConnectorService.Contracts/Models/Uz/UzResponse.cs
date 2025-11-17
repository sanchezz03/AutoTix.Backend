namespace RailwayConnectorService.Contracts.Models.Uz;

public class UzResponse<T>
{
    public T? Data { get; set; }
    public string? Error { get; set; }
}
