using GraphQL.Types;

namespace ZX_Challenge.Application.GraphQL
{
    public class PdvInputType : InputObjectGraphType
    {
        public PdvInputType()
        {
            Name = "PdvRequest";
            Field<NonNullGraphType<StringGraphType>>("tradingName");
            Field<NonNullGraphType<StringGraphType>>("ownerName");
            Field<NonNullGraphType<StringGraphType>>("document");
            Field<NonNullGraphType<StringGraphType>>("coverageArea");
            Field<NonNullGraphType<StringGraphType>>("address");
        }
    }
}