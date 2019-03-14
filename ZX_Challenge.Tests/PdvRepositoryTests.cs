using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using ZX_Challenge.Application.Requests;
using ZX_Challenge.Domain.Models;
using ZX_Challenge.Domain.Interfaces.Repositories;
using ZX_Challenge.Infrastructure;

namespace Tests
{
    public class PdvRepositoryTests
    {
        private ZxContext _context;
        private IPdvRepository _repository;

        [OneTimeSetUp]
        public void SetupDb()
        {
            this._context = new ZxContext();
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            SeedDatabase();
        }

        [SetUp]
        public void Setup()
        {
            this._repository = new PdvRepository(_context);
        }

        [Test, Order(1)]
        public void Insert_ValidPdv_ReturnsPdv()
        {
            Pdv validPdv = NewValidPdv;

            Pdv response = _repository.Insert(validPdv);

            Assert.IsNotNull(response);
            Assert.AreSame(typeof(Pdv), response.GetType());

            Assert.AreEqual(validPdv.TradingName, response.TradingName);
            Assert.AreEqual(validPdv.OwnerName, response.OwnerName);
            Assert.AreEqual(validPdv.Document, response.Document);
            Assert.AreEqual(validPdv.CoverageArea, response.CoverageArea);
            Assert.AreEqual(validPdv.Address, response.Address);
        }

        [Test]
        public void Insert_DuplicateDocument_ThrowsDbUpdateException()
        {
            Pdv duplicate = NewDuplicatePdv;

            TestDelegate testDelegate = new TestDelegate(() => _repository.Insert(duplicate));

            Assert.Throws<DbUpdateException>(testDelegate);
        }

        [Test]
        public void Insert_InvalidPdvRequest_ThrowsDbUpdateException()
        {
            Pdv invalidPdv = NewInvalidPdv;

            TestDelegate testDelegate = new TestDelegate(() => _repository.Insert(invalidPdv));

            Assert.Throws<DbUpdateException>(testDelegate);
        }

        [Test]
        public void Select_ValidId_ReturnsPdv()
        {
            const int id = 1;

            var response = _repository.Select(id);
            Assert.IsNotNull(response);
            Assert.AreSame(typeof(Pdv), response.GetType());
            Assert.AreEqual(id, response.Id);
        }

        [Test]
        public void Select_InvalidId_ReturnsNull()
        {
            const int id = 0;

            var response = _repository.Select(id);

            Assert.IsNull(response);
        }

        [Test]
        public void Search_PdvAddressLocation_ReturnsSamePdv()
        {
            const int id = 1;
            var pdv = _repository.Select(id);

            var response = _repository.Search(pdv.Address);

            Assert.IsNotNull(response);
            Assert.AreEqual(pdv, response);
        }

        [Test]
        public void Search_IslandLocation_ReturnsNull()
        {
            var location = new Point(-12.310750 , - 37.067608);
            location.SRID = 4326;

            var response = _repository.Search(location);

            Assert.IsNull(response);
        }
        [Test]
        public void Search_InvalidLocation_ThrowsException()
        {
            var location = new Point(-200, 200);
            location.SRID = 4326;

            TestDelegate testDelegate = new TestDelegate(() => _repository.Search(location));

            Assert.That(testDelegate, Throws.InstanceOf<Exception>());
        }


        private void SeedDatabase()
        {

            List<PdvRequest> list = new List<PdvRequest>();

            string json = File.ReadAllText("content/seed.json");

            list = JsonConvert.DeserializeObject<List<PdvRequest>>(json);


            foreach (var request in list)
                _context.Add(new Pdv(request));
            _context.SaveChanges();
        }


        private Pdv NewValidPdv
        {
            get
            {
                string json = File.ReadAllText("content/newpdv.json");

                var request = JsonConvert.DeserializeObject<PdvRequest>(json);

                return new Pdv(request);
            }
        }

        private Pdv NewDuplicatePdv
        {
            get
            {
                string json = File.ReadAllText("content/duplicatepdv.json");

                var request = JsonConvert.DeserializeObject<PdvRequest>(json);

                return new Pdv(request);
            }
        }

        private Pdv NewInvalidPdv
        {
            get
            {
                string json = File.ReadAllText("content/invalidpdv.json");

                var request = JsonConvert.DeserializeObject<PdvRequest>(json);

                return new Pdv(request);
            }
        }
    }
}