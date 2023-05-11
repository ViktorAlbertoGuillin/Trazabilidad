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

        public void AgregarBloque(string dato)
        {
            _repositorio.GuardarBloque(dato);
        }

        /*public int ContadorBloque()
        {
            List<Bloque> lista = _repositorio.GetCadena();
            int cantidad = lista.Count();
            return cantidad;

        }*/

        /*public string UltimoHash()
        {
            List<Bloque> lista = _repositorio.GetCadena();
            string hash = lista.Last().Hash;
            return hash;
        }*/

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

        public string VerificarIntegridad()
        {
            string mensaje;
            List<Bloque> listaDatos = _repositorio.GetCadena();
            for (int i = 0; i < listaDatos.Count; i++)
            {
                Bloque bloque = listaDatos[i];
                if (i == 0)
                {
                    if (bloque.Hash_anterior != "0")
                    {
                        return "Falta el bloque de informacion inicial ";
                    }
                }
                else
                {
                    Bloque bloqueAnterior = listaDatos[i - 1];
                    if (bloque.Hash_anterior != bloqueAnterior.Hash)
                    {
                        return "Falta al menos un bloque de informacion";
                    }
                }
            }
            return "La cadena de bloque se encuentra completa";
        }
    }

    

   
}
