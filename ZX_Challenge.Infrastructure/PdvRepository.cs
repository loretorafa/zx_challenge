using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ZX_Challenge.Domain.Models;
using ZX_Challenge.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace ZX_Challenge.Infrastructure
{
    public class PdvRepository : IPdvRepository
    {

        private ZxContext _context;

        public PdvRepository(ZxContext context)
        {
            this._context = context;
        }

        public Pdv Select(int id)
        {
            return _context.Set<Pdv>().Find(id);
        }

        public Pdv Insert(Pdv obj)
        {
            if (obj == null || !obj.CoverageArea.IsValid || !obj.Address.IsValid)
                throw new DbUpdateException("Invalid Entity", new ArgumentException("Invalid Geometry"));

            _context.Set<Pdv>().Add(obj);
            _context.SaveChanges();

            return this.Select(obj.Id);
        }

        public Pdv Search(IPoint location)
        {
            var obj = _context.Pdvs
             .OrderBy(c => c.Address.Distance(location))
             .FirstOrDefault(c => c.CoverageArea.Contains(location));

            return obj;
        }

        public List<string> Documents
        {
            get
            {
                return _context.Set<Pdv>().Select(p => p.Document).ToList();
            }
        }
    }
}
