using ZX_Challenge.Domain.Interfaces.Requests;

namespace ZX_Challenge.Application.Requests
{
    public class PdvRequest : IPdvRequest
    {
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public string CoverageArea { get; set; }
        public string Address { get; set; }
    }
}
