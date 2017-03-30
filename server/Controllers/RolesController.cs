using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace RadzenEndToEndAngularApplication.Controllers
{
    [Authorize(Roles="Administrator", ActiveAuthenticationSchemes="Bearer")]
    [Route("auth/roles")]
    public class RolesController : Controller
    {
       private RoleManager<IdentityRole> roleManager;

       public RolesController(RoleManager<IdentityRole> roleManager)
       {
           this.roleManager = roleManager;
       }

       [HttpGet]
       public IEnumerable<IdentityRole> Get()
       {
           return roleManager.Roles;
       }

       [HttpPost]
       public async Task<IActionResult> Post([FromBody] IdentityRole role)
       {
           if (role == null)
           {
               return BadRequest();
           }

           await roleManager.CreateAsync(role);

           return Created($"auth/roles/{role.Id}", role);
       }

       [HttpDelete("{id}")]
       public async Task<IActionResult> Delete(string id)
       {
           var role = await roleManager.FindByIdAsync(id);

           if (role == null)
           {
               return NotFound();
           }

           await roleManager.DeleteAsync(role);

           return new NoContentResult();
       }
    }
}
