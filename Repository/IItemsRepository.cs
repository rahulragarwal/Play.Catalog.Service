using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item entity);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetItemAsync(Guid id);
        Task RemoveAsync(Guid Id);
        Task UpdateAsync(Item entity);
    }
}