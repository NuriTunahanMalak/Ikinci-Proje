using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    // POST api/ProjectProduct
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProjectProduct projectProduct)
    {
        if (projectProduct == null)
            return BadRequest("Eksik veri.");

        // İlgili Project ve Product var mı kontrol et
        var existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectProduct.ProjectId);
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == projectProduct.ProductId);

        if (existingProject == null)
            return NotFound("İlgili Project bulunamadı.");
        if (existingProduct == null)
            return NotFound("İlgili Product bulunamadı.");

        // Aynı ilişki zaten var mı kontrol et
        var alreadyExists = await _context.ProjectProducts
            .AnyAsync(pp => pp.ProjectId == projectProduct.ProjectId && pp.ProductId == projectProduct.ProductId);
        if (alreadyExists)
            return Conflict("Bu ilişki zaten mevcut.");

        _context.ProjectProducts.Add(projectProduct);
        await _context.SaveChangesAsync();
        // Composite key ile CreatedAtAction kullanımı:
        return CreatedAtAction(nameof(Add), new { projectId = projectProduct.ProjectId, productId = projectProduct.ProductId }, projectProduct);
    }

    // DELETE api/ProjectProduct?projectId=1&productId=2
    [HttpDelete]
    public async Task<IActionResult> Remove([FromQuery] int projectId, [FromQuery] int productId)
    {
        try
        {
            var projectProduct = await _context.ProjectProducts
                .FirstOrDefaultAsync(pp => pp.ProjectId == projectId && pp.ProductId == productId);

            if (projectProduct == null)
                return NotFound("Silinecek ProjectProduct bulunamadı.");

            _context.ProjectProducts.Remove(projectProduct);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Sunucu hatası: {ex.Message}");
        }
    }
}