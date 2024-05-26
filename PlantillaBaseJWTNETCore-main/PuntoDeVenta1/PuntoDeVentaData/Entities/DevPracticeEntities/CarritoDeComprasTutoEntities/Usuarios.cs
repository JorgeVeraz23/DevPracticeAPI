﻿using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.DevPracticeEntities.CarritoDeComprasTutoEntities
{
    public  class Usuarios : CrudEntities
    {
        [Key]
        public long IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }


    }
}
