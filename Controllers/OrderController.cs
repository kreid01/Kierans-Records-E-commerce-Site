using Microsoft.AspNetCore.Mvc;
using RecordShop.Models;
using RecordShop.Store;

namespace RecordShop.Controllers

{

    [ApiController]
    [Route("/")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        [Route("order")]
        public async Task<IActionResult> Get()
        {
            var order = await _orderRepository.GetOrderAsync();
            return Ok(order);
        }

        [HttpGet]
        [Route("order/{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpGet]
        [Route("customers/orders/{id}")]
        public async Task<IActionResult> GetOrdersOfCustomer(string id)
        {
            var orders = await _orderRepository.GetAllOrdersByIdAsync(id);
            return Ok(orders);
        }


        [HttpPut]
        [Route("order")]
        public async Task<IActionResult> Put(Order orderToUpdate)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(orderToUpdate.Id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.UpdateOrderAsync(orderToUpdate);

            return NoContent();
        }

        [HttpPost]
        [Route("order")]
        public async Task<IActionResult> Post(Order newOrder)
        {

            await _orderRepository.CreateNewOrderAsync(newOrder);

            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }

        [HttpDelete]
        [Route("order")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteOrderAsync(id);

            return NoContent();
        }

    }
}