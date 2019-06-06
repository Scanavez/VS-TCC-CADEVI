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
using CadeviTCC.Repository;

namespace CadeviTCC.Controllers
{
    public class DocumentoController : Controller
    {
        private ContextBanco db = new ContextBanco();

        private HomeRepository repository = new HomeRepository();

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

            Session.Remove("VincAlunoDoc");

            var IdAluno = Convert.ToInt32(Session["IdAluno"]);

            //db.alunoxDocumento.Where(x => x.IdAluno == IdAluno).Select(x => x.IdDocumento).ToList();

            var IdVinculo = db.alunoxDocumento.Where(x => x.IdAluno == IdAluno && x.IdDocumento == id)
                .Select(x => x.Id).FirstOrDefault();

            Session["VincAlunoDoc"] = IdVinculo.ToString();


            return RedirectToAction("IndexDocDigital", "ArquivoDigitalDocumento", new { id });
        }

        public ActionResult Desvincular(int id)
        {
            //Session.Remove("IdDocumento");
            //Session["IdDocumento"] = id.ToString();

            Int32 Id;

            var idAluno = Convert.ToInt32(Session["IdAluno"]);

            var IdTipo = Convert.ToInt32(Session["usuarioTipo"]);

            if (IdTipo == 2)
            {
                var existeArqDigital = from alunoDoc in db.alunoxDocumento.ToList()
                                       from arq in db.ArquivoDigitalDocumento.ToList().Where(x => x.IdAlunoXDocumento == alunoDoc.Id)
                                       where alunoDoc.IdDocumento == id && alunoDoc.IdAluno == idAluno
                                       select new DocumentoDTO
                                       {
                                           IdDocumento = alunoDoc.Id
                                       };

                if (existeArqDigital.Count() > 0)
                {
                    Id = idAluno;
                    return RedirectToAction("IndexDocAluno", "Documento", new { Id });
                }
                else
                {
                    return RedirectToAction("Desvincular", "AlunoxDocumento", new { id });
                }
            }
            else
            {
                Id = idAluno;
                return RedirectToAction("IndexDocAluno", "Documento", new { Id });
            }
        }

        // GET: Documento/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Documento/Create
        public ActionResult VincularNovoDocumento(int Id)
        {
            return RedirectToAction("VincularNovoDoc", "AlunoxDocumento",  new {Id});
        }

        public ActionResult VincularDocumento()
        {
            var IdAluno = Convert.ToInt32(Session["IdAluno"]);

            var IdsDocVinculado = db.alunoxDocumento.Where(x => x.IdAluno == IdAluno).Select(x => x.IdDocumento).ToList();

            var DocNVinculado = from doc in db.Documentos.ToList()
                                where !IdsDocVinculado.Contains(doc.Id)
                                select new DocumentoDTO
                                {
                                    IdDocumento = doc.Id,
                                    Descricao = doc.Descricao
                                };

            return View(DocNVinculado.ToList());
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
