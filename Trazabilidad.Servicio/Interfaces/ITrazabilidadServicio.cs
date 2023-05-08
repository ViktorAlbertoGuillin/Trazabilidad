using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trazabilidad.Repositorio.Entidades;

namespace Trazabilidad.Servicio.Interfaces
{
    public interface ITrazabilidadServicio
    {
        void AgregarBloque(Bloque bloque);
        int ContadorBloque();
        string UltimoHash();
        List<Bloque> ListarBloques();
        Bloque BuscarBloquePorId(int Id);
        void ModificarBloque(Bloque bloqueEditado);
        void EliminarBloque(int Id);
    }
}
