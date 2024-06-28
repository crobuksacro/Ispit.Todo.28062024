using AutoMapper;
using Ispit.Todo._28062024.Data;
using Ispit.Todo._28062024.Models.Dbo;
using Ispit.Todo._28062024.Models.ViewModels;
using Ispit.Todo._28062024.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ispit.Todo._28062024.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext db;
        private IMapper mapper;
        private SignInManager<ApplicationUser> signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, ApplicationDbContext db,
            IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.db = db;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApplicationUserViewModel> GetUserProfile(ClaimsPrincipal user)
        {
            var dbo = await db.Users
                  .Include(x => x.ToDoLists)
                  .ThenInclude(x => x.Tasks)
                  .FirstOrDefaultAsync(x => x.Id == userManager.GetUserId(user));

            //return new ApplicationUserViewModel
            //{
            //    Id = dbo.Id,
            //    FirstName = dbo.FirstName,
            //    LastName = dbo.LastName,
            //};

            return mapper.Map<ApplicationUserViewModel>(dbo);

        }
    }
}
