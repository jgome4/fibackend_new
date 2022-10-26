using FI_Infra_Tools_Aggregate;
using FI_Infra_Tools_Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly TokenManagement _tokenManagement;
    private readonly IToken _token;



    public LoginController(TokenManagement tokenManagement, IToken token)
    {
        _token = token;
        _tokenManagement = tokenManagement;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginRequest request)
    {

        if (!_token.IsValidUser(request.UserName, request.Password))
        {
            return BadRequest("Invalid Request");
        }
        var claims = new[]
        {
                new Claim(ClaimTypes.Name,request.UserName)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwtToken = new JwtSecurityToken(
            _tokenManagement.Issuer,
            _tokenManagement.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
            signingCredentials: credentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return Ok(new LoginResult
        {
            UserName = request.UserName,
            JwtToken = token
        });
    }

    
}