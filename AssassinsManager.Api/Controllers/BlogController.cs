using Microsoft.AspNetCore.Mvc;
using AssassinsManager.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AssassinsManager.Api.Controllers;

[ApiController]
[Route("api/blogs")]
public class BlogController(AssassinCoreContext dbContext) : ControllerBase
{
    private readonly AssassinCoreContext _dbContext = dbContext;

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        return Ok(await _dbContext.Blogs.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromBody] Blog blog)
    {
        await _dbContext.Blogs.AddAsync(blog);
        await _dbContext.SaveChangesAsync();
        return Ok(blog);
    }
}