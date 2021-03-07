using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using GreenHealthApi.Endpoints.Customers;
using GreenHealthApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Authentication
{
    [EnableCors]
    [Route("api/v1/auth/")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly ICustomerReader _usersReader;
        
        public Controller(ICustomerReader usersReader)
        {
            _usersReader = usersReader;
        }
        
        [HttpPost]
        [Route("token/")]
        public async Task<IActionResult> Token(Person model)
        {
            try
            {
                var phone = model.Phone;
                var password = model.Password;
                if (String.IsNullOrEmpty(phone) || String.IsNullOrEmpty(password))
                {
                    return BadRequest("Invalid username or password.");
                }
                var user = await _usersReader.GetIdentityUserAsync(phone, password);
                ClaimsIdentity identity = null;
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Phone),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                        new Claim(ClaimTypes.Role, user.Role)
                    };
                    identity =
                        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                            ClaimsIdentity.DefaultRoleClaimType);
                }
                
                if (identity == null)
                {
                    return BadRequest("Invalid username or password.");
                }

                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                        SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    fullName = user.FullName,
                    phone = user.Phone,
                    email = user.Email,
                    login = user.Login,
                    city = user.City,
                    address = user.Address,
                    role = user.Role
                };

                return Created("token",
                    JsonConvert.SerializeObject(response,
                        new JsonSerializerSettings {Formatting = Formatting.Indented}));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}