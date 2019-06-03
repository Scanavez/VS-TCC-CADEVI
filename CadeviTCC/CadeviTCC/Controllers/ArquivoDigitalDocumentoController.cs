using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CadeviTCC.Models.Context;
using CadeviTCC.Models.Entities;

namespace CadeviTCC.Controllers
{
    public class ArquivoDigitalDocumentoController : Controller
    {
        private ContextBanco db = new ContextBanco();

        // GET: ArquivoDigitalDocumento
        public ActionResult Index()
        {
            var arquivoDigitalDocumentoes = db.ArquivoDigitalDocumentoes.Include(a => a.documento);
            return View(arquivoDigitalDocumentoes.ToList());
        }

        public ActionResult IndexDoc(int? Id)
        {
            var arquivoDigitalDocumentoes = db.ArquivoDigitalDocumentoes.Where(x => x.IdDocumento == Id);
            return View(arquivoDigitalDocumentoes.ToList());
        }

        // GET: ArquivoDigitalDocumento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArquivoDigitalDocumento arquivoDigitalDocumento = db.ArquivoDigitalDocumentoes.Find(id);
            if (arquivoDigitalDocumento == null)
            {
                return HttpNotFound();
            }
            return View(arquivoDigitalDocumento);
        }

        // GET: ArquivoDigitalDocumento/Create
        public ActionResult Create()
        {
            ViewBag.IdDocumento = new SelectList(db.Documentos, "Id", "Descricao");
            return View();
        }

        // POST: ArquivoDigitalDocumento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomeArquivo,Arquivo,IdDocumento")] ArquivoDigitalDocumento arquivoDigitalDocumento, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var Nome = System.IO.Path.GetFileName(upload.FileName);

                    var nomeArquivo = Nome.Split('.')[0];

                    var arquivo = new ArquivoDigitalDocumento
                    {
                        NomeArquivo = Nome,
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        arquivo.Arquivo = reader.ReadBytes(upload.ContentLength);
                    }
                    arquivoDigitalDocumento.Arquivo = arquivo.Arquivo;
                    arquivoDigitalDocumento.NomeArquivo = arquivo.NomeArquivo;
                }
                db.ArquivoDigitalDocumentoes.Add(arquivoDigitalDocumento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDocumento = new SelectList(db.Documentos, "Id", "Descricao", arquivoDigitalDocumento.IdDocumento);
            return View(arquivoDigitalDocumento);
        }

        public FileResult DownloadFile(int? id)
        {

            var Arquivo = db.ArquivoDigitalDocumentoes.Where(x => x.Id == id).FirstOrDefault();


            return File(Arquivo.Arquivo, "application/pdf", Arquivo.NomeArquivo);
        }

        // GET: ArquivoDigitalDocumento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArquivoDigitalDocumento arquivoDigitalDocumento = db.ArquivoDigitalDocumentoes.Find(id);
            if (arquivoDigitalDocumento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDocumento = new SelectList(db.Documentos, "Id", "Descricao", arquivoDigitalDocumento.IdDocumento);
            return View(arquivoDigitalDocumento);
        }

        // POST: ArquivoDigitalDocumento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomeArquivo,Arquivo,IdDocumento")] ArquivoDigitalDocumento arquivoDigitalDocumento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arquivoDigitalDocumento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDocumento = new SelectList(db.Documentos, "Id", "Descricao", arquivoDigitalDocumento.IdDocumento);
            return View(arquivoDigitalDocumento);
        }

        // GET: ArquivoDigitalDocumento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArquivoDigitalDocumento arquivoDigitalDocumento = db.ArquivoDigitalDocumentoes.Find(id);
            if (arquivoDigitalDocumento == null)
            {
                return HttpNotFound();
            }
            return View(arquivoDigitalDocumento);
        }

        // POST: ArquivoDigitalDocumento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArquivoDigitalDocumento arquivoDigitalDocumento = db.ArquivoDigitalDocumentoes.Find(id);
            db.ArquivoDigitalDocumentoes.Remove(arquivoDigitalDocumento);
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
