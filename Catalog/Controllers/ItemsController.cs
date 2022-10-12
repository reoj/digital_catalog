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
        private readonly IItemsRepo _repo;

        public ItemsController(IItemsRepo repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public IEnumerable<ItemDTO> GetItems()
        {
            return _repo.GetItems().Select( item => item.AsDto());
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDTO> GetItem(Guid id)
        {
            var foundItem = _repo.GetItem(id);
            return foundItem is null ? NotFound() : Ok(foundItem.AsDto());
        }
        [HttpPost]
        public ActionResult<ItemDTO> CreateItem (CreateItemDTO createItemDTO)
        {
            Item it = new(){
                Id = Guid.NewGuid(),
                Name = createItemDTO.Name,
                Price = createItemDTO.Price,
                CreatedTime = DateTimeOffset.UtcNow
            };

            _repo.CreateItem(it);

            return CreatedAtAction(nameof(GetItem), new {id = it.Id},it.AsDto());
        }
        [HttpPut("{id}")]
        public ActionResult UpdateItem (Guid id, UpdateItemDTO ItemDTO)
        {
            var exItem = _repo.GetItem(id);

            if (exItem is null)
            {
                return NotFound();
            }else
            {
                var updatedItem = exItem with{
                    Name = ItemDTO.Name,
                    Price = ItemDTO.Price
                };
                _repo.UpdateItem(updatedItem);

                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteItem (Guid id)
        {
            var exItem = _repo.GetItem(id);

            if (exItem is null)
            {
                return NotFound();
            }else
            {
                _repo.DeleteItem(id);
                return NoContent();
            }
        }
    }
}