using Microsoft.AspNetCore.Mvc;
using Trazabilidad.Repositorio.Entidades;
using Trazabilidad.Servicio.Interfaces;

namespace Trazabilidad.Web.Controllers
{
    public class CadenaBloqueController : Controller
    {
        readonly ITrazabilidadServicio _servicio;

        public CadenaBloqueController(ITrazabilidadServicio servicio)   // importantisimo tener este constructor para enlazar el constructor con el servicio
        {
            _servicio = servicio;
        }

        public IActionResult Agregar()
        {
            return View();
        }

        // Método para agregar un bloque a la cadena
        public IActionResult AgregarBloque(IFormCollection formulario)
        {

            _servicio.AgregarBloque(formulario["Datos"]);
            return Redirect("/CadenaBloque/Listar");
        }

        public IActionResult Listar()
        {
            List<Bloque> lista = _servicio.ListarBloques();
            return View(lista);
        }

        public IActionResult Editar(int Id) 
        {
            Bloque bloque = _servicio.BuscarBloquePorId(Id);
            return View(bloque); 
        }

        [HttpPost]
        public IActionResult Editar(IFormCollection formulario)
        {
            Bloque bloqueEditado = new Bloque();
            bloqueEditado.Id = int.Parse(formulario["Id"]);
            bloqueEditado.Datos = formulario["Datos"];
            bloqueEditado.Hash = formulario["Hash"];
            bloqueEditado.Hash_anterior = formulario["Hash_anterior"];
            bloqueEditado.Tiempo = DateTime.Parse( formulario["Tiempo"]);
            _servicio.ModificarBloque(bloqueEditado);
            return View();
        }

        [HttpGet]
        public IActionResult Eliminar(int Id)
        {
            _servicio.EliminarBloque(Id);
            return Redirect("/CadenaBloque/Listar");
        }

    }
}
