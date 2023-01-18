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
            ViewBag.fecha = DateTime.Now;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Persona persona)
        {
            try
            {
                var validar = db.Persona.Where(x => x.Identificacion == persona.Identificacion).Any(); 
                if (!validar) 
                {
                    if (ModelState.IsValid)
                    {
                        var dato = new Persona();
                        dato.Identificacion = persona.Identificacion;
                        dato.PrimerNombre = persona.PrimerNombre;
                        dato.PrimerApellido = persona.PrimerApellido;
                        dato.Fecha_de_nacimiento = persona.Fecha_de_nacimiento;
                        dato.Direccion = persona.Direccion;
                        dato.Correo = persona.Correo;
                        dato.Telefono = persona.Telefono;

                        db.Persona.Add(dato);
                        db.SaveChanges();
                        TempData["exito"] = "Registro agregado con éxito!";
                        return RedirectToAction("Index");
                    }else
                    {
                        return View(persona);
                    }

                }
                else
                {
                    ViewBag.repetido = "El número de identificación ya está registrado";
                    return View(persona);
                }
      
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
                    //var usuario = db.Persona.Find(persona.Id_usuario);
                    //usuario.PrimerNombre = persona.PrimerNombre;
                    //usuario.SegundoNombre = persona.SegundoNombre;
                    //usuario.PrimerApellido = persona.PrimerApellido;
                    //usuario.SegundoApellido = persona.SegundoApellido;
                    //usuario.Ciudad = persona.Ciudad;
                    //usuario.Edad = persona.Edad;
                    //db.Entry(usuario).State = EntityState.Modified;
                    //db.SaveChanges();
                    //TempData["exito"] = "Registro actualizado con éxito!";
                    //return RedirectToAction("Index");
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
                //var usuario = db.Persona.Find(persona.Id_usuario);
                //db.Persona.Remove(usuario);
                //db.SaveChanges();
                //TempData["exito"] = "Registro eliminado con éxito!";
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