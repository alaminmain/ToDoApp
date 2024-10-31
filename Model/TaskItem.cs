namespace ToDoApp.Model
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Today;
        public bool IsDone { get; set; }
        public string Description { get; set; }
    }
}
