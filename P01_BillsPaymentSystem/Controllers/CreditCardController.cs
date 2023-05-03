using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ILogger<CreditCardController> _logger;
        private BillsPaymentSystemContext context;
        public CreditCardController(ILogger<CreditCardController> logger,
            BillsPaymentSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCard>>> GetAllCreditCardAsync()
        {
            try
            {
                var results = await context.creditCards.ToListAsync();
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
        public async Task<ActionResult<CreditCard>> GetCreditCardByIdAsync(int id)
        {
            try
            {
                var result = await context.creditCards.Where(e => e.CreditCardId == id).SingleOrDefaultAsync();
                if (result == null)
                {
                    _logger.LogInformation($"Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали з бази даних!");
                    return Ok(result);
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
        public async Task<ActionResult> PostCreditCardAsync([FromBody] CreditCard fullentity)
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
                var entity = new CreditCard()
                {
                    Limit = fullentity.Limit,
                    MoneyOwed = fullentity.MoneyOwed,
                    ExpirationDate = fullentity.ExpirationDate,
                };
                await context.creditCards.AddAsync(entity);
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
        public async Task<ActionResult> UpdateCreditCardAsync(int id, [FromBody] CreditCard updatedentity)
        {
            try
            {
                if (updatedentity == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("object is null");
                }

                var entity = await context.creditCards.Where(e => e.CreditCardId == id).SingleOrDefaultAsync();
                if (entity == null)
                {
                    _logger.LogInformation($"ID: {id} was not found in the database");
                    return NotFound();
                }
                entity.Limit = updatedentity.Limit;
                entity.MoneyOwed = updatedentity.MoneyOwed;
                entity.ExpirationDate = updatedentity.ExpirationDate;
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
        public async Task<ActionResult> DeleteCreditCardByIdAsync(int id)
        {
            try
            {
                var entity = await context.creditCards.Where(e => e.CreditCardId == id).SingleOrDefaultAsync();
                if (entity == null)
                {
                    _logger.LogInformation($"Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                context.creditCards.Remove(entity);
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
