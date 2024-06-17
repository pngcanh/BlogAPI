using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Model;
using Microsoft.AspNetCore.Identity;

namespace BlogAPI.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> GetAll();
        Task<IdentityRole> GetByID(string id);
        Task<IdentityResult> Create(IdentityRole role);
        Task<IdentityResult> Delete(IdentityRole entity);
    }
}