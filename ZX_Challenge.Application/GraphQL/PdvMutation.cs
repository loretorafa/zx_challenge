using GraphQL.Types;
using ZX_Challenge.Application.Requests;
using ZX_Challenge.Application.Services;
using ZX_Challenge.Domain.Models;

namespace ZX_Challenge.Application.GraphQL
{
    public class PdvMutation : ObjectGraphType
    {
        public PdvMutation(IPdvService pdvService)
        {
            Name = "PdvMutation";

            Field<PdvType>(
                "createPdv",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PdvInputType>> { Name = "pdv" }
                ),
                resolve: context =>
                {
                    var pdv = context.GetArgument<PdvRequest>("pdv");
                    return pdvService.Create(pdv);
                });
        }
    }
}
