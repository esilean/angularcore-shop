using System.Threading.Tasks;
using AngularShop.Application.Dtos.Address;
using AngularShop.Application.Dtos.Login;
using AngularShop.Application.Dtos.Register;
using AngularShop.Application.Errors;
using AngularShop.Application.UseCases.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AngularShop.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountUseCase _accountUseCase;

        public AccountController(ILogger<AccountController> logger,
                                 IAccountUseCase accountUseCase)
        {
            _logger = logger;
            _accountUseCase = accountUseCase;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<LoginResponse>> GetCurrentUser()
        {
            return await _accountUseCase.GetCurrentUser();
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressResponse>> GetCurrentUserAddress()
        {
            return await _accountUseCase.GetCurrentUserAddress();
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<AddressResponse>> UpdateCurrentUserAddress(AddressRequest addressRequest)
        {
            var addressUpdated = await _accountUseCase.UpdateCurrentUserAddress(addressRequest);
            if (addressUpdated == null) return BadRequest(new ApiResponse(400));
            return Ok(addressUpdated);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailsExists([FromQuery] string email)
        {
            return await _accountUseCase.CheckEmailExists(email);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var loginResponse = await _accountUseCase.Login(loginRequest);
            if (loginResponse is null) return Unauthorized(new ApiResponse(401));

            return loginResponse;
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> Register(RegisterRequest registerRequest)
        {
            var loginResponse = await _accountUseCase.Register(registerRequest);
            if (loginResponse is null) return BadRequest(new ApiResponse(400));

            return loginResponse;
        }

        [HttpDelete("login/{email}")]
        [Authorize]
        public async Task<IActionResult> Remove(string email)
        {
            var userRemoved = await _accountUseCase.Remove(email);
            if (!userRemoved) return BadRequest(new ApiResponse(400));

            return NoContent();
        }
    }
}