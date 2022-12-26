using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly employee_managementContext _context;
    public EmployeeController(employee_managementContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Employees.ToList());
    }
}