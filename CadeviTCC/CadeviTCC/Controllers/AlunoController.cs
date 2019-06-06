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
    public class AlunoController : Controller
    {
        private ContextBanco db = new ContextBanco();

        private HomeRepository repository = new HomeRepository();


        // GET: Aluno
        public ActionResult Index()
        {
            var IdUsuario = Convert.ToInt32(Session["usuarioLogadoID"]);
            var IdTipo = Convert.ToInt32(Session["usuarioTipo"]);

            //var IdTipo = repository.getTipoUsuarioLogado();
            ICollection<Aluno> aluno = new List<Aluno>();

            if (IdTipo == 2)
            {
                aluno = db.Alunos.ToList();
            }
            else
            {
                aluno = db.Alunos.Where(x => x.IdUsuario == IdUsuario).ToList();
            }

            return View(aluno.ToList());
        }

        // GET: Aluno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        public ActionResult DocumentosAluno(int? id)
        {
            Session.Remove("IdAluno");
            Session["IdAluno"] = id.ToString();
            return RedirectToAction("IndexDocAluno", "Documento", new { id });
        }

        // GET: Aluno/Create
        public ActionResult Create()
        {
            var IdTipo = Convert.ToInt32(Session["usuarioTipo"]);
            var IdUsuario = Convert.ToInt32(Session["usuarioLogadoID"]);

            if (IdTipo == 2)
            {
                ViewBag.IdUsuario = new SelectList(db.Usuarios.Where(x => x.IdTipoUsuario == 1), "Id", "Nome");
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Aluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,IdUsuario")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Alunos.Add(aluno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.Usuarios.Where(x => x.IdTipoUsuario == 3), "Id", "Nome", aluno.IdUsuario);

            return View(aluno);
        }

        // GET: Aluno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "Login", aluno.IdUsuario);
            return View(aluno);
        }

        // POST: Aluno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,IdUsuario")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "Login", aluno.IdUsuario);
            return View(aluno);
        }

        // GET: Aluno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluno aluno = db.Alunos.Find(id);
            db.Alunos.Remove(aluno);
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
