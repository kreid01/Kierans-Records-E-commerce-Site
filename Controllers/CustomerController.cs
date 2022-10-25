using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordShop.Models;
using RecordShop.Store;

namespace RecordShop.Controllers

{

    [ApiController]
    [Route("/")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        [HttpGet]
        [Route("customer")]
        
        public async Task<IActionResult> Get()
        {
            var customer = await _customerRepository.GetCustomerAsync();
            return Ok(customer);
        }

            
        [HttpGet]
        [Route("customer/{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            return Ok(customer);
        }

        [HttpPut]
        [Route("customer")]
        public async Task<IActionResult> Put(Customer updateCustomer)
        {
            Customer customer = await _customerRepository.GetCustomerByIdAsync(updateCustomer.Id);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.UpdateCustomerAsync(updateCustomer);

            return NoContent();
        }

        [HttpPost]
        [Route("customer")]
        public async Task<IActionResult> Post(Customer newCustomer)
        {

            await _customerRepository.CreateNewCustomerAsync(newCustomer);

            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpDelete]
        [Route("customer")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.DeleteCustomerAsync(id);

            return NoContent();
        }

    }
}