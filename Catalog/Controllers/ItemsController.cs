using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DTOs;
using Catalog.Models;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Catalog.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController : ControllerBase
    {
        #region Singleton
        private readonly IItemsRepo _repo;
        #endregion

        #region Constructor
        public ItemsController(IItemsRepo repo)
        {
            this._repo = repo;
        }
        #endregion

        #region HTTP Methods
        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
        {
            return (await _repo.GetItemsAsync()).Select( item => item.AsDto());
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<ItemDTO>> GetItemAsync(Guid id)
        {
            var foundItem = await _repo.GetItemAsync(id);
            return foundItem is null ? NotFound() : Ok(foundItem.AsDto());
        }
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItemAsync (CreateItemDTO createItemDTO)
        {
            Item it = new(){
                Id = Guid.NewGuid(),
                Name = createItemDTO.Name,
                Price = createItemDTO.Price,
                CreatedTime = DateTimeOffset.UtcNow
            };

            await _repo.CreateItemAsync(it);

            return CreatedAtAction(nameof(GetItemAsync), new {id = it.Id},it.AsDto());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync (Guid id, UpdateItemDTO ItemDTO)
        {
            var exItem = await _repo.GetItemAsync(id);

            if (exItem is null)
            {
                return NotFound();
            }else
            {
                var updatedItem = exItem with{
                    Name = ItemDTO.Name,
                    Price = ItemDTO.Price
                };
                await _repo.UpdateItemAsync(updatedItem);

                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem (Guid id)
        {
            var exItem = await _repo.GetItemAsync(id);

            if (exItem is null)
            {
                return NotFound();
            }else
            {
                await _repo.DeleteItemAsync(id);
                return NoContent();
            }
        }
        #endregion
    }
}