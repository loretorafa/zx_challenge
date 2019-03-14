using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System.IO;
using ZX_Challenge.Domain.Interfaces.Requests;

namespace ZX_Challenge.Domain.Models
{
    public class Pdv
    {
        public Pdv()
        { }

        public Pdv(IPdvRequest request)
        {

            var gjs = GeoJsonSerializer.CreateDefault();

            this.Id = 0;
            this.TradingName = request.TradingName;
            this.OwnerName = request.OwnerName;
            this.Document = request.Document;
            this.CoverageArea = gjs.Deserialize<MultiPolygon>(new JsonTextReader(new StringReader(request.CoverageArea)));
            this.CoverageArea.SRID = 4326;
            this.Address = gjs.Deserialize<Point>(new JsonTextReader(new StringReader(request.Address)));
            this.Address.SRID = 4326;
        }

        public int Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public IMultiPolygon CoverageArea { get; set; }
        public IPoint Address { get; set; }
    }
}
