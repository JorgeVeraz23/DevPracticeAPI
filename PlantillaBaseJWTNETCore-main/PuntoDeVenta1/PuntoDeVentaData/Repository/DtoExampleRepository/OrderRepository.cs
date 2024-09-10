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
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }

            if (_context == null)
            {
                throw new InvalidOperationException("DbContext is not initialized.");
            }

            var order = await _context.Orders
                .Include(o => o.Cliente)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                // Log or handle the fact that the order was not found
            }

            return order ?? new Order();

        }
    }
}
