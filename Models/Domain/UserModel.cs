using Microsoft.AspNetCore.Identity;

namespace Strawberry.Models.Domain
{
    public class UserModel : IdentityUser
    {
        public string Name{ get; set; }
    }
}
