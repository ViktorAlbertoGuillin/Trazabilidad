using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Trazabilidad.Repositorio.Entidades;
using Trazabilidad.Repositorio.Interfaces;

namespace Trazabilidad.Repositorio
{
    public class TrazabilidadRepositorio : ITrazabilidadRepositorio
    {
        public void GuardarBloque(Bloque bloque)
        {
            int? idUltimo = ObtenerUltimoId();
            idUltimo++;
            bloque.Id = (int)idUltimo;
            bloque.Hash = CalcularHash(bloque);
            TrazabilidadSeudoDatabase.GetCadena().Add(bloque);
        }

        private int ObtenerUltimoId()
        {
            try
            {
                if(TrazabilidadSeudoDatabase.GetCadena().Count()== 0)
                {
                    return 0;
                }
                else 
                {
                    return TrazabilidadSeudoDatabase.GetCadena().LastOrDefault().Id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private string CalcularHash(Bloque bloque)
        {
            string texto = bloque.Id + bloque.Datos + bloque.Hash_anterior + bloque.Tiempo;
            return SHA256.Create(texto).ToString();
        }

        public List<Bloque> GetCadena()
        {
            return TrazabilidadSeudoDatabase.GetCadena();
        }

        public Bloque BuscarBloquePorId(int id)
        {
            return TrazabilidadSeudoDatabase.GetCadena().SingleOrDefault(e => e.Id == id);
        }

        public void ModificarBloque(Bloque bloqueEditado)
        {
            Bloque bloque = TrazabilidadSeudoDatabase.GetCadena().Find(f => f.Id == bloqueEditado.Id);
            bloque.Id = bloqueEditado.Id;
            bloque.Datos = bloqueEditado.Datos;
            bloque.Hash = bloqueEditado.Hash;
            bloque.Hash_anterior = bloqueEditado.Hash_anterior;
            bloque.Tiempo = bloqueEditado.Tiempo;
        }

        public void EliminarBloque(int Id)
        {
            Bloque bloque = BuscarBloquePorId(Id);
            TrazabilidadSeudoDatabase.GetCadena().Remove(bloque);
        }
    }
}
