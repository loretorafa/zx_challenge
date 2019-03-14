using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZX_Challenge.Application.Requests;
using ZX_Challenge.Application.Responses;
using ZX_Challenge.Application.Services;
using ZX_Challenge.Domain.Models;
using ZX_Challenge.Domain.Interfaces.Repositories;
using ZX_Challenge.Tests.Fakes;
using FluentValidation;

namespace Tests
{
    public class PdvServiceTests
    {
        private IPdvService _service;
        private IPdvRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakePdvRepository();
            this._service = new PdvService(_repository);
        }

        [Test]
        public void Create_ValidPdvRequest_ReturnsPdvResponse()
        {
            var request = this.RandomValidPdvRequest;

            var response = _service.Create(request);

            Assert.IsNotNull(response);
            Assert.AreSame(typeof(PdvResponse), response.GetType());

            Assert.AreEqual(request.TradingName, response.TradingName);
            Assert.AreEqual(request.OwnerName, response.OwnerName);
            Assert.AreEqual(request.Document, response.Document);
            Assert.AreEqual(request.Address, response.Address);
        }

        [Test]
        public void Create_EmptyPdvRequest_ThrowsValidationException()
        {
            var request = new PdvRequest();

            TestDelegate testDelegate = new TestDelegate(() => _service.Create(request));

            Assert.Throws<ValidationException>(testDelegate);
        }

        [Test]
        public void Get_PdvExists_ReturnsPdvResponse()
        {
            var pdv = this.RandomExistingPdv;
            var expected = new PdvResponse(pdv);

            var response = _service.Get(pdv.Id);

            Assert.IsNotNull(response);
            Assert.AreSame(typeof(PdvResponse), response.GetType());
            Assert.AreEqual(expected.Id, response.Id);
        }

        [Test]
        public void Get_PdvDoesNotExist_ReturnsNull()
        {
            const int id = 0;

            var response = _service.Get(id);

            Assert.IsNull(response);
        }

        [Test]
        public void Search_ValidLocation_ReturnsPdvResponse()
        {
            var pdv = this.RandomExistingPdv;
            var expected = new PdvResponse(pdv);

            var response = _service.Search(pdv.Address.Coordinates[0].X, pdv.Address.Coordinates[0].Y);

            Assert.IsNotNull(response);
            Assert.AreSame(typeof(PdvResponse), response.GetType());
            Assert.AreEqual(expected.Id, response.Id);
        }


        [Test]
        public void Search_InvalidLocation_ReturnsNull()
        {
            const double lng = 200;
            const double lat = 200;

            var response = _service.Search(lng , lat);

            Assert.IsNull(response);
        }

        #region private

        private void PopulateRepository(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _repository.Insert(new Pdv(SampleValidPdvsRequests[i]));
            }
        }

        private List<PdvRequest> SampleValidPdvsRequests
        {
            get
            {
                List<PdvRequest> list = new List<PdvRequest>();

                string json = File.ReadAllText("content/pdvs.json");

                list = JsonConvert.DeserializeObject<List<PdvRequest>>(json);

                return list;
            }
        }

        private PdvRequest RandomValidPdvRequest
        {
            get
            {
                return SampleValidPdvsRequests.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            }
        }

        private Pdv RandomExistingPdv
        {
            get
            {
                Random r = new Random();
                var quantity = r.Next(SampleValidPdvsRequests.Count);
                PopulateRepository(quantity);

                var pdv = _repository.Select(quantity);

                return pdv;
            }
        }

        #endregion
    }
}