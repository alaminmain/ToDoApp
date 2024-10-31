using Blazored.LocalStorage;
using ToDoApp.Model;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
    public class TaskService:ITaskService
    {
        private readonly ILocalStorageService _localStorageService;
        private const string TaskKey = "tasks";

        public TaskService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            var tasks = await _localStorageService.GetItemAsync<List<TaskItem>>(TaskKey);
            return tasks ?? new List<TaskItem>();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            var tasks = await GetAllTasksAsync();
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            var tasks = await GetAllTasksAsync();
            task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1; // Generate a new ID
            tasks.Add(task);
            await SaveTasksAsync(tasks);
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            var tasks = await GetAllTasksAsync();
            var existingTask = tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.DueDate = task.DueDate;
                existingTask.IsDone = task.IsDone;
                existingTask.Description = task.Description;
                await SaveTasksAsync(tasks);
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            var tasks = await GetAllTasksAsync();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                await SaveTasksAsync(tasks);
            }
        }

        public async Task SaveTasksAsync(List<TaskItem> tasks)
        {
            await _localStorageService.SetItemAsync(TaskKey, tasks);
        }
    }
    
    
}
