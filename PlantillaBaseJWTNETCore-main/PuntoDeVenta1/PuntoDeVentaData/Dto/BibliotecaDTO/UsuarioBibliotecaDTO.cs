using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.BibliotecaDTO
{
    public class UsuarioBibliotecaDTO
    {
        public long IdUsuario { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; } = "";
        [MaxLength(100)]
        public string Email { get; set; } = "";
    }
}
