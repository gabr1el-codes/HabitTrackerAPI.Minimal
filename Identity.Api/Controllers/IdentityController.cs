using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using Habits.Application.Database;
using Habits.Application.Models;
using System.Text.Json;
using Identity.Api.Requests;

namespace Identity.Api.Controllers;

[ApiController]
public class IdentityController : ControllerBase
{
    private const string TokenSecret = "ForTheLoveOfGodStoreAndLoadThisSecurely";
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);
    private readonly HabitTrackerDbContext _dbContext;

    // Injeção de dependência para o contexto DB
    public IdentityController(HabitTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Endpoint para registrar um novo usuário
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegistrationRequest request)
    {
        // Verificar se os dados de email e senha foram passados
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Email and password are required.");

        // Verificar se o email já está em uso
        if (_dbContext.Users.Any(u => u.Email == request.Email))
            return Conflict("A user with this email already exists.");

        // Criar um novo usuário com a senha hasheada
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        // Adicionar o usuário ao banco de dados e salvar
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        // Retornar a criação com o Id do usuário
        return Created("", new { user.Id, user.Email, user.PasswordHash });
    }

    // Endpoint para fazer login e gerar um token JWT
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserRegistrationRequest request)
    {
        // Verificar se os dados de email e senha foram passados
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Email and password are required.");

        // Procurar usuário pelo email
        var user = _dbContext.Users.SingleOrDefault(u => u.Email == request.Email);

        // Se o usuário não existir ou a senha estiver incorreta
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");

        // Gerar o token para o usuário imediatamente após login bem-sucedido
        var tokenRequest = new TokenGenerationRequest
        {
            UserId = user.Id,
            Email = user.Email,
            CustomClaims = new Dictionary<string, object>
            {
                { "member", true } // Adicionar claims extras aqui, como o "member"
            }
        };

        // Chamar o método de geração de token
        var token = GenerateToken(tokenRequest);

        // Retornar o token gerado
        return Ok(token);
    }

    // Método para gerar o token JWT
    private string GenerateToken(TokenGenerationRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(TokenSecret);

        var claims = new List<Claim>
    {
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(JwtRegisteredClaimNames.Sub, request.Email),
        new(JwtRegisteredClaimNames.Email, request.Email),
        new("userid", request.UserId.ToString())
    };

        // Adicionar claims personalizados
        foreach (var claimPair in request.CustomClaims)
        {
            var valueType = claimPair.Value switch
            {
                bool boolValue => ClaimValueTypes.Boolean,
                double doubleValue => ClaimValueTypes.Double,
                string stringValue => ClaimValueTypes.String,
                _ => ClaimValueTypes.String // Default case, you can refine this based on other types
            };

            // Adiciona o claim corretamente
            var claim = new Claim(claimPair.Key, claimPair.Value.ToString(), valueType);
            claims.Add(claim);
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifetime),
            Issuer = "HabitTracker.Identity",
            Audience = "HabitTracker.API",
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token); // Retorna o JWT como string
    }
}