using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
namespace MVC.Controllers
{
    public class UsuarioController : Controller
    {
        usuarioEntities db = new usuarioEntities();
        // GET: Usuario
        public ActionResult Index()
        {
            var list = db.Persona.ToList();

            return View(list);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Persona persona)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dato = new Persona();
                    dato.PrimerNombre = persona.PrimerNombre;
                    dato.SegundoNombre = persona.SegundoNombre;
                    dato.PrimerApellido = persona.PrimerApellido;
                    dato.SegundoApellido = persona.SegundoApellido;
                    dato.Ciudad = persona.Ciudad;
                    dato.Edad = persona.Edad;

                    db.Persona.Add(dato);
                    db.SaveChanges();
                    TempData["exito"] = "Registro agregado con éxito!";
                    return RedirectToAction("Index");
                }
                return View(persona);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Ha ocurrido un error al crear el registro!";
                return RedirectToAction("Index");
            }

        }


        public ActionResult Editar(int id)
        {
            var usuario = db.Persona.Find(id);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Editar(Persona persona)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = db.Persona.Find(persona.Id_usuario);
                    usuario.PrimerNombre = persona.PrimerNombre;
                    usuario.SegundoNombre = persona.SegundoNombre;
                    usuario.PrimerApellido = persona.PrimerApellido;
                    usuario.SegundoApellido = persona.SegundoApellido;
                    usuario.Ciudad = persona.Ciudad;
                    usuario.Edad = persona.Edad;
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["exito"] = "Registro actualizado con éxito!";
                    return RedirectToAction("Index");
                }
                return View(persona);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Ha ocurrido un error al actualizar el registro!";
                return RedirectToAction("Index");
            }
        }

        public ActionResult ELiminar(int id)
        {
            var usuario = db.Persona.Find(id);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult ELiminarRegistro(Persona persona)
        {
            try
            {
                var usuario = db.Persona.Find(persona.Id_usuario);
                db.Persona.Remove(usuario);
                db.SaveChanges();
                TempData["exito"] = "Registro eliminado con éxito!";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = "Ha ocurrido un error al eliminar el registro!";
                return RedirectToAction("Index");
            }
           
        }

        public ActionResult Detalles(int id)
        {
            var usuario = db.Persona.Find(id);
            return View(usuario);
        }
    }
}