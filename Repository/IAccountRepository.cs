using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace BlogAPI.Repository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> Register(RegisterModel model);
        public Task<string> Login(LoginModel model);
        Task<IdentityResult> AddRoleAsync(string userID, string roleName);
        Task<IdentityResult> RemoveRoleAsync(string userID, string roleName);
    }
}