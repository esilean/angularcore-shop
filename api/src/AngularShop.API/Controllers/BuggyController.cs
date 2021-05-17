using AngularShop.Application.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AngularShop.API.Controllers
{
    public class BuggyController : BaseController
    {
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            string error = null;
            return error.ToString();
        }

        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("bad-request/{id}")]
        public IActionResult GetBadRequestId(int id)
        {
            return Ok();
        }

    }
}