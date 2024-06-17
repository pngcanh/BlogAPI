using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public Task<IdentityResult> Create(IdentityRole role)
        {
            return roleManager.CreateAsync(role);
        }
        public async Task<IdentityResult> Delete(IdentityRole role)
        {
            return await roleManager.DeleteAsync(role);
        }

        public async Task<IEnumerable<IdentityRole>> GetAll()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityRole> GetByID(string id)
        {
            return await roleManager.FindByIdAsync(id);
        }
    }


}