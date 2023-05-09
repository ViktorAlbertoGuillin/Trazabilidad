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

        public List<IntegridadBloque> Integridad()
        {
            List<Bloque> lista = new List<Bloque>();
            lista = _repositorio.GetCadena();
            List<IntegridadBloque> listaRevisada = new List<IntegridadBloque>();    
            for(int i=1; i < lista.Count();  i++  )
            {
                listaRevisada.Add(new IntegridadBloque { Id = lista[i].Id,Datos = lista[i].Datos, Hash = lista[i].Hash, Hash_anterior = lista[i].Hash_anterior, Tiempo = lista[i].Tiempo, (lista[i + 1].Hash_anterior == lista[i].Hash) ? listaRevisada[i].Integro = true : listaRevisada[i].Integro = true });
                if (lista[i + 1].Hash_anterior == lista[i].Hash )
                {
                    listaRevisada. = true;
                }
                else
                {
                    listaRevisada[i].Integro = true;
                }
            }
            return listaRevisada;
        }
    }
}
