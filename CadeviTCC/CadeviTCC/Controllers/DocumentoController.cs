using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CadeviTCC.Models.Context;
using CadeviTCC.Models.Context.Entities;
using CadeviTCC.Models.DTO;

namespace CadeviTCC.Controllers
{
    public class DocumentoController : Controller
    {
        private ContextBanco db = new ContextBanco();

        // GET: Documento
        public ActionResult Index()
        {
            return View(db.Documentos.ToList());
        }

        public ActionResult IndexDocAluno(int Id)
        {
            var documento = from doc in db.Documentos.ToList()
                            from alunodoc in db.alunoxDocumento.ToList().Where(x => x.IdDocumento == doc.Id)
                            where alunodoc.IdAluno == Id
                            select new DocumentoDTO {
                                IdDocumento = doc.Id,
                                Descricao = doc.Descricao
                            };

            //var documentoGrid = from doc in documento.ToList()
            //                    select new DocumentoDTO
            //                    {
            //                        IdDocumento = doc.Id
            //                    };

            return View(documento.ToList());
        }

        // GET: Documento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documentos.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }


        // GET: Documento/Details/5
        public ActionResult Buscar(int id)
        {
            Session.Remove("IdDocumento");
            Session["IdDocumento"] = id.ToString();
            return RedirectToAction("IndexDocDigital", "ArquivoDigitalDocumento", new { id });
        }

        // GET: Documento/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Documento/Create
        public ActionResult CreateDocAluno()
        {
            return RedirectToAction("IndexDocDigital", "ArquivoDigitalDocumento", new { id });
        }

        // POST: Documento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao,HoraRegistro")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                db.Documentos.Add(documento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documento);
        }

        // GET: Documento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documentos.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }

        // POST: Documento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao,HoraRegistro")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documento);
        }

        // GET: Documento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documentos.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }

        // POST: Documento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Documento documento = db.Documentos.Find(id);
            db.Documentos.Remove(documento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
