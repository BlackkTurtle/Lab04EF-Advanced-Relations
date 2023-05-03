using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private BillsPaymentSystemContext context;
        public UserController(ILogger<UserController> logger,
            BillsPaymentSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUserAsync()
        {
            try
            {
                var results = await context.users.ToListAsync();
                _logger.LogInformation($"Отримали всі дані з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("id/{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int id)
        {
            try
            {
                var entity = await context.users.Where(e => e.UserId == id).SingleOrDefaultAsync();
                if (entity == null)
                {
                    _logger.LogInformation($"Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали з бази даних!");
                    return Ok(entity);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events
        [HttpPost]
        public async Task<ActionResult> PostUserAsync([FromBody] User fullentity)
        {
            try
            {
                if (fullentity == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт є некоректним");
                }
                var entity = new User()
                {
                    FirstName = fullentity.FirstName,
                    LastName = fullentity.LastName,
                    Email = fullentity.Email,
                    Password = fullentity.Password
                };
                await context.users.AddAsync(entity);
                await context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(int id, [FromBody] User updatedentity)
        {
            try
            {
                if (updatedentity == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("object is null");
                }

                var entity = await context.users.Where(e => e.UserId == id).SingleOrDefaultAsync();
                if (entity == null)
                {
                    _logger.LogInformation($"ID: {id} was not found in the database");
                    return NotFound();
                }
                entity.FirstName = updatedentity.FirstName;
                entity.LastName = updatedentity.LastName;
                entity.Email = updatedentity.Email;
                entity.Password = updatedentity.Password;
                await context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in method - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        //GET: api/events/Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserByIdAsync(int id)
        {
            try
            {
                var entity = await context.users.Where(e => e.UserId == id).SingleOrDefaultAsync();
                if (entity == null)
                {
                    _logger.LogInformation($"Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                context.users.Remove(entity);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
