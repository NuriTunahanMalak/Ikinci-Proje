using Microsoft.AspNetCore.Mvc;
using SirketData;
using SirketEntites;

[ApiController]
[Route("api/[controller]")]
public class ProjectProductController : ControllerBase
{
    private readonly SirketDbContext _context;

    public ProjectProductController(SirketDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProjectProduct projectProduct)
    {
        _context.ProjectProducts.Add(projectProduct);
        await _context.SaveChangesAsync();
        return Ok(projectProduct);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove([FromBody] ProjectProduct projectProduct)
    {
        _context.ProjectProducts.Remove(projectProduct);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
