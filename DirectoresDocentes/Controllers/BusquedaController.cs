using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DirectoresDocentes.Controllers
{
    public class BusquedaController : Controller
    {
        //
        // GET: /Busqueda/

        public ActionResult Index()
        {
            ViewBag.Login = false;
            ViewBag.Busqueda = false;
            ViewBag.Director = false;
            ViewBag.IfUser = true;
            ViewBag.User = Session["username"];
            ViewBag.Persona = Session["persona"];
            return View();
        }

    }
}
