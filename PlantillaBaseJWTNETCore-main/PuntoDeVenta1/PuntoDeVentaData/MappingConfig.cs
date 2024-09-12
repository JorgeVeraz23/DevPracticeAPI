using AutoMapper;
using Data.Dto.DtoExampleDTO;
using Data.Entities.DtoExample;
using Data.Entities.UnitTest;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            try
            {
                CreateMap<Cliente, ClienteDto>();
                CreateMap<Order, OrderDto>()
                    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Cliente.Name));
                CreateMap<OrderDetail, OrderDetailDto>()
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            
            }catch (Exception ex)
            {
                throw new Exception($"Error al configurar AutoMapper: {ex.Message}");

            }
        }

    }
}
