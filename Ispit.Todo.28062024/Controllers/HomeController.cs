using Ispit.Todo._28062024.Models;
using Ispit.Todo._28062024.Models.Binding;
using Ispit.Todo._28062024.Models.ViewModels;
using Ispit.Todo._28062024.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ispit.Todo._28062024.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService accountService;

        public HomeController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> GetAllTasks(int id)
        {
            var tasks = await accountService.GetTasks(id);
            var model = new ListTaskViewModel { List = tasks, ToDoListId = id};

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> RegulateStatus(int id)
        {
            var tasks = await accountService.RegulateStatus(id);
            return RedirectToAction("GetAllTasks", new { id = tasks.ToDoListId });
        }


        [Authorize]
        public async Task<IActionResult> AddTask(int toDoListId)
        {           
            return View(new TaskBinding {ToDoListId = toDoListId });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTask(TaskBinding model)
        {
            await accountService.AddTask(model);
            return RedirectToAction("GetAllTasks",new {id = model.ToDoListId});
        }



        [Authorize]
        public async Task<IActionResult> AddTodoList()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTodoList(ToDoListBinding model)
        {
            await accountService.AddTodoList(model, User);
            return RedirectToAction("Profile");
        }


        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var profile = await accountService.GetUserProfile(User);
            return View(profile);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
