using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AssassinsManager.Api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAccount()
    {
        return BadRequest("Not implemented.");
    }
}
