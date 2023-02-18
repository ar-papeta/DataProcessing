
namespace DataProcessing.Models;

internal class OutputModel
{
    public string? City { get; set; }
    public IEnumerable<Service>? Services { get; set; }
    public decimal Total { get; set; }
}
