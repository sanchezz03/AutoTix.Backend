using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

public class Loyalty
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("info_link")]
    public string InfoLink { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }

    [JsonProperty("icon_hugs")]
    public string IconHugs { get; set; }

    [JsonProperty("points_count")]
    public int PointsCount { get; set; }

    [JsonProperty("points")]
    public Points Points { get; set; }

    [JsonProperty("data_columns")]
    public List<DataColumn> DataColumns { get; set; }

    [JsonProperty("info_block")]
    public InfoBlock InfoBlock { get; set; }
}
