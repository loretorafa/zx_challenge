
using GeoAPI.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using ZX_Challenge.Domain.Models;
using ZX_Challenge.Domain.Interfaces.Responses;

namespace ZX_Challenge.Application.Responses
{
    public class PdvResponse : IPdvResponse
    {

        public PdvResponse(Pdv pdv)
        {
            this.Id = pdv.Id.ToString();
            this.TradingName = pdv.TradingName;
            this.OwnerName = pdv.OwnerName;
            this.Document = pdv.Document;
            this.CoverageArea = Serialize(pdv.CoverageArea);
            this.Address = Serialize(pdv.Address);
        }

        public string Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public string CoverageArea { get; set; }
        public string Address { get; set; }

        private string Serialize(IGeometry geo)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            JsonSerializer serializer = GeoJsonSerializer.CreateDefault();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            serializer.Serialize(writer, geo);

            return sb.ToString();
        }
    }

    
}
