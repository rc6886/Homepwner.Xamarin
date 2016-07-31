using System;

namespace Homepwner.API.Features.Item.Models
{
    public class Item
    {
        public Item() { }

        public Item(Guid id, string name, string serialNumber, double value, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            SerialNumber = serialNumber;
            Value = value;
            DateCreated = dateCreated;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public double Value { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

