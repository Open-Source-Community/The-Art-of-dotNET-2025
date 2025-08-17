using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;
using System.Security.Claims;
using Task_Manager.ApiService.DTOs;
using Task_Manager.ApiService.Services;
using Task_Manager.Web.Data;

namespace Task_Manager.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTasksController : ControllerBase
    {
        private ITodoTaskService _todoTaskService;
        private readonly IMapper _mapper;
        public TodoTasksController(ITodoTaskService todoTaskService, IMapper mapper)
        {
            _todoTaskService = todoTaskService;
            _mapper = mapper;
        }
        
        [HttpPost] // C
        [Route("")]
        [Authorize]
        public async Task<ActionResult<int>> CreateTodoTask(CreateTodoTaskDto taskDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var newTaskId = await _todoTaskService.CreateTodoTask(userId, taskDto);
            
            return Ok(newTaskId);
        }

        [HttpPut] // U
        [Route("")]
        [Authorize]
        public async Task<ActionResult<int>> UpdateTodoTask(UpdateTodoTaskDto taskDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var ret = await _todoTaskService.UpdateTodoTask(userId, taskDto);
            
            if (ret == null)
                return NotFound();
           
            return Ok(ret);
        }

        [HttpDelete] // D
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<int>> DeleteTodoTask(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var deleted = await _todoTaskService.DeleteTodoTask(userId, id);

            if(!deleted)
                return NotFound();

            return Ok();
        }
        
        
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<PaginatedResponseDto<TodoTask>>> GetAllTodoTasks(
        [FromQuery] bool? isDone,
        [FromQuery] bool? order = false,
        [FromQuery] bool? ascOrder = true,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var ret = await _todoTaskService.GetAllTodoTasks(userId, isDone, order, ascOrder, page, pageSize);

            return Ok(ret);
        }

        [SwaggerOperation(Summary = "Retrieves all todo tasks",
            Description = "Returns a list of all tasks in the system. " +
            "Requires JWT authentication.")]
        [HttpGet]
        [Route("user/{id}")]
        [Authorize(Roles = "AppAdmin")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetAllTodoTasksForSomeUser(int userId, string? title)
        {
            var record = await _todoTaskService.GetAllTodoTasksForSomeUser(userId, title);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
         

        [HttpGet] // R
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<TodoTask>> GetById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var isAdmin = User.IsInRole("AppAdmin");

            var task = await _todoTaskService.GetById(userId, isAdmin, id);
            
            if (task == null)
                return NotFound();

            return Ok(task);
        }
    }



}
