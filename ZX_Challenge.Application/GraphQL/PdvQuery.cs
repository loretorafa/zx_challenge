using GeoAPI.Geometries;
using GraphQL.Types;
using ZX_Challenge.Application.Services;

namespace ZX_Challenge.Application.GraphQL
{
    public class PdvQuery : ObjectGraphType
    {
        public PdvQuery(IPdvService pdvService)
        {


            Field<PdvType>(
                "pdv",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => pdvService.Get(context.GetArgument<int>("id")));

            Field<PdvType>(
            "closest",
            arguments: new QueryArguments(
                new QueryArgument<FloatGraphType>() { Name = "lng" },
                new QueryArgument<FloatGraphType>() { Name = "lat" }
            ),
            resolve: context =>
            {
                double lng = context.GetArgument<double>("lng");
                double lat = context.GetArgument<double>("lat");

                return pdvService.Search(lng, lat);
            });
        }
    }
}





