using Ispit.Todo._28062024.Models.Binding;
using Ispit.Todo._28062024.Models.ViewModels;
using System.Security.Claims;

namespace Ispit.Todo._28062024.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUserViewModel> GetUserProfile(ClaimsPrincipal user);
        /// <summary>
        /// Add todl list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ToDoListViewModel> AddTodoList(ToDoListBinding model, ClaimsPrincipal user);
        /// <summary>
        /// Regulate status of task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<TaskViewModel> RegulateStatus(int taskId);


        /// <summary>
        /// Add task to todo list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TaskViewModel> AddTask(TaskBinding model);

        /// <summary>
        /// Get task by todo list id
        /// </summary>
        /// <param name="todoListId"></param>
        /// <returns></returns>
        Task<List<TaskViewModel>> GetTasks(int todoListId);
    }
}