using Microsoft.AspNetCore.Mvc;
using TaskTracker.Data;
using TaskTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        // GET: api/tasks/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem updatedTask)
        {

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)

                return NotFound();

            
            if (!string.IsNullOrEmpty(updatedTask.Title))
            {
                task.Title = updatedTask.Title;
            }

            if (!string.IsNullOrEmpty(updatedTask.Description))
            {
                task.Description = updatedTask.Description;
            }

            if (updatedTask.DueDate != null)
            {
                task.DueDate = updatedTask.DueDate;
            }

            if (!string.IsNullOrEmpty(updatedTask.Status))
            {
                task.Status = updatedTask.Status;
            }

            task.IsCompleted = updatedTask.IsCompleted; 

            if (!string.IsNullOrEmpty(updatedTask.AssignedTo))
            {
                task.AssignedTo = updatedTask.AssignedTo;
            }


           /* task.Description = updatedTask.Description;
            task.DueDate = updatedTask.DueDate;
            task.Status = updatedTask.Status;
            task.IsCompleted = updatedTask.IsCompleted;
            task.AssignedTo = updatedTask.AssignedTo;*/

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var total = await _context.Tasks.CountAsync();
            var completed = await _context.Tasks.CountAsync(t => t.IsCompleted);
            var pending = total - completed;

            var stats = new
            {
                Total = total,
                Completed = completed,
                Pending = pending
            };

            return Ok(stats);
        }
        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}