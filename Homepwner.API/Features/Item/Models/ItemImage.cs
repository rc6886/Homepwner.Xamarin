using System;

namespace Homepwner.API.Features.Item.Models
{
    public class ItemImage
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}