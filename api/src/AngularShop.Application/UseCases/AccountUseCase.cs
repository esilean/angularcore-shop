using System.Threading.Tasks;
using AngularShop.Application.Dtos.Address;
using AngularShop.Application.Dtos.Login;
using AngularShop.Application.Dtos.Register;
using AngularShop.Application.Extensions;
using AngularShop.Application.Services.Accessors;
using AngularShop.Application.Services.Security;
using AngularShop.Application.UseCases.Gateways;
using AngularShop.Core.Entities.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AngularShop.Application.UseCases
{
    public class AccountUseCase : IAccountUseCase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserAccessor _userAccessor;

        public AccountUseCase(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              IMapper mapper,
                              ITokenService tokenService,
                              IUserAccessor userAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _userAccessor = userAccessor;
        }

        public async Task<LoginResponse> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailUserFromClaimsPrincipleAsync(_userAccessor.GetUser());

            return new LoginResponse
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        public async Task<AddressResponse> GetCurrentUserAddress()
        {
            var user = await _userManager.FindByEmailUserWithAddressFromClaimsPrincipleAsync(_userAccessor.GetUser());

            return _mapper.Map<Address, AddressResponse>(user.Address);
        }

        public async Task<AddressResponse> UpdateCurrentUserAddress(AddressRequest addressRequest)
        {
            var user = await _userManager.FindByEmailUserWithAddressFromClaimsPrincipleAsync(_userAccessor.GetUser());
            user.Address = _mapper.Map<AddressRequest, Address>(addressRequest);

            var userUpdated = await _userManager.UpdateAsync(user);
            if (!userUpdated.Succeeded) return null;

            return _mapper.Map<Address, AddressResponse>(user.Address);

        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user is null) return null;

            var resultSignIn = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);

            if (!resultSignIn.Succeeded) return null;

            return new LoginResponse
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        public async Task<LoginResponse> Register(RegisterRequest registerRequest)
        {
            var userSignedUp = await _userManager.FindByEmailAsync(registerRequest.Email);
            if (userSignedUp != null) return null;

            var user = new AppUser
            {
                DisplayName = registerRequest.DisplayName,
                Email = registerRequest.Email,
                UserName = registerRequest.Email
            };

            var resultSignUp = await _userManager.CreateAsync(user, registerRequest.Password);

            if (!resultSignUp.Succeeded) return null;

            return new LoginResponse
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        public async Task<bool> Remove(string email)
        {
            var userSignedUp = await _userManager.FindByEmailAsync(email);
            if (userSignedUp == null) return false;

            var resultDeleted = await _userManager.DeleteAsync(userSignedUp);

            if (!resultDeleted.Succeeded) return false;

            return true;
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}