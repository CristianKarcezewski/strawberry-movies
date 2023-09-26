using Strawberry.Models.DTO;

namespace Strawberry.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<StatusDTO> LoginAsync(LoginDTO dto);
        Task LogoutAsync();
        Task<StatusDTO> RegisterAsync(RegisterDTO dto);
    }
}
