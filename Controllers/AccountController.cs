using BuyWebPage.Data;
using BuyWebPage.Data.Entities;
using BuyWebPage.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuyWebPage.Controllers
{
    public class AccountController : Controller
    {
        private ILogger<AccountController> Loger;
        private readonly SignInManager<Users> manager;
        private readonly UserManager<Users> _Manager;
        private readonly IConfiguration _Config;
        private readonly IBuyRepository Context;
        private DateTime expirationtoken = new DateTime();
      

        public AccountController(ILogger<AccountController> Loger, SignInManager<Users> Manager, UserManager<Users> _manager, IConfiguration _config,
            IBuyRepository context)
        {
            this.Loger = Loger;
            manager = Manager;
            _Manager = _manager;
            _Config = _config;
            Context = context;
        }



        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel Register)
        {
            if (ModelState.IsValid)
            {
                var user = await _Manager.FindByEmailAsync(Register.Email);

                if (user == null)
                {
                    user = new Users()
                    {
                        FirstName = Register.FirstName,
                        LastName = Register.LastName,
                        Email = Register.Email,
                        UserName = Register.UserName,

                    };
                    var registerUser = await _Manager.CreateAsync(user, Register.Password);

                    if (registerUser != IdentityResult.Success)
                    {
                        ModelState.AddModelError("", "failed to create the user");
                        //return BadRequest();
                    }
                    else
                    {
                        var result = await manager.PasswordSignInAsync(Register.UserName, Register.Password, false, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Shop", "App");
                        }
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await manager.PasswordSignInAsync(model.Username, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("Shop", "App");
                    }
                }

            }
            ModelState.AddModelError("", "Failed to Login");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await manager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }
        [HttpGet]
        public IActionResult IsAuthenthicated(LoginViewModel model)
        {
            bool auth= false;
            //var user = await _Manager.FindByEmailAsync(model.Username);
         
            if (this.User.Identity.IsAuthenticated &&  this.expirationtoken.TimeOfDay < DateTime.UtcNow.TimeOfDay)
            {
                return Ok(auth);
            }
            return Ok(auth);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _Manager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result = await manager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };
                        //los campos del JSON DEBEN EMPEZAR EN MAYUSCULA EJEMPLO "Tokens: Key"
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["Tokens:Key"]));

                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            //los campos del JSON DEBEN EMPEZAR EN MAYUSCULA EJEMPLO "Tokens: Issuer"
                            _Config["Tokens:Issuer"],
                            _Config["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(15),
                            signingCredentials: credentials
                        );
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                        };

                        await Login(model);
                        this.expirationtoken = results.expiration;
                        return Created("", results);


                    }
                    return BadRequest("you have to authenthicate");

                }
            }
            return BadRequest("failed to generate token");

        }
    }
}
