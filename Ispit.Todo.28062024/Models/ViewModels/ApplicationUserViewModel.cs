using Ispit.Todo._28062024.Models.Dbo;

namespace Ispit.Todo._28062024.Models.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ToDoListViewModel> ToDoLists { get; set; }
    }
}
