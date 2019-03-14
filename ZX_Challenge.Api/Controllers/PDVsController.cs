using Microsoft.AspNetCore.Mvc;
using ZX_Challenge.Application.Requests;
using ZX_Challenge.Application.Responses;
using ZX_Challenge.Application.Services;

namespace ZX_Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDVsController : ControllerBase
    {
        private IPdvService _service;

        public PDVsController(IPdvService service)
        {
            this._service = service;
        }

        [HttpPost]
        [Produces("application/JSON")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post(PdvRequest request)
        {
            var pdv = _service.Create(request);

            return CreatedAtAction(nameof(Get), new { id = pdv.Id }, pdv);
        }

        [HttpGet("{id}")]
        [Produces("application/JSON")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<PdvResponse> Get(int id)
        {
            var pdv = _service.Get(id);

            if (pdv == null)
            {
                return NotFound();
            }

            return Ok(pdv);
        }

        [HttpGet("Search")]
        [Produces("application/JSON")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<PdvResponse> Search(double lng, double lat)
        {
            var pdv = _service.Search(lng, lat);

            if (pdv == null)
            {
                return NotFound();
            }

            return Ok(pdv);
        }
    }
}