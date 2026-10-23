using SistemaWebAver_JMedina.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaWebAver_JMedina.Controllers
{
    public class ObservadoresController : Controller
    {
        private Observadores observador = new Observadores();
        // GET: /Observadores?criterio=...
        public ActionResult Index(string criterio)
        {
            ViewBag.Criterio = criterio;
            return View(string.IsNullOrWhiteSpace(criterio)
                ? observador.Listar()
                : observador.Buscar(criterio.Trim()));
        }

        // GET: /Observadores/Ver/5
        public ActionResult Ver(int id)
        {
            var o = observador.Obtener(id);
            if (o == null) return HttpNotFound();
            return View(o);
        }

        // GET: /Observadores/AgregarEditar      (nuevo)
        // GET: /Observadores/AgregarEditar/5    (modificar)
        public ActionResult AgregarEditar(int id = 0)
        {
            if (id == 0)
                return View(new Observadores { FechaIngreso = DateTime.Today });

            var o = observador.Obtener(id);
            if (o == null) return HttpNotFound();
            return View(o);
        }

        // POST: /Observadores/Guardar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(Observadores modelo)
        {
            if (!ModelState.IsValid)
                return View("AgregarEditar", modelo);

            try
            {
                modelo.Guardar();
                TempData["ok"] = "Observador guardado correctamente.";
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "El registro ya no existe.");
            }
            catch (DbUpdateException ex)
            {
                int n = NumeroSql(ex);
                if (n == 2627 || n == 2601)          // índice o restricción UNIQUE
                    ModelState.AddModelError("Email",
                        "Ya existe un observador con ese correo.");
                else
                    throw;
            }
            return View("AgregarEditar", modelo);
        }

        // POST: /Observadores/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id)
        {
            try
            {
                observador.ObservadorId = id;
                observador.Eliminar();
                TempData["ok"] = "Observador eliminado.";
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["error"] = "El registro ya no existe.";
            }
            catch (DbUpdateException ex)
            {
                if (NumeroSql(ex) == 547)             // llave foránea
                    TempData["error"] = "No se puede eliminar: el observador tiene " +
                        "expediciones, avistamientos o una cuenta de usuario asociados.";
                else
                    throw;
            }
            return RedirectToAction("Index");
        }

        // Número de error de SQL Server detrás de la excepción de EF
        private static int NumeroSql(Exception ex)
        {
            var sql = ex.GetBaseException() as SqlException;
            return sql == null ? 0 : sql.Number;
        }
    }
}