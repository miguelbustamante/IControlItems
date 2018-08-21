using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IControlItems.Interfaces;
using IControlItems.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IControlItems.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {

        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Item>> Get()
        {
            return await _itemRepository.GetAllItems();
        }

        [HttpGet("{id}")]
        public async Task<Item> Get(string id)
        {
            return await _itemRepository.GetItem(id) ?? new Item();
        }

        [HttpPost]
        public void Post([FromBody] ItemParams newItem)
        {
            _itemRepository.AddItem(new Item {
                Id = newItem.Id,
                Name = newItem.Name,
                Tipo = newItem.Tipo
            });

        }

        [HttpPut("id")]
        public void Put(string id, [FromBody] ItemParams item)
        {
            _itemRepository.UpdateItem(id, new Item {
                Id = item.Id,
                Name = item.Name,
                Tipo = item.Tipo
            });
        }

        [HttpDelete]
        public void Delete(string id)
        {
            _itemRepository.RemoteItem(id);
        }
    }
}
