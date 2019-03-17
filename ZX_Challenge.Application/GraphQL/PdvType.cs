using GraphQL.Types;
using ZX_Challenge.Application.Responses;

namespace ZX_Challenge.Application.GraphQL
{
    public class PdvType : ObjectGraphType<PdvResponse>
    {
        public PdvType()
        {
            Field(x => x.Id);
            Field(x => x.TradingName);
            Field(x => x.OwnerName);
            Field(x => x.Document);
            Field(x => x.CoverageArea);
            Field(x => x.Address);
        }
    }
}