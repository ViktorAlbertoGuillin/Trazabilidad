using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Trazabilidad.Repositorio;
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
            int? idUltimo = _repositorio.ObtenerUltimoId();
            idUltimo++;
            Bloque bloque = new Bloque();
            bloque.Id = (int)idUltimo;
            bloque.Datos = dato;
            bloque.Tiempo = DateTime.Now;
            bloque.Hash_anterior = TrazabilidadSeudoDatabase.GetCadena().Count() == 0 ? "0" : TrazabilidadSeudoDatabase.GetCadena().Last().Hash;
            bloque.Hash = CalcularHash(bloque.Id + bloque.Datos + bloque.Tiempo + bloque.Hash_anterior);
            _repositorio.GuardarBloque(bloque);
        }
        public string CalcularHash(string dato)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{dato}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(outputBytes);
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

        public string VerificarIntegridad()
        {
            string hash;
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
                hash = CalcularHash(bloque.Id + bloque.Datos + bloque.Tiempo + bloque.Hash_anterior);
                if(hash != bloque.Hash)
                {
                    return "Al menos un bloque de informacion fue alterado, el hash almacenado no coincide con la informacion del bloque";
                }
               
            }
            return "La cadena de bloque se encuentra completa y su contenido no fue alterado";
        }
    }

    

   
}
