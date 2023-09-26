using Microsoft.AspNetCore.Mvc;
using Strawberry.Models.DTO;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
            this.RegisterAdmin();
        }
        public IActionResult Register()
        {
            return View();
        }

        /*
         * Each time the application starts, lets try to register an admin account.
         * If the account name already is registered, the method will fail.
         */
        private async void RegisterAdmin()
        {
            var dto = new RegisterDTO();
            dto.Name = "admin";
            dto.UserName = "admin";
            dto.Email = "admin@gmail.com";
            dto.Password = "admin@123";
            dto.PasswordConfirm = "admin@123";
            dto.Role = "Admin";

            var result = await authService.RegisterAsync(dto);
            if (result.StatusCode.Equals(201))
            {
                System.Diagnostics.Debug.WriteLine("Registrando usuário admin.");
            }
            if (result.StatusCode.Equals(409))
            {
                System.Diagnostics.Debug.WriteLine("Usuário admin já registrado.");
            }
            if (result.StatusCode.Equals(501))
            {
                System.Diagnostics.Debug.WriteLine("Erro ao registrar usuário admin.");
            }
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await authService.LoginAsync(dto);
            if (result.StatusCode.Equals(200))
            { 
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["msg"] = "Não foi possível realizar o login";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }
    }
}
