using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace RadzenEndToEndAngularApplication.Controllers
{
    [Authorize(Roles="Administrator", ActiveAuthenticationSchemes="Bearer")]
    [Route("auth/users")]
    public class UsersController : Controller
    {
       private UserManager<IdentityUser> userManager;

       public UsersController(UserManager<IdentityUser> userManager)
       {
           this.userManager = userManager;
       }

       [HttpGet]
       public IEnumerable<IdentityUser> Get()
       {
           return userManager.Users;
       }

       [HttpDelete("{id}")]
       public async Task<IActionResult> Delete(string id)
       {
           var user = await userManager.FindByIdAsync(id);

           if (user == null)
           {
               return NotFound();
           }

           await userManager.DeleteAsync(user);

           return new NoContentResult();
       }
    }
}
