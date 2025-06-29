using MenuApi.Domain.Entities;
using MenuApi.DTOs;
using MenuApi.Infrastructure.Data;
using MenuApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuApi.Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;
        public MenuItemRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<MenuItem> AddMenuItemAsync(MenuItem menuItem)
        {
            await _context.AddAsync(menuItem);

            await _context.SaveChangesAsync();

            return menuItem;
        }

        public async Task DeleteMenuItemByIdAsync(int menuId)
        {
            var menuItem = await _context.MenuItems.FindAsync(menuId);
            if (menuItem is not null)
            {
                _context.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
            return;
            
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _context.MenuItems
                .Include(mi => mi.Category)
                .ToListAsync();
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(int menuId)
        {
            var menuItem= await _context.MenuItems.Include(mi => mi.Category).FirstOrDefaultAsync(m=>m.Id==menuId);
            return menuItem;
        }
        public async Task<MenuItem> UpdateMenuItemByIdAsync(int menuId, [FromBody] MenuItem menuItem)
        {
            var menuIteminDb = await GetMenuItemByIdAsync(menuId);
            if (menuIteminDb is not null)
            {
                menuIteminDb.Price = menuItem.Price;
                menuIteminDb.Description = menuItem.Description;
                menuIteminDb.CategoryId = menuItem.CategoryId;
                menuIteminDb.Name= menuItem.Name;
                menuIteminDb.ImageUrl = menuItem.ImageUrl;
                menuIteminDb.IsAvailable= menuItem.IsAvailable;
                await _context.SaveChangesAsync();
            }

            return menuIteminDb;
        }


        public async Task<bool> MenuItemExistsAsync(int id)
        {
            return await _context.MenuItems.AnyAsync(e => e.Id == id);
        }
    }
}
