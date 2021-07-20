using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;
using System;
using Catalog.Dtos;
using System.Linq;


namespace Catalog.Controllers
{


    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {

        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {

            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {

            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item.AsDto());

        }

        [HttpPost]

        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new Item
            {
                Id = Guid.NewGuid(),
                Price = itemDto.Price,
                Name = itemDto.Name,
                CreatedDate = DateTimeOffset.Now

            };

            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item item = existingItem with
            {
                Price = itemDto.Price,
                Name = itemDto.Name
            };

            repository.UpdateItem(item);
            return NoContent();
        }

    }
}