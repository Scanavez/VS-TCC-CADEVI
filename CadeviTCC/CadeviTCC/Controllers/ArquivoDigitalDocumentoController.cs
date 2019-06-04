using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CadeviTCC.Models.Context;
using CadeviTCC.Models.DTO;
using CadeviTCC.Models.Entities;

namespace CadeviTCC.Controllers
{
    public class ArquivoDigitalDocumentoController : Controller
    {
        private ContextBanco db = new ContextBanco();

        // GET: ArquivoDigitalDocumento
        public ActionResult Index()
        {
            var arquivoDigitalDocumento = db.ArquivoDigitalDocumento.Include(a => a.alunoxDocumento).ToList();
            return View(arquivoDigitalDocumento);
        }

        public ActionResult IndexDocDigital(int id)
        {
            var IdAluno = Convert.ToInt32(Session["IdAluno"]);
            var IdDocumento = Convert.ToInt32(Session["IdDocumento"]);

            var arquivo = from arquivoB in db.ArquivoDigitalDocumento.ToList()
                          from alunoDoc in db.alunoxDocumento.ToList().Where(x => x.Id == arquivoB.IdAlunoXDocumento)
                          where alunoDoc.IdAluno == IdAluno && alunoDoc.IdDocumento == IdDocumento
                          select new ArquivoDigitalDocumentoDTO
                          {
                              Id = arquivoB.Id,
                              NomeArquivo = arquivoB.NomeArquivo
                          };

            //var arquivoDigitalDocumento = db.ArquivoDigitalDocumento.Include(a => a.alunoxDocumento).ToList();
            return View(arquivo.ToList());
        }

        // GET: ArquivoDigitalDocumento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArquivoDigitalDocumento arquivoDigitalDocumento = db.ArquivoDigitalDocumento.Find(id);
            if (arquivoDigitalDocumento == null)
            {
                return HttpNotFound();
            }
            return View(arquivoDigitalDocumento);
        }

        // GET: ArquivoDigitalDocumento/Create
        public ActionResult Create()
        {
            ViewBag.IdAlunoXDocumento = new SelectList(db.alunoxDocumento, "Id", "Id");
            return View();
        }

        // POST: ArquivoDigitalDocumento/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomeArquivo,Arquivo,IdDocumento,IdAlunoXDocumento")] ArquivoDigitalDocumento arquivoDigitalDocumento, HttpPostedFileBase upload)
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
                db.ArquivoDigitalDocumento.Add(arquivoDigitalDocumento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAlunoXDocumento = new SelectList(db.alunoxDocumento, "Id", "Id", arquivoDigitalDocumento.IdAlunoXDocumento);
            return View(arquivoDigitalDocumento);
        }

        // GET: ArquivoDigitalDocumento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArquivoDigitalDocumento arquivoDigitalDocumento = db.ArquivoDigitalDocumento.Find(id);
            if (arquivoDigitalDocumento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAlunoXDocumento = new SelectList(db.alunoxDocumento, "Id", "Id", arquivoDigitalDocumento.IdAlunoXDocumento);
            return View(arquivoDigitalDocumento);
        }

        // POST: ArquivoDigitalDocumento/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomeArquivo,Arquivo,IdDocumento,IdAlunoXDocumento")] ArquivoDigitalDocumento arquivoDigitalDocumento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arquivoDigitalDocumento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAlunoXDocumento = new SelectList(db.alunoxDocumento, "Id", "Id", arquivoDigitalDocumento.IdAlunoXDocumento);
            return View(arquivoDigitalDocumento);
        }

        // GET: ArquivoDigitalDocumento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArquivoDigitalDocumento arquivoDigitalDocumento = db.ArquivoDigitalDocumento.Find(id);
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
            ArquivoDigitalDocumento arquivoDigitalDocumento = db.ArquivoDigitalDocumento.Find(id);
            db.ArquivoDigitalDocumento.Remove(arquivoDigitalDocumento);
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
