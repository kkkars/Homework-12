using Microsoft.AspNetCore.Mvc;
using DepsWebApp.Filters;
using System;
using DepsWebApp.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using DepsWebApp.Services;
using System.Threading.Tasks;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Authorization controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [CustomExceptionFilter]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accounts;

        public AuthController(
            IAccountService accounts)
        {
            _accounts = accounts;
        }

        /// <summary>
        /// Register method that register a new account
        /// </summary>
        /// <param name="newAccount">LoginData model that contains login and password</param>
        /// <exception cref="NotImplementedException"> Temporary: action does not have implementation.</exception>
        [HttpPost]
        [Route("/register")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> Register([FromBody] Account newAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var successfullyAdded = await _accounts.Add(newAccount);
            if (!successfullyAdded)
            {
                return Conflict("Account with such login is already exist");
            }

            return Ok();
        }
    }
}
