using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using ZX_Challenge.Application.Responses;
using ZX_Challenge.Domain.Models;
using ZX_Challenge.Domain.Interfaces.Repositories;
using ZX_Challenge.Application.Requests;
using ZX_Challenge.Application.Validators;
using FluentValidation;

namespace ZX_Challenge.Application.Services
{
    public class PdvService : IPdvService
    {
        private IPdvRepository _repository;
        private PdvRequestValidator _validator;

        public PdvService(IPdvRepository repository)
        {
            this._repository = repository;
            this._validator = new PdvRequestValidator(_repository);
        }

        public PdvResponse Create(PdvRequest request)
        {
            _validator.ValidateAndThrow(request);
            //TODO: Implementar handler validação

            Pdv pdv = _repository.Insert(new Pdv(request));

            return new PdvResponse(pdv);
        }

        public PdvResponse Get(int id)
        {
            Pdv pdv = _repository.Select(id);

            if (pdv == null)
                return null;

            return new PdvResponse(pdv);
        }

        public PdvResponse Search(double lng, double lat)
        {
            IPoint location = new Point(lng, lat);
            location.SRID = 4326;

            Pdv pdv = _repository.Search(location);

            if (pdv == null)
                return null;

            return new PdvResponse(pdv);
        }
    }
}
