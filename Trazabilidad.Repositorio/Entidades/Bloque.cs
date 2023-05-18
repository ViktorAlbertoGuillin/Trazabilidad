using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trazabilidad.Repositorio.Entidades
{
    public class Bloque
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Este Campo es obligatorio")]
        public string Datos { get; set; }
        public string Hash { get; set; }
        public string Hash_anterior { get; set; }
        public DateTime Tiempo { get; set; }
    }
}
