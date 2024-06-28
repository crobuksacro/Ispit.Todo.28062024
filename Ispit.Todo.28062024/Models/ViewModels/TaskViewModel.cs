namespace Ispit.Todo._28062024.Models.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public int? ToDoListId { get; set; }
    }

    public class ListTaskViewModel
    {
        public List<TaskViewModel> List { get; set; }
        public int? ToDoListId { get; set; }
    }


}
