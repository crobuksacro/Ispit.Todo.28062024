using AutoMapper;
using Ispit.Todo._28062024.Data;
using Ispit.Todo._28062024.Models.Binding;
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
        /// Regulate status of task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<TaskViewModel> RegulateStatus(int taskId)
        {
            var dbo = db.Tasks.FirstOrDefault(x => x.Id == taskId);
            dbo.Status = false;
            await db.SaveChangesAsync();
            return mapper.Map<TaskViewModel>(dbo);
        }


        /// <summary>
        /// Add task to todo list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TaskViewModel> AddTask(TaskBinding model)
        {
            var dbo = mapper.Map<Models.Dbo.Task>(model);
            dbo.Status = true;
            await db.Tasks.AddAsync(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<TaskViewModel>(dbo);
        }

        /// <summary>
        /// Get task by todo list id
        /// </summary>
        /// <param name="todoListId"></param>
        /// <returns></returns>
        public async Task<List<TaskViewModel>> GetTasks(int todoListId)
        {
            var dbos = await db.Tasks
                .Include(x => x.ToDoList)
                .Where(x => x.ToDoListId == todoListId && x.Status).ToListAsync();

            return dbos.Select(x => mapper.Map<TaskViewModel>(x)).ToList();

        }


        /// <summary>
        /// Add todl list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ToDoListViewModel> AddTodoList(ToDoListBinding model, ClaimsPrincipal user)
        {
            var dbo = mapper.Map<ToDoList>(model);
            dbo.ApplicationUserId = userManager.GetUserId(user);
            await db.ToDoLists.AddAsync(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ToDoListViewModel>(dbo);
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
