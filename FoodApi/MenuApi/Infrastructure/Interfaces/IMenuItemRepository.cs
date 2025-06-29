using MenuApi.Domain.Entities;
using MenuApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MenuApi.Infrastructure.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();

        Task<MenuItem> AddMenuItemAsync(MenuItem menuItem);

        Task<MenuItem> GetMenuItemByIdAsync(int menuId);

        Task<MenuItem> UpdateMenuItemByIdAsync(int menuId, [FromBody] MenuItem menuItem);

        Task DeleteMenuItemByIdAsync(int menuItemId);

        Task<bool> MenuItemExistsAsync(int id);
    }
}
