using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        [HttpGet]
        public ActionResult GetAllCatalogos()
        {
            Dictionary<string, object> resultEmpleado = Negocio.Empleado.GetAll();
            bool resultadoEmpleado = (bool)resultEmpleado["Resultado"];
            Negocio.Empleado empleado = new Negocio.Empleado();

            if (resultadoEmpleado)
            {
                empleado = (Negocio.Empleado)resultEmpleado["Empleado"];
                Dictionary<string, object> resultPuesto = Negocio.Puesto.GetAll();
                bool resultadoPuesto = (bool)resultPuesto["Resultado"];
                if (resultadoPuesto)
                {
                    empleado.Puesto = (Negocio.Puesto)resultPuesto["Puesto"];
                    Dictionary<string, object> resultDepartamento = Negocio.Departamento.GetAll();
                    bool resultadoDepartamento = (bool)resultDepartamento["Resultado"];
                    if (resultadoDepartamento)
                    {
                        empleado.Departamento = (Negocio.Departamento)resultDepartamento["Departamento"];
                        return View(empleado);
                    }
                    else
                    {
                        return View(empleado);
                    }
                }
                else
                {
                    return View(empleado);
                }
            }
            else
            {
                return View(empleado);
            }
        }

        [HttpGet]
        public ActionResult EmpleadoGetAllDelete()
        {
            Dictionary<string, object> resultEmpleado = Negocio.Empleado.GetAll();
            bool resultadoEmpleado = (bool)resultEmpleado["Resultado"];
            Negocio.Empleado empleado = new Negocio.Empleado();

            if (resultadoEmpleado)
            {
                empleado = (Negocio.Empleado)resultEmpleado["Empleado"];
                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }

        [HttpGet]
        public ActionResult Delete(int EmpleadoID)
        {
            Dictionary<string, object> resultEmpleado = Negocio.Empleado.Delete(EmpleadoID);
            bool resultadoEmpleado = (bool)resultEmpleado["Resultado"];

            if (resultadoEmpleado)
            {
                ViewBag.Mensaje = "El registro se elimino correctamente";
                return PartialView("Modal");
            }
            else
            {
                string excepcion = (string)resultEmpleado["Excepcion"];
                ViewBag.Mensaje = "Ha ocurrido un error al borrar el registro: " + excepcion;
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public ActionResult EmpleadoGetByNombre(string Nombre)
        {
            Negocio.Empleado empleado = new Negocio.Empleado();

            if (Nombre == null)
            {
                Nombre = "";
            }

            Dictionary<string, object> resultEmpleado = Negocio.Empleado.GetByNombre(Nombre);
            bool resultadoEmpleado = (bool)resultEmpleado["Resultado"];

            if (resultadoEmpleado)
            {
                empleado = (Negocio.Empleado)resultEmpleado["Empleado"];
                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }
    }
}