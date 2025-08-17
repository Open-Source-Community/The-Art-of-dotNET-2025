using Microsoft.AspNetCore.Mvc;
using Task_Manager.ApiService.DTOs;
using Task_Manager.Web.Data;

namespace Task_Manager.ApiService.Services
{
    public interface ITodoTaskService
    {
        public Task<int> CreateTodoTask(int userId, CreateTodoTaskDto task);
        public Task<TodoTask?> UpdateTodoTask(int userId, UpdateTodoTaskDto task);
        public Task<bool> DeleteTodoTask(int userId, int taskId);
        public Task<PaginatedResponseDto<TodoTask>> GetAllTodoTasks(
        int userId,
        bool? isDone,
        bool? order = false,
        bool? ascOrder = true,
        int page = 1,
        int pageSize = 10);
        public Task<IEnumerable<TodoTask>> GetAllTodoTasksForSomeUser(int userId, string? title);
        public Task<TodoTask?> GetById(int userId, bool isAdmin, int taskId);
    }
}
