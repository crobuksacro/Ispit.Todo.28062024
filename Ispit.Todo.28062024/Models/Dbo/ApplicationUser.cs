using Microsoft.AspNetCore.Identity;

namespace Ispit.Todo._28062024.Models.Dbo
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<ToDoList> ToDoLists { get; set; }

    }
}
