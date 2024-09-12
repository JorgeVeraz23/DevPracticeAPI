using Data.Dto.DtoExampleDTO;
using Data.Entities.DtoExample;
using Data.Interfaces.DtoExampleInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.DtoExampleRepository
{
    public class OrderRepository : OrderInterface
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Order> GetOrder(long id)
        {
            
            var order = await _context.Orders
                .Include(o => o.Cliente)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return null;
            }

            return order ?? new Order();

        }








        public async Task<OrderDto> GetOrderWithoutMapper(long id)
        {
            //Obtener la orden con los detalles y el cliente
            var order = await _context.Orders
                .Include(o => o.Cliente)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) { return null;  }
                

            //Convertir la entidad Order a OrderDto manualmente
            var orderDto = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                CustomerName = order.Cliente!.Name, //Convertir Cliente.Name a CustomerName
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ProductId = od.ProductId,
                    ProductName = od.Product.Name != null ? od.Product.Name : "Producto no disponible", // Manejar la posibilidad de un Product nulo
                    Quantity = od.Quantity,
                    TotalPrice = od.TotalPrice,
                }).ToList()
            };

            return orderDto;



        }
    }
}
