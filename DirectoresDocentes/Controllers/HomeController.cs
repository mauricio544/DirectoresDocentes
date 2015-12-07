using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bachillerato.Controllers;
using DirectoresDocentes.Context;

namespace DirectoresDocentes.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db;
        private Common funciones;
        public class Usuario
        {
            public int Codigo { get; set; }

            [MaxLength(20)]
            public string UserName { get; set; }

            [MaxLength(50)]
            public string Password { get; set; }
        }

        public class Responsables
        {
            public int PERSONA { get; set; }

            [MaxLength(20)]
            public string USUARIO { get; set; }
        }
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                return RedirectToAction("Index", "Busqueda");
            }
            ViewBag.IfUser = false;
            ViewBag.Busqueda = false;
            ViewBag.Director = false;
            ViewBag.Login = true;
            return View();            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Usuario user)
        {
            try
            {
                db = new AppDbContext();
                funciones = new Common();
                var contrasenia = funciones.Md5Hash(user.Password);
                var persona_user = db.Personas
                    .First(p => p.USUARIO == user.UserName && p.CONTRASENIA2 == contrasenia);                
                if (persona_user != null)
                {
                    Session["username"] = persona_user.USUARIO;
                    Session["persona"] = persona_user.CODIGO;
                    List<int> estados_alumno = new List<int>();
                    estados_alumno.Add(1);
                    estados_alumno.Add(5);
                    estados_alumno.Add(8);
                    estados_alumno.Add(10);
                    List<int> director_decano = new List<int>();
                    director_decano.Add(7);
                    director_decano.Add(18);
                    var roles_programas =
                        db.Programas_Estudios_Personas.Any(
                            p => p.PERSONA_RESPONSABLE == persona_user.CODIGO && director_decano.Contains(p.PERSONA_ROL));
                    if (roles_programas)
                    {
                        Session["director"] = true;
                        ViewBag.Login = false;
                        ViewBag.Busqueda = false;
                        ViewBag.Director = false;
                        ViewBag.IfUser = true;
                        return RedirectToAction("Index", "Busqueda");
                    }                    
                    return RedirectToAction("Index", "Busqueda");
                }
                ViewBag.Message = "Error con usuario o contraseña!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Error con usuario o contraseña!";
                ViewBag.IfUser = false;
            }
            ViewBag.Login = false;
            ViewBag.Busqueda = false;
            ViewBag.Director = false;
            ViewBag.IfUser = true;
            return View();
        }
    }
}
