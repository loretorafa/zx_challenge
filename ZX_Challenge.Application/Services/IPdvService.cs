using ZX_Challenge.Application.Requests;
using ZX_Challenge.Application.Responses;

namespace ZX_Challenge.Application.Services
{
    public interface IPdvService
    {
        PdvResponse Create(PdvRequest pdv);
        PdvResponse Get(int id);
        PdvResponse Search(double lat, double lng);
    }
}
