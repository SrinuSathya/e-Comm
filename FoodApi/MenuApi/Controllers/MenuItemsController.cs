using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuApi.Domain.Entities;
using MenuApi.Infrastructure.Data;
using MenuApi.Infrastructure.Interfaces;
using AutoMapper;
using MenuApi.DTOs;

namespace MenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;


        public MenuItemsController(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;  
        }

        // GET: api/MenuItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetMenuItems()
        {
            var menuItemResponse = await _menuItemRepository.GetAllMenuItemsAsync();
            return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(menuItemResponse));
            
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> AddMenuItem(CreateOrUpdateMenuItemDto menuItem)
        {
            var createmenuItemDomain = _mapper.Map<MenuItem>(menuItem);
            try
            {
                var addMenuItemResponse = await _menuItemRepository.AddMenuItemAsync(createmenuItemDomain);
                return Ok(_mapper.Map<MenuItemDto>(addMenuItemResponse));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetMenuItem([FromRoute] int id)
        {

            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(id);
            if (menuItem is not null)
            {
                return Ok(_mapper.Map<MenuItemDto>(menuItem));
            }
            return NotFound($"MenuItem with ItemId : {id} does not exists");
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItemDto>> UpdateMenuItemById([FromRoute] int id, [FromBody] CreateOrUpdateMenuItemDto menuItem)
        {
            var updatedMenuItem = await _menuItemRepository.UpdateMenuItemByIdAsync(id, _mapper.Map<MenuItem>(menuItem));
            if (updatedMenuItem is not null)
            {
                return Ok(_mapper.Map<MenuItemDto>(updatedMenuItem));
            }
            return NotFound($"MenuItem with ItemId : {id} does not exists");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenuItemById([FromRoute] int id)
        {
            if (!await _menuItemRepository.MenuItemExistsAsync(id))
            {
                return NotFound($"MenuItem with ItemId : {id} does not exists");
            }
            await _menuItemRepository.DeleteMenuItemByIdAsync(id);
            return Ok();

        }


        /*
        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return menuItem;
        }

        // PUT: api/MenuItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItem(int id, MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(menuItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MenuItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
        */
    }
}
