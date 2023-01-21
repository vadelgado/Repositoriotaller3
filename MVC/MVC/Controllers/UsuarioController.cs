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
    
       /* Creating a new instance of the database. */
        usuarioEntities db = new usuarioEntities(); 
        // GET: Usuario
       /// <summary>
       /// It returns a list of all the people in the database
       /// </summary>
       /// <returns>
       /// A list of all the people in the database.
       /// </returns>
        public ActionResult Index()
        {
            var list = db.Persona.ToList();
            return View(list);
        }

       /// <summary>
       /// This function is called when the user clicks on the "Create" button on the "Index" page.
       /// </summary>
       /// <returns>
       /// A view
       /// </returns>
        public ActionResult Create()
        {           
            return View();
        }

       /// <summary>
       /// If the model is valid, then create a new instance of the model, assign the values of the
       /// model to the new instance, add the new instance to the database, save the changes, and
       /// redirect to the index page. 
       /// 
       /// If the model is not valid, then return the view with the model. 
       /// 
       /// If the model is valid, but the identification number already exists, then return the view
       /// with the model and a message. 
       /// 
       /// If an error occurs, then redirect to the index page with a message.
       /// </summary>
       /// <param name="Persona">is the model</param>
       /// <returns>
       /// The view is being returned.
       /// </returns>
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


       /// <summary>
       /// It finds the user with the given identification number.
       /// </summary>
       /// <param name="Identificacion">The primary key of the table.</param>
       /// <returns>
       /// The view is being returned.
       /// </returns>
        public ActionResult Editar(string Identificacion)
        {
            var usuario = db.Persona.Find(Identificacion);
            return View(usuario);
        }

       /// <summary>
       /// A function that allows you to edit the data of a person, it is a function that is in the
       /// controller of the person.
       /// </summary>
       /// <param name="Persona">is the model</param>
       /// <returns>
       /// The view is being returned.
       /// </returns>
        [HttpPost]
        public ActionResult Editar(Persona persona)
        {
            try
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
                    db.Entry(dato).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["exito"] = "Registro actualizado con éxito!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(persona);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = "Ha ocurrido un error al actualizar el registro!";
                return RedirectToAction("Index");
            }
        }

       /// <summary>
       /// The function is called ELiminar, it takes a string called Identificacion, it finds a user in
       /// the database with that Identificacion, and then it returns a view of that user.
       /// </summary>
       /// <param name="Identificacion">The primary key of the table.</param>
       /// <returns>
       /// The user is being returned.
       /// </returns>
        public ActionResult ELiminar(string Identificacion)
        {
            var usuario = db.Persona.Find(Identificacion);
            return View(usuario);
        }

       /// <summary>
       /// The function receives a parameter of type Persona, then it searches for the user in the
       /// database, then it deletes the user from the database and finally it saves the changes in the
       /// database
       /// </summary>
       /// <param name="Persona">This is the model that I'm using to get the data from the view.</param>
       /// <returns>
       /// The action result is being returned.
       /// </returns>
        [HttpPost]
        public ActionResult ELiminarRegistro(Persona persona)
        {
            try
            {
                var usuario = db.Persona.Find(persona.Identificacion);
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

/// <summary>
/// La función Detalles() toma una cadena como parámetro y devuelve una vista del usuario con los datos del usuario.
/// </summary>
/// <param name="Identificacion">La llave primaria de la tabla.</param>
/// <returns>
/// The view is being returned.
/// </returns>
        public ActionResult Detalles(string Identificacion)
        {
            var usuario = db.Persona.Find(Identificacion);
            return View(usuario);
        }
    }
}