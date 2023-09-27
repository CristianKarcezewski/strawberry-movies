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
        }
        public IActionResult Register()
        {
            return View();
        }

        /*
         * Each time the application starts, lets try to register an admin account.
         * If the account name already is registered, the method will fail.
         */
        public async Task<IActionResult> RegisterAdmin()
        {
            var dto = new RegisterDTO
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                Password = "Admin@123",
                PasswordConfirm = "Admin@123",
                Role = "Admin",
            };

            var result = await authService.RegisterAsync(dto);
            return Ok(result.Message);
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
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
