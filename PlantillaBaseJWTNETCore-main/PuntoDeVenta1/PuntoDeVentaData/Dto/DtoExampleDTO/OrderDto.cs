using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.DtoExampleDTO
{
    public class OrderDto
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public ClienteDto Customer { get; set; }
        public List<OrderDetailDto>? OrderDetails { get; set; }
    }
}
