using SistemaWebAver_JMedina.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaWebAver_JMedina.Controllers
{
    public class SitiosController : Controller
    {
        private readonly AveTacnaContext db = new AveTacnaContext();

        private static readonly string[] Tipos = { "Bofedal", "Costa rocosa", "Desembocadura",
            "Humedal costero", "Laguna altoandina", "Lomas costeras", "Valle agrícola" };
        private static readonly string[] Provincias = { "Candarave", "Jorge Basadre", "Tacna", "Tarata" };

        // GET: /Sitios?buscar=laguna
        public ActionResult Index(string buscar)
        {
            ViewBag.Buscar = buscar;
            var consulta = db.Sitios.Include(s => s.Expediciones).AsQueryable();
            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.Trim();
                consulta = consulta.Where(s => s.Nombre.Contains(buscar) || s.Tipo.Contains(buscar));
            }
            return View(consulta.OrderBy(s => s.Nombre).ToList());
        }

        // GET: /Sitios/Details/1
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var sitio = db.Sitios.Find(id.Value);
            if (sitio == null) return HttpNotFound();
            return View(sitio);
        }

        // GET: /Sitios/Create
        public ActionResult Create()
        {
            var sitio = new Sitios();
            CargarListas(sitio);
            return View(sitio);
        }

        // POST: /Sitios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Nombre,Tipo,Provincia,AltitudMsnm,Latitud,Longitud")] Sitios sitio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Sitios.Add(sitio);
                    db.SaveChanges();
                    TempData["Mensaje"] = "Sitio registrado correctamente.";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex) when (EsDuplicado(ex))
                {
                    ModelState.AddModelError("Nombre", "Ya existe un sitio con ese nombre.");
                }
            }
            CargarListas(sitio);
            return View(sitio);
        }

        // GET: /Sitios/Edit/1
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var sitio = db.Sitios.Find(id.Value);
            if (sitio == null) return HttpNotFound();
            CargarListas(sitio);
            return View(sitio);
        }

        // POST: /Sitios/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "SitioId,Nombre,Tipo,Provincia,AltitudMsnm,Latitud,Longitud")] Sitios sitio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(sitio).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Mensaje"] = "Sitio actualizado correctamente.";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex) when (EsDuplicado(ex))
                {
                    ModelState.AddModelError("Nombre", "Ya existe un sitio con ese nombre.");
                }
            }
            CargarListas(sitio);
            return View(sitio);
        }

        // GET: /Sitios/Delete/1
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var sitio = db.Sitios.Find(id.Value);
            if (sitio == null) return HttpNotFound();
            return View(sitio);
        }

        // POST: /Sitios/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sitio = db.Sitios.Find(id);
            if (sitio == null) return HttpNotFound();
            try
            {
                db.Sitios.Remove(sitio);
                db.SaveChanges();
                TempData["Mensaje"] = "Sitio eliminado correctamente.";
            }
            catch (DbUpdateException ex) when (NumeroSql(ex) == 547)   // llave foránea
            {
                TempData["Error"] =
                    "No se puede eliminar el sitio porque tiene expediciones o especies asociadas.";
            }
            return RedirectToAction("Index");
        }

        private void CargarListas(Sitios sitio)
        {
            ViewBag.Tipos = new SelectList(Tipos, sitio?.Tipo);
            ViewBag.Provincias = new SelectList(Provincias, sitio?.Provincia);
        }

        // Número de error de SQL Server que hay detrás de la DbUpdateException
        private static int NumeroSql(Exception ex)
        {
            var sql = ex.GetBaseException() as SqlException;
            return sql == null ? 0 : sql.Number;
        }

        private static bool EsDuplicado(Exception ex)
        {
            int n = NumeroSql(ex);
            return n == 2627 || n == 2601;     // restricción UNIQUE
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();       // libera la conexión al terminar la solicitud
            base.Dispose(disposing);
        }
    }
}