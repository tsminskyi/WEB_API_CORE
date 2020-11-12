using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WEB_API_CORE.DL;
using WEB_API_CORE.Hash;
using WEB_API_CORE.Models;

namespace WEB_API_CORE.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {

        // POST api/<loginController>
        [HttpPost]
        public JsonResult Post([FromBody] JObject value)
        {
            var pass = value["pass"].ToString();
            var login = value["username"].ToString();

            var identity = GetIdentity(login, pass);

            if (identity == null)
            {
                return new JsonResult(new ResponsModel()
                {
                    status = false,
                    description = "not found"
                });
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);



            return new JsonResult(new ResponsModel()
            {
                status = true,
                access_token = encodedJwt
            });
        }


        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var users = DBCarServiceDL_ADMIN.SearchAdmin(username);
            if (users != null)
            {


                if (users.password != GetCode.Hash(password))
                {
                    return null;
                }

                else
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, users.login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, users.role)
                };

                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token",
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                    return claimsIdentity;
                }

            }
            return null;
        }
    }


}
