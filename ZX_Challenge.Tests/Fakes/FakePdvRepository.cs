using GeoAPI.Geometries;
using System.Collections.Generic;
using System.Linq;
using ZX_Challenge.Domain.Models;
using ZX_Challenge.Domain.Interfaces.Repositories;

namespace ZX_Challenge.Tests.Fakes
{
    public class FakePdvRepository : IPdvRepository
    {
        private List<Pdv> _pdvs = new List<Pdv>();

        public Pdv Insert(Pdv pdv)
        {
            pdv.Id = _pdvs.Count() + 1;
            _pdvs.Add(pdv);

            return Select(pdv.Id);
        }
        public Pdv Select(int id)
        {
            return _pdvs.FirstOrDefault(p => p.Id == id);
        }

        public Pdv Search(IPoint location)
        {
            return _pdvs.FirstOrDefault(p => p.Address.Coordinates[0].X == location.X
                                            && p.Address.Coordinates[0].Y == location.Y);
        }

        public List<string> Documents
        {
            get
            {
                return this._pdvs.Select(p => p.Document).ToList();
            }
        }
    }
}
