using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace Homepwner.Xamarin.iOS.Infrastructure.HTTP
{
    public interface IHomepwnerApi
    {
        [Post("/items")]
        [Headers("Accept: application/json")]
        Task<IEnumerable<Item>> GetAllItems();

        [Post("/item/update")]
        Task UpdateItem(Item item);
    }
}