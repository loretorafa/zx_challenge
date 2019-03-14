using BAMCIS.GeoJSON;
using FluentValidation;
using Newtonsoft.Json;
using ZX_Challenge.Application.Requests;
using ZX_Challenge.Domain.Interfaces.Repositories;

namespace ZX_Challenge.Application.Validators
{
    public class PdvRequestValidator : AbstractValidator<PdvRequest>
    {
        private IPdvRepository _repository;

        public PdvRequestValidator(IPdvRepository repository)
        {
            this._repository = repository;

            RuleFor(x => x.TradingName).NotNull().NotEmpty();
            RuleFor(x => x.OwnerName).NotNull().NotEmpty();
            RuleFor(x => x.Document).NotNull().NotEmpty().Must(BeAvailable).WithMessage("Document not available");
            RuleFor(x => x.CoverageArea).NotNull().NotEmpty().Must(BeAValidMultiPolygon).WithMessage("Invalid MultiPolygon");
            RuleFor(x => x.Address).NotNull().NotEmpty().Must(BeAValidPoint).WithMessage("Invalid Point"); ;
        }

        private bool BeAvailable(string document)
        {
            return !_repository.Documents.Contains(document);
        }


        private bool BeAValidMultiPolygon(string coverageArea)
        {
            try
            {
                MultiPolygon multiPolygon = JsonConvert.DeserializeObject<MultiPolygon>(coverageArea);

                return multiPolygon != null;
            }
            catch
            {
                return false;
            }
        }

        private bool BeAValidPoint(string address)
        {
            try
            {
                Point point = JsonConvert.DeserializeObject<Point>(address);

                return point != null;
            }
            catch
            {
                return false;
            }
        }
    }
}

