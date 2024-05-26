using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.DevPracticesDTO.CarritoDeComprasDTO
{
    public class ProductoDTO
    {
        public long IdProductos { get; set; }
        public string Nombre { get; set; }
        public long Precio { get; set; }
        public string Categoria { get; set; }
    }
}
