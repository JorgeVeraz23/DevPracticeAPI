using Data.Interfaces.FormulasInterface;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.FormulaRepository
{
    public class FormulaRepository : FormulasInterface
    {
        private readonly Random _random;

        public FormulaRepository()
        {
            _random = new Random();
        }

        public async Task<int> GenerarNumeroUnicoCuentaBancaria()
        {
            return await Task.Run(() =>
            {

                //Genera un numero de 9 digitos para asegurarnos que no haya problemas con los limites de int
                int numeroCuentaBase = _random.Next(100000000, 100000000);
                //Asegurar que el numero sea de 10 digitos añadiendo un digito al frente.
                return numeroCuentaBase;
            });
        }
    }
}
