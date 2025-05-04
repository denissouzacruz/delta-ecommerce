﻿using Delta.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;
using Delta.Business.Models;
using System.Diagnostics.SymbolStore;
using System.Security.Claims;

namespace Delta.Api.Controllers
{
    [ApiController]
    [Route("api/conta")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager; 
        private readonly JwtSettings _jwtSettings;
        private readonly IVendedorRepository _vendedorRepository;

        public AuthController(SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IOptions<JwtSettings> jwtSettings,
                               IVendedorRepository vendedorRepository
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _vendedorRepository = vendedorRepository;
        }

        [HttpPost("registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(new ValidationProblemDetails(ModelState)
                { Title = "Ocorreu um erro ao cadastrar o usuário!" });

            var user = new IdentityUser()
            {
                UserName = registerUserViewModel.Email,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerUserViewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                await _vendedorRepository.Adicionar(new Business.Models.Vendedor()
                {
                    Id = Guid.Parse(user.Id),
                    Nome = registerUserViewModel.Name,
                    Email = registerUserViewModel.Email
                });

                return Ok(await GerarJwt(registerUserViewModel.Email));
            }

            var errors = result.Errors
                        .GroupBy(e => e.Code) 
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.Description).ToArray()
                        );

            return ValidationProblem(new ValidationProblemDetails(errors)
            { Title = "Ocorreu um erro ao cadastrar o usuário!" });
        
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(new ValidationProblemDetails(ModelState)
                    {Title = "Ocorreu um erro ao cadastrar o usuário!"});

            var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.Email, userLoginViewModel.Password, false, true);
            if (result.Succeeded)
            {
                return Ok(await GerarJwt(userLoginViewModel.Email));
            }

            return Problem("Usuário ou senha inválidos!");
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = _userManager.FindByEmailAsync(email);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Result.Id)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

            var token = tokenHandler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor() 
            { 
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;    
        }
    }
}
