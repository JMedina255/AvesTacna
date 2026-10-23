using SistemaWebAver_JMedina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SistemaWebAver_JMedina.Controllers
{
    public class ExpedicionesController : Controller
    {
        private readonly AveTacnaContext db = new AveTacnaContext();
        // GET: Expediciones
        public ActionResult Index(string buscar)
        {
            var expediciones = db.Expediciones
                .Include(e => e.Sitios)
                .Include(e => e.Observadores)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                buscar = buscar.Trim();

                expediciones = expediciones.Where(e =>
                e.Sitios.Nombre.Contains(buscar) ||
                e.Observadores.Nombres.Contains(buscar) ||
                e.Observadores.Apellidos.Contains(buscar));
            }

            ViewBag.Buscar = buscar;

            return View(expediciones
                .OrderByDescending(e => e.Fecha)
                .ThenBy(e => e.Nombre)
                .ToList());
        }

        // GET: Expediciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var expedicion = db.Expediciones
                .Include(e => e.Sitios)
                .Include(e => e.Observadores)
                .SingleOrDefault(e => e.ExpedicionId == id.Value);

            if (expedicion == null)
            {
                return HttpNotFound();
            }

            return View(expedicion);
        }

        // GET: Expediciones/Create
        public ActionResult Create()
        {
            var expedicion = new Expediciones
            {
                Fecha = System.DateTime.Today
            };

            CargarListas(expedicion);

            return View(expedicion);
        }

        // POST: Expediciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Fecha,Nombre,SitioId,GuiaId")] Expediciones expedicion)
        {
            ValidarRelaciones(expedicion);

            if (ModelState.IsValid)
            {
                db.Expediciones.Add(expedicion);
                db.SaveChanges();

                TempData["Mensaje"] = "Expedición registrada correctamente.";

                return RedirectToAction("Index");
            }

            CargarListas(expedicion);

            return View(expedicion);
        }

        // GET: Expediciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var expedicion = db.Expediciones.Find(id.Value);

            if (expedicion == null)
            {
                return HttpNotFound();
            }

            CargarListas(expedicion);

            return View(expedicion);
        }

        // POST: Expediciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ExpedicionId,Fecha,Nombre,SitioId,GuiaId")] Expediciones expedicion)
        {
            ValidarRelaciones(expedicion);

            if (ModelState.IsValid)
            {
                var expedicionDb = db.Expediciones.Find(expedicion.ExpedicionId);

                if (expedicionDb == null)
                {
                    return HttpNotFound();
                }

                expedicionDb.Fecha = expedicion.Fecha;
                expedicionDb.Nombre = expedicion.Nombre;
                expedicionDb.SitioId = expedicion.SitioId;
                expedicionDb.GuiaId = expedicion.GuiaId;

                db.SaveChanges();

                TempData["Mensaje"] = "Expedición actualizada correctamente.";

                return RedirectToAction("Index");
            }

            CargarListas(expedicion);

            return View(expedicion);
        }

        // GET: Expediciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var expedicion = db.Expediciones
                .Include(e => e.Sitios)
                .Include(e => e.Observadores)
                .SingleOrDefault(e => e.ExpedicionId == id.Value);

            if (expedicion == null)
            {
                return HttpNotFound();
            }

            return View(expedicion);
        }

        // POST: Expediciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var expedicion = db.Expediciones.Find(id);

            if (expedicion == null)
            {
                return HttpNotFound();
            }

            db.Expediciones.Remove(expedicion);
            db.SaveChanges();

            TempData["Mensaje"] = "Expedición eliminada correctamente.";

            return RedirectToAction("Index");
        }

        private void CargarListas(Expediciones expedicion)
        {
            ViewBag.Sitios = new SelectList(
                db.Sitios
                    .OrderBy(s => s.Nombre)
                    .ToList(),
                "SitioId",
                "Nombre",
                expedicion.SitioId);

            var guias = db.Observadores
                .OrderBy(o => o.Apellidos)
                .ThenBy(o => o.Nombres)
                .ToList()
                .Select(o => new
                {
                    o.ObservadorId,
                    NombreCompleto = o.Apellidos + ", " + o.Nombres
                })
                .ToList();

            ViewBag.Guias = new SelectList(
                guias,
                "ObservadorId",
                "NombreCompleto",
                expedicion.GuiaId);
        }

        private void ValidarRelaciones(Expediciones expedicion)
        {
            if (!db.Sitios.Any(s => s.SitioId == expedicion.SitioId))
            {
                ModelState.AddModelError("SitioId", "Debe seleccionar un sitio válido.");
            }

            if (!db.Observadores.Any(o => o.ObservadorId == expedicion.GuiaId))
            {
                ModelState.AddModelError("GuiaId", "Debe seleccionar un guía válido.");
            }
        }

    }
}