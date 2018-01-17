using System;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using MIAAPI.Models;
using MIAAPI.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace MIAAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/AuthToken")]
    public class AuthTokenController : Controller
    {
        [HttpPost]
        public object GetAuthToken([FromBody]AppUser appUser)
        {
            AppUserController appUserController = new AppUserController();

            var user = appUserController.Get(appUser.userId);

            if (user != null)
            {
                var requestAt = DateTime.Now;
                var expiresIn = requestAt + AuthTokenOption.ExpiresSpan;
                var token = GenerateToken(user, expiresIn);

                return new AuthTokenResult
                {
                    State = RequestState.Success,
                    Data = new
                    {
                        requertAt = requestAt,
                        expiresIn = AuthTokenOption.ExpiresSpan.TotalSeconds,
                        tokeyType = AuthTokenOption.TokenType,
                        accessToken = token
                    }
                };
            }
            else
            {
                return new AuthTokenResult
                {
                    State = RequestState.Failed,
                    Msg = "Login name or password is invalid."
                };
            }
        }

        [HttpGet]
        [Authorize("Bearer")]
        public object Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return new AuthTokenResult
            {
                State = RequestState.Success,
                Data = new { loginName = claimsIdentity.Name }
            };
        }

    private string GenerateToken(AppUser appUser, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(appUser.loginName, "TokenAuth"),
                new[] {
                    new Claim("ID", appUser.userId.ToString())
                }
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = AuthTokenOption.Issuer,
                Audience = AuthTokenOption.Audience,
                SigningCredentials = AuthTokenOption.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
            return handler.WriteToken(securityToken);
        }
    }
}