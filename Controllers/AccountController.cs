using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(registerdto.Password))
                    return BadRequest("La password è obbligatoria.");

                if (string.IsNullOrEmpty(registerdto.Email))
                    return BadRequest("L'email è obbligatoria.");

                var appUser = new AppUser
                {
                    UserName = registerdto.Email,
                    Email = registerdto.Email,
                    Nome = registerdto.Nome ?? "N/A",
                    Cognome = registerdto.Cognome ?? "N/A",
                    numeroTelefono = registerdto.NumeroTelefono ?? "N/A",
                    Role = "User"
                };

                var result = await _userManager.CreateAsync(appUser, registerdto.Password);

                if (result.Succeeded)
                {
                    var addRoleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (!addRoleResult.Succeeded)
                    {
                        return StatusCode(500, "Errore durante l'assegnazione del ruolo: " + string.Join(", ", addRoleResult.Errors.Select(e => e.Description)));
                    }

                    return Ok(new NuovoUtenteDTO
                    {
                        Nome = appUser.Nome ?? "",
                        Cognome = appUser.Cognome ?? "",
                        Email = appUser.Email,
                        NumeroTelefono = appUser.numeroTelefono ?? "",
                        Token = _tokenService.CreateToken(appUser)
                    });
                }
                else if (result.Errors.Any(e => e.Code == "DuplicateUserName" || e.Code == "DuplicateEmail"))
                {
                    return Conflict("L'email è già in uso.");
                }
                else
                {
                    return BadRequest("Errore durante la registrazione: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore durante la registrazione: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email!);
            if (user == null)
            {
                return Unauthorized("Credenziali non valide.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password!);
            if (!isPasswordValid)
            {
                return Unauthorized("Credenziali non valide.");
            }

            return Ok(new NuovoUtenteDTO
            {
                Nome = user.Nome ?? "",
                Cognome = user.Cognome ?? "",
                Email = user.Email!,
                NumeroTelefono = user.numeroTelefono ?? "",
                Token = _tokenService.CreateToken(user)
            });
        }
}}