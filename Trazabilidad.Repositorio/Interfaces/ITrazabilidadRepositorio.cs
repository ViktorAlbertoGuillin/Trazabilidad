using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trazabilidad.Repositorio.Entidades;

namespace Trazabilidad.Repositorio.Interfaces
{
    public interface ITrazabilidadRepositorio
    {
        void GuardarBloque(Bloque bloque);
        List<Bloque> GetCadena();
        Bloque BuscarBloquePorId(int id);
        void ModificarBloque(Bloque bloqueEditado);
        void EliminarBloque(int Id);
    }
}
