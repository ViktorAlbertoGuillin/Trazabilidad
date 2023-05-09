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
        public void GuardarBloque(string dato)
        {
            int? idUltimo = ObtenerUltimoId();
            idUltimo++;
            Bloque bloque = new Bloque();
            bloque.Id = (int)idUltimo;
            bloque.Datos = dato;
            bloque.Tiempo = DateTime.Now;
            bloque.Hash_anterior = TrazabilidadSeudoDatabase.GetCadena().Count() == 0 ? "0" : TrazabilidadSeudoDatabase.GetCadena().Last().Hash;
            bloque.Hash = CalcularHash(bloque.Id + bloque.Datos + bloque.Tiempo + bloque.Hash_anterior);
            TrazabilidadSeudoDatabase.GetCadena().Add(bloque);
        }

        private int ObtenerUltimoId()
        {
            try
            {
                if(TrazabilidadSeudoDatabase.GetCadena().Count() == 0)
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

        private string CalcularHash(string dato)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{dato}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(outputBytes);
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
