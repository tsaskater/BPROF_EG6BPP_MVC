using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.VM;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class AuthLogic
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _rolemanager;

        public AuthLogic(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolemanager)
        {
            _userManager = userManager;
            _rolemanager = rolemanager;
        }

        public async Task<string[]> RegisterUser(RegisterViewModel model)
        {
            var user2 = await _userManager.FindByEmailAsync(model.Email);

            if (user2 == null)
            {
                var guidId = Guid.NewGuid().ToString();

                var user = new IdentityUser
                {
                    Id = guidId,//update
                    Email = model.Email,
                    UserName = model.UserName,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                string psswrd = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, psswrd);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }

                string[] returnarray = new string[3];
                returnarray[0] = user.UserName;
                returnarray[1] = user.Email;
                returnarray[2] = psswrd;
                return returnarray;
            }

            throw new ArgumentException("Email Alredy Exists");
        }

        public async Task<TokenViewModel> LoginUser(LoginViewModel model)
        {
            IdentityUser user;
            bool validemail = IsValidEmail(model.ValidationName);

            if (validemail)
            {
                user = await _userManager.FindByEmailAsync(model.ValidationName);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.ValidationName);
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new List<Claim>
                {
                  new Claim(JwtRegisteredClaimNames.Sub, model.ValidationName),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

                var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes("Paris Berlin Cairo Sydney Tokyo Beijing Rome London Athens"));

                var token = new JwtSecurityToken(
                  issuer: "http://www.security.org",
                  audience: "http://www.security.org",
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(60),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
                return new TokenViewModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
            throw new ArgumentException("Login failed");
        }

        public IQueryable<IdentityUser> GetAllUser()
        {
            return _userManager.Users;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
