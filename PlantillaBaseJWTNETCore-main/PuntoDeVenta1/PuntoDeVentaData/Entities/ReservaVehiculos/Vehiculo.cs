﻿using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.ReservaVehiculos
{
    public class Vehiculo: CrudEntities
    {
        [Key]
        public long IdVehiculo { get; set; }
        [MaxLength(100)]
        public string Marca { get; set; } = "";
        [MaxLength(100)]
        public string Modelo { get; set; } = "";
        public int Anio { get; set; }
        public bool Disponible { get; set; }

    }
}