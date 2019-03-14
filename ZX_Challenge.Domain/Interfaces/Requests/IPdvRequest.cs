namespace ZX_Challenge.Domain.Interfaces.Requests
{
    public interface IPdvRequest
    {
        string TradingName { get; set; }
        string OwnerName { get; set; }
        string Document { get; set; }
        string CoverageArea { get; set; }
        string Address { get; set; }
    }
}
