using GeoAPI.Geometries;
using System.Collections.Generic;
using ZX_Challenge.Domain.Models;

namespace ZX_Challenge.Domain.Interfaces.Repositories
{
    public interface IPdvRepository
    {
        Pdv Insert(Pdv pdv);
        Pdv Select(int id);
        Pdv Search(IPoint location);
        List<string> Documents { get; }
    }
}
