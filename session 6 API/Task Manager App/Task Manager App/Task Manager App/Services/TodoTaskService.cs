using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using Task_Manager.ApiService.DTOs;
using Task_Manager.Web.Data;

namespace Task_Manager.ApiService.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public TodoTaskService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        async Task<int> ITodoTaskService.CreateTodoTask(int userId, CreateTodoTaskDto taskDto)
        {
            TodoTask newTask = _mapper.Map<TodoTask>(taskDto);

            newTask.CreatorId = userId;

            _appDbContext.TodoTasks.Add(newTask);

            await _appDbContext.SaveChangesAsync();

            return newTask.Id;
        }
        async Task<TodoTask?> ITodoTaskService.UpdateTodoTask(int userId, UpdateTodoTaskDto taskDto)
        {
            var destination = await _appDbContext.TodoTasks.FindAsync(taskDto.Id);

            if (destination == null || userId != destination.CreatorId)
                return null;

            _mapper.Map(taskDto, destination);

            _appDbContext.TodoTasks.Update(destination);

            await _appDbContext.SaveChangesAsync();

            return destination;
        }

        async Task<bool> ITodoTaskService.DeleteTodoTask(int userId, int taskId)
        {
            var destination = await _appDbContext.TodoTasks.FindAsync(taskId);

            if (destination == null || destination.CreatorId != userId)
                return false;

            _appDbContext.TodoTasks.Remove(destination);

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        async Task<PaginatedResponseDto<TodoTask>> ITodoTaskService.GetAllTodoTasks(int userId, bool? isDone, bool? order, bool? ascOrder, int page, int pageSize)
        {
            var query = _appDbContext.TodoTasks.Where(x => x.CreatorId == userId).AsQueryable();

            // Filtering
            if (isDone.HasValue)
            {
                query = query.Where(t => t.IsDone == isDone.Value);
            }

            // Sorting
            if (order == true)
            {
                query = ascOrder == true ?
                    query.OrderBy(t => t.Score)
                    : query.OrderByDescending(t => t.Score);
            }

            // Pagination
            var totalItems = await query.CountAsync();
            var results = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Return paginated results with metadata
            return new PaginatedResponseDto<TodoTask> {
                Items = results,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize
            };
        }

        async Task<IEnumerable<TodoTask>> ITodoTaskService.GetAllTodoTasksForSomeUser(int userId, string? title)
        {
            if (title == null)
                return await _appDbContext.TodoTasks.Where(x => x.CreatorId == userId).ToListAsync();
            else
                return await _appDbContext.TodoTasks.Where(x => x.Title == title && x.CreatorId == userId).ToListAsync();
        }

        async Task<TodoTask?> ITodoTaskService.GetById(int userId, bool isAdmin, int taskId)
        {
            var task = await _appDbContext.TodoTasks.FindAsync(taskId);
            
            if (task == null)
                return null;

            if (!isAdmin && task.CreatorId != userId)
                return null; 

            return task;
        }
    }
}
