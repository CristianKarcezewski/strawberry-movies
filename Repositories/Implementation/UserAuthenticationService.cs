using Microsoft.AspNetCore.Identity;
using NuGet.Protocol;
using Strawberry.Models.Domain;
using Strawberry.Models.DTO;
using Strawberry.Repositories.Abstract;
using System.Net;
using System.Security.Claims;

namespace Strawberry.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<UserModel> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<UserModel> signInManager;

        public UserAuthenticationService(
            UserManager<UserModel> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<UserModel> signInManager
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<StatusDTO> LoginAsync(LoginDTO dto)
        {
            var status = new StatusDTO();

            //Check credentials
            var user = await userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                status.StatusCode = ((int)HttpStatusCode.NotFound);
                status.Message = "Login inválido";
                return status;
            }
            var pw = await userManager.CheckPasswordAsync(user, dto.Password);
            if (!pw)
            {
                status.StatusCode = ((int)HttpStatusCode.NotFound);
                status.Message = "Login inválido";
                return status;
            }

            var signResult = await signInManager.PasswordSignInAsync(user, dto.Password, false, true);
            if (signResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = ((int)HttpStatusCode.OK);
                status.Message = "Login realizado com sucesso.";
            }
            else if (signResult.IsLockedOut)
            {
                status.StatusCode = ((int)HttpStatusCode.Unauthorized);
                status.Message = "Token de usuário expirado";
            }
            else
            {
                status.StatusCode = ((int)HttpStatusCode.Unauthorized);
                status.Message = "Erro ao realizar o login";
            }

            return status;
        }

        public async Task<StatusDTO> RegisterAsync(RegisterDTO dto)
        {
            var status = new StatusDTO();

            //Verify if axists an user with same name already registered
            var userExists = await userManager.FindByNameAsync(dto.UserName);
            if (userExists != null)
            {
                status.StatusCode = ((int)HttpStatusCode.Conflict);
                status.Message = "Este usuário já existe.";
                return status;
            }

            //Parce DTO data and register new user
            UserModel user = new UserModel();
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.UserName = dto.UserName;
            user.Email = dto.Email;

            var result = await userManager.CreateAsync(user,dto.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = ((int)HttpStatusCode.NotImplemented);
                //status.Message = "Erro ao salvar novo usuário.";
                status.Message = result.ToJson();
                return status;
            }

            if(!await roleManager.RoleExistsAsync(dto.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(dto.Role));
                await userManager.AddToRoleAsync(user, dto.Role);
            }

            status.StatusCode = ((int)HttpStatusCode.Created);
            status.Message = "Usuário registrado com sucesso.";
            return status;
        }
    }
}
