using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.ExampleUseCallBackUseFetch
{
    public class Usuario
    {
        [Key]
        public long IdUsuario { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
    }
}
