using System.Threading.Tasks;
using AngularShop.Application.Dtos.Address;
using AngularShop.Application.Dtos.Login;
using AngularShop.Application.Dtos.Register;

namespace AngularShop.Application.UseCases.Gateways
{
    public interface IAccountUseCase
    {
        Task<LoginResponse> GetCurrentUser();
        Task<AddressResponse> GetCurrentUserAddress();
        Task<AddressResponse> UpdateCurrentUserAddress(AddressRequest addressRequest);
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<LoginResponse> Register(RegisterRequest registerRequest);
        Task<bool> Remove(string email);
        Task<bool> CheckEmailExists(string email);
    }
}