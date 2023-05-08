using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trazabilidad.Repositorio.Entidades;
using Trazabilidad.Repositorio.Interfaces;

namespace Trazabilidad.Repositorio
{
    public class TrazabilidadSeudoDatabase
    {
        public static List<Bloque> CadenaDeBloques = new List<Bloque>(0);   // se crea la lista de Bloques 

        public static List<Bloque> GetCadena()
        {
            return CadenaDeBloques;
        }
    }
}
