using AutoMapper;
using Data.Dto.DtoExampleDTO;
using Data.Entities.DtoExample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                
                config.CreateMap<Order, OrderDto>();
                config.CreateMap<Cliente, ClienteDto>();
                config.CreateMap<OrderDetail, OrderDetailDto>()
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            });
            return mappingConfig;
        }
    }
}
