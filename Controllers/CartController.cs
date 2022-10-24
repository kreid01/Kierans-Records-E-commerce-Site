using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordShop.Models;
using RecordShop.Services;
using RecordShop.Store;

namespace RecordShop.Controllers

{

    [ApiController]
    [Route("/")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IRecordFilterService _filterService;
        public CartController( ICartRepository cartRepository, IRecordFilterService recordFilter)
        {
            _cartRepository = cartRepository;
            _filterService = recordFilter;
        }
        [HttpGet]
        [Route("cart")]
        public async Task<IActionResult> Get()
        {
            var cart = await _cartRepository.GetCartAsync();
            return Ok(cart);
        }

        [HttpGet]
        [Route("cart/{id}")]
        public async Task<IActionResult> GetCartById(string id)
        {
            var cart = await _cartRepository.GetCartByIdAsync(id);
            return Ok(cart);
        }

        [HttpPut]
        [Route("cart")]
        public async Task<IActionResult> Put(Cart updateCart)
        {
            Cart cart = await _cartRepository.GetCartByIdAsync(updateCart.Id);
            if (cart == null)
            {
                return NotFound();
            }

            await _cartRepository.UpdateCartAsync(updateCart);

            return NoContent();
        }
        
        [HttpPost]
        [Route("cart")]
        public async Task<IActionResult> Post(Cart newCart)
        {
            
            await _cartRepository.CreateNewCartAsync(newCart);

            return CreatedAtAction(nameof(Get), new { id = newCart.Id }, newCart);
        }
        
        [HttpDelete]
        [Route("cart")]
        public async Task<IActionResult> Delete(string id)
        {
            var cart = await _cartRepository.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            await _cartRepository.DeleteCartAsync(id);

            return NoContent();
        }

    }
}
