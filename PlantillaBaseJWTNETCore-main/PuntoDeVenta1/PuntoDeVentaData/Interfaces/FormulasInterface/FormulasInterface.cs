﻿using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.FormulasInterface
{
    public interface FormulasInterface
    {
        public Task<int> GenerarNumeroUnicoCuentaBancaria();
    }
}
