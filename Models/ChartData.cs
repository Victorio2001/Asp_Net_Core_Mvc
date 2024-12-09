namespace NomDuProjet.Models;

public class ChartData
{
    public List<string> Labels { get; set; } = new();
    public List<int> Data { get; set; } = new();
    public string ChartType { get; set; } = "bar";
}