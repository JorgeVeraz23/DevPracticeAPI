using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.DevPracticeEntities.CarritoDeComprasTutoEntities
{
    public class Productos : CrudEntities
    {
        [Key]
        public long IdProductos { get; set; }
        public string Nombre { get; set; }
        public long Precio { get; set; }
        public string Categoria { get; set; }

    }
}
