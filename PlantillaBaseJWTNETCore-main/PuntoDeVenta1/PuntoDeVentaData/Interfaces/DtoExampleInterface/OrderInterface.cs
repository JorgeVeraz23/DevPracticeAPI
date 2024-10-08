﻿using Data.Dto.DtoExampleDTO;
using Data.Entities.DtoExample;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.DtoExampleInterface
{
    public interface OrderInterface
    {
        public Task<Order> GetOrder(long id);
        public Task<OrderDto> GetOrderWithoutMapper(long id);

    }
}
