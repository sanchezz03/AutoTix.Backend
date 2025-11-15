namespace RailwayConnectorService.Contracts.Models.Uz;

public class UzResponse<T>
{
    public T Content { get; set; }

    public UzResponse(T content)
    {
        Content = content;
    }
}
