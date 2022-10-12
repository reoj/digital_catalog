using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<Item> GetItems()
        {
            return _repo.GetItems();
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var foundItem = _repo.GetItem(id);
            return foundItem is null ? NotFound() : Ok(foundItem);
        }
    }
}