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
using CadeviTCC.Repository;

namespace CadeviTCC.Controllers
{
    public class AlunoxDocumentoController : Controller
    {
        private ContextBanco db = new ContextBanco();

        private HomeRepository repository = new HomeRepository();


        // GET: AlunoxDocumento
        public ActionResult Index()
        {
            var alunoxDocumento = db.alunoxDocumento.Include(a => a.Aluno).Include(a => a.Documento);
            return View(alunoxDocumento.ToList());
        }

        // GET: AlunoxDocumento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoxDocumento alunoxDocumento = db.alunoxDocumento.Find(id);
            if (alunoxDocumento == null)
            {
                return HttpNotFound();
            }
            return View(alunoxDocumento);
        }

        // GET: AlunoxDocumento/Create
        public ActionResult Create()
        {
            ViewBag.IdAluno = new SelectList(db.Alunos, "Id", "Nome");
            ViewBag.IdDocumento = new SelectList(db.Documentos, "Id", "Descricao");
            return View(false);
        }

        public ActionResult VincularNovoDoc(int id)
        {
            var IdAluno = Convert.ToInt32(Session["IdAluno"]);

            AlunoxDocumento alunoxdoc = new AlunoxDocumento();

            alunoxdoc.IdDocumento = id;
            alunoxdoc.IdAluno = IdAluno;

            db.alunoxDocumento.Add(alunoxdoc);
            db.SaveChanges();
            int Id = IdAluno;
            return RedirectToAction("IndexDocAluno", "Documento", new {Id});
        }

        public ActionResult Desvincular(int id)
        {
            var Id = Convert.ToInt32(Session["IdAluno"]);
            var IdAlunoDoc = db.alunoxDocumento.Where(x => x.IdAluno == Id && x.IdDocumento == id).Select(x => x.Id).FirstOrDefault();
            var idsArq = db.ArquivoDigitalDocumento.Where(x => x.IdAlunoXDocumento == IdAlunoDoc).Select(x => x.Id).ToList();

            foreach (var item in idsArq)
            {
                ArquivoDigitalDocumento arq = db.ArquivoDigitalDocumento.Find(item);
                db.ArquivoDigitalDocumento.Remove(arq);
            }

            AlunoxDocumento alunoxDocumento = db.alunoxDocumento.Where(x => x.IdAluno == Id && x.IdDocumento == id).FirstOrDefault();
            db.alunoxDocumento.Remove(alunoxDocumento);
            db.SaveChanges();

            return RedirectToAction("IndexDocAluno", "Documento", new { Id });
        }

        // POST: AlunoxDocumento/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdAluno,IdDocumento")] AlunoxDocumento alunoxDocumento)
        {
            if (ModelState.IsValid)
            {
                db.alunoxDocumento.Add(alunoxDocumento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAluno = new SelectList(db.Alunos, "Id", "Nome", alunoxDocumento.IdAluno);
            ViewBag.IdDocumento = new SelectList(db.Documentos, "Id", "Descricao", alunoxDocumento.IdDocumento);
            return View(alunoxDocumento);
        }

        // GET: AlunoxDocumento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoxDocumento alunoxDocumento = db.alunoxDocumento.Find(id);
            if (alunoxDocumento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAluno = new SelectList(db.Alunos, "Id", "Nome", alunoxDocumento.IdAluno);
            ViewBag.IdDocumento = new SelectList(db.Documentos, "Id", "Descricao", alunoxDocumento.IdDocumento);
            return View(alunoxDocumento);
        }

        // POST: AlunoxDocumento/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdAluno,IdDocumento")] AlunoxDocumento alunoxDocumento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alunoxDocumento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAluno = new SelectList(db.Alunos, "Id", "Nome", alunoxDocumento.IdAluno);
            ViewBag.IdDocumento = new SelectList(db.Documentos, "Id", "Descricao", alunoxDocumento.IdDocumento);
            return View(alunoxDocumento);
        }

        // GET: AlunoxDocumento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoxDocumento alunoxDocumento = db.alunoxDocumento.Find(id);
            if (alunoxDocumento == null)
            {
                return HttpNotFound();
            }
            return View(alunoxDocumento);
        }

        // POST: AlunoxDocumento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlunoxDocumento alunoxDocumento = db.alunoxDocumento.Find(id);
            db.alunoxDocumento.Remove(alunoxDocumento);
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
