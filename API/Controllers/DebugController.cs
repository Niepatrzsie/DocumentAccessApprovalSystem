using Document_Access_Approval_System.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Document_Access_Approval_System.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebugController : ControllerBase
{
    private readonly AppDbContext _context;

    public DebugController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var users = _context.Users
            .Select(u => new { u.Id, u.Name, u.Role })
            .ToList();
        return Ok(users);
    }

    [HttpGet("documents")]
    public IActionResult GetDocuments()
    {
        var docs = _context.Documents
            .Select(d => new { d.Id, d.Title })
            .ToList();
        return Ok(docs);
    }
}
