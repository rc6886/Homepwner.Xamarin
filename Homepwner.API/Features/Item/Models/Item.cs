using System;
using NPoco;

namespace Homepwner.API.Features.Item.Models
{
    public class Item
    {
        public Item() { }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public double Value { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

