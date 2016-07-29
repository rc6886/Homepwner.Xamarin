using AutoMapper;
using Homepwner.API.Features.Item.Handlers;
using Homepwner.API.Features.Item.Models;

namespace Homepwner.API.AutoMapper
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile()
        {
            CreateMap<AddItemCommand, Item>();
        } 
    }
}