using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BlogAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<BlogUser> userManager;
        private readonly SignInManager<BlogUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepository(UserManager<BlogUser> userManager, SignInManager<BlogUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }

        public async Task<string> Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            var checkPass = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (user == null || !checkPass.Succeeded)
            {
                return string.Empty;
            }

            //generate token:
            var userRole = await userManager.GetRolesAsync(user);
            var authClaim = new List<Claim> // tạo claim chứa thong tin người dùng
            {
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            foreach (var role in userRole)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            //ma hoa key
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])); // lay secretKey trong appsetting

            //tao token
            var tokenDiscreptor = new SecurityTokenDescriptor
            {
                // danh sach cac claim
                Subject = new ClaimsIdentity(authClaim),
                // thoi gian het han cua token
                Expires = DateTime.Now.AddMinutes(20),
                Issuer = configuration["JWT:ValidIssuer"],
                Audience = configuration["JWT:ValidAudience"],
                // ký vào signinkey
                SigningCredentials = new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            };
            //sinh ra chuỗi token theo các thông số trên
            var token = new JwtSecurityTokenHandler().CreateToken(tokenDiscreptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = new BlogUser
            {
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "user");
            }
            return result;
        }

        public async Task<IdentityResult> AddRoleAsync(string userID, string roleName)
        {
            var roleExist = await roleManager.FindByNameAsync(roleName);
            if (roleExist == null)
            {
                return IdentityResult.Failed();
            }
            var userExist = await userManager.FindByIdAsync(userID);
            if (userExist == null)
            {
                return IdentityResult.Failed();
            }
            var result = await userManager.AddToRoleAsync(userExist, roleName);
            return result;
        }
        public async Task<IdentityResult> RemoveRoleAsync(string userID, string roleName)
        {
            var user = await userManager.FindByIdAsync(userID);
            var roleExit = await userManager.GetRolesAsync(user);
            if (!roleExit.Contains(roleName))
            {
                return IdentityResult.Failed();
            }
            var result = await userManager.RemoveFromRolesAsync(user, roleExit);
            if (result.Succeeded)
            {
                return IdentityResult.Failed();
            }
            return result;
        }
    }
}