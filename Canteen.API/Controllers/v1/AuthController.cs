using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Canteen.Core.DataAccess;
using Canteen.Core.Entities;
using Canteen.Core.BusinessModels;
using Microsoft.AspNetCore.Authorization;
using Canteen.Core.Managers;

namespace Canteen.API.Controllers.v1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IActiveRepository _ctx;
        private readonly IAuthManger _auth;

        public AuthController(IActiveRepository context, IAuthManger auth)
        {
            _auth = auth;
            _ctx = context;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login(AuthenticateModel model)
        {
            var user = _auth.LoginUser(model);

            return Ok(user);
        }
        //// GET: api/Auth
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<AppUser>>> GetAppUsers()
        //{
        //    return await _ctx..ToListAsync();
        //}

        //// GET: api/Auth/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<AppUser>> GetAppUser(int id)
        //{
        //    var appUser = await _ctx.AppUsers.FindAsync(id);

        //    if (appUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return appUser;
        //}

        //// PUT: api/Auth/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAppUser(int id, AppUser appUser)
        //{
        //    if (id != appUser.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _ctx.Entry(appUser).State = EntityState.Modified;

        //    try
        //    {
        //        await _ctx.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AppUserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Auth
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
        //{
        //    _ctx.AppUsers.Add(appUser);
        //    await _ctx.SaveChangesAsync();

        //    return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
        //}

        //// DELETE: api/Auth/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<AppUser>> DeleteAppUser(int id)
        //{
        //    var appUser = await _ctx.AppUsers.FindAsync(id);
        //    if (appUser == null)
        //    {
        //        return NotFound();
        //    }

        //    _ctx.AppUsers.Remove(appUser);
        //    await _ctx.SaveChangesAsync();

        //    return appUser;
        //}

        //private bool AppUserExists(int id)
        //{
        //    return _ctx.AppUsers.Any(e => e.Id == id);
        //}
    }
}
