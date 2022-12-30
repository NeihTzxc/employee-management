using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly employee_managementContext _context;
    private IConfiguration _config;
    public EmployeeController(employee_managementContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Employees.ToList());
    }

    [HttpPost("authenticate")]
    public string GenerateJSONWebToken(Employee employeeInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        Random rnd = new Random();
        int num = rnd.Next();
        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, employeeInfo.UserName),
            new Claim(JwtRegisteredClaimNames.Name, employeeInfo.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

        };
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(1200),
            signingCredentials: credentials
        );
        var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodetoken;
    }
    [HttpPost("login")]
    public ActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = Authenticate(userLogin);
        if (user != null)
        {
            var token = GenerateJSONWebToken(user);
            return Ok(token);
        }
        return NotFound("user not found");

    }
    private Employee Authenticate(UserLogin userLogin)
    {
        var currentUser = _context.Employees.FirstOrDefault(x => 
        x.UserName.ToLower() == userLogin.UserName.ToLower() &&
        x.Password == userLogin.Password);
        if (currentUser != null)
        {
            return currentUser;
        }
        return null;
    }
    [HttpPut("{id}/update-avatar")]
    public async Task<IActionResult> UpdateAvatar(int id, string avatar_url)
    {
        if (id == null)
        {
            return BadRequest();
        }
        var employee = new Employee() {Id = id, AvatarUrl = avatar_url};
        var entity = _context.Employees.FirstOrDefault(item => item.Id == id);
        if (entity != null)
        {
            entity.AvatarUrl = avatar_url;
            _context.SaveChanges();
        }
        return Ok();
    }
}