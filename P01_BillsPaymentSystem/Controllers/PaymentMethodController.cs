using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly ILogger<PaymentMethodController> _logger;
        private BillsPaymentSystemContext context;
        public PaymentMethodController(ILogger<PaymentMethodController> logger,
            BillsPaymentSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetAllPaymentMethodAsync()
        {
            try
            {
                var results = await context.paymentMethods.ToListAsync();
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
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethodByIdAsync(int id)
        {
            try
            {
                var entity = await context.paymentMethods.Where(e => e.Id == id).SingleOrDefaultAsync();
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
        public async Task<ActionResult> PostPaymentMethodAsync([FromBody] PaymentMethod fullentity)
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
                var entity = new PaymentMethod()
                {
                    UserId=fullentity.UserId,
                    BankAccountId=fullentity.BankAccountId,
                    CreditCardId=fullentity.CreditCardId
                };
                await context.paymentMethods.AddAsync(entity);
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
        public async Task<ActionResult> UpdatePaymentMethodAsync(int id, [FromBody] PaymentMethod updatedentity)
        {
            try
            {
                if (updatedentity == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("object is null");
                }

                var entity = await context.paymentMethods.Where(e => e.Id == id).SingleOrDefaultAsync();
                if (entity == null)
                {
                    _logger.LogInformation($"ID: {id} was not found in the database");
                    return NotFound();
                }
                entity.UserId = updatedentity.UserId;
                entity.BankAccountId = updatedentity.BankAccountId;
                entity.CreditCardId = updatedentity.CreditCardId;
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
        public async Task<ActionResult> DeletePaymentMethodByIdAsync(int id)
        {
            try
            {
                var entity = await context.paymentMethods.Where(e => e.Id == id).SingleOrDefaultAsync();
                if (entity == null)
                {
                    _logger.LogInformation($"Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                context.paymentMethods.Remove(entity);
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
