using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace Homepwner.Xamarin.iOS.Infrastructure.HTTP
{
    public interface IHomepwnerApi
    {
        [Get("/item/getimage/{itemId}")]
        [Headers("Accept: application/octet-stream")]
        Task<HttpContent> GetItemImage(Guid itemId);

        [Post("/items")]
        [Headers("Accept: application/json")]
        Task<IEnumerable<Item>> GetAllItems();

        [Post("/item/update")]
        Task UpdateItem(Item item);
    }
}