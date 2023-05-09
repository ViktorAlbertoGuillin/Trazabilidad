using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trazabilidad.Repositorio.Entidades
{
    public class IntegridadBloque
    {
        public int Id { get; set; }
        public string Datos { get; set; }
        public string Hash { get; set; }
        public string Hash_anterior { get; set; }
        public DateTime Tiempo { get; set; }
        public Boolean Integro { get; set; }
    }
}
