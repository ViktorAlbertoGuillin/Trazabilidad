using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Trazabilidad.Repositorio.Entidades;
using Trazabilidad.Repositorio.Interfaces;
using Trazabilidad.Servicio.Interfaces;

namespace Trazabilidad.Servicio
{
    public class TrazabilidadServicio : ITrazabilidadServicio
    {
        readonly ITrazabilidadRepositorio _repositorio;

        public TrazabilidadServicio(ITrazabilidadRepositorio repositorio)    // importantisimo tener este constructor para enlazar con el repositorio
        {
            _repositorio = repositorio;
        }

        public void AgregarBloque(Bloque bloque)
        {
            _repositorio.GuardarBloque(bloque);
        }

        public int ContadorBloque()
        {
            List<Bloque> lista = _repositorio.GetCadena();
            int cantidad = lista.Count();
            return cantidad;

        }

        public string UltimoHash()
        {
            List<Bloque> lista = _repositorio.GetCadena();
            string hash = lista.Last().Hash;
            return hash;
        }

        public List<Bloque> ListarBloques()
        {
            return _repositorio.GetCadena();
        }

        public Bloque BuscarBloquePorId(int Id)
        {
            return _repositorio.BuscarBloquePorId(Id);
        }

        public void ModificarBloque(Bloque bloqueEditado)
        {
            _repositorio.ModificarBloque(bloqueEditado);
        }
        public void EliminarBloque(int Id)
        {
            _repositorio.EliminarBloque(Id);
        }
    }
}
