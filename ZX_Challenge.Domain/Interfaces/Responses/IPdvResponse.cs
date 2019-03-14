
namespace ZX_Challenge.Domain.Interfaces.Responses
{
    public interface IPdvResponse
    {
        string Id { get; set; }
        string TradingName { get; set; }
        string OwnerName { get; set; }
        string Document { get; set; }
        string CoverageArea { get; set; }
        string Address { get; set; }
    }
}
