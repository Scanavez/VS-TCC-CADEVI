﻿using System;
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
    public class UsuarioController : Controller
    {
        private ContextBanco db = new ContextBanco();

        private HomeRepository repository = new HomeRepository();


        // GET: Usuario
        public ActionResult Index()
        {
            var usuario = db.Usuarios.ToList();
            var tipoUsuario = db.TipoUsuarios.ToList();
            var usuarios = db.Usuarios.Include(u => u.tipoUsuario).Where(x => x.tipoUsuario.Id != 2).ToList();

            return View(usuario);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            var T = Convert.ToInt32(Session["usuarioTipo"]);

            if (T != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            var T = Convert.ToInt32(Session["usuarioTipo"]);

            if (T != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.IdTipoUsuario = new SelectList(db.TipoUsuarios, "Id", "Descricao");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Senha,Nome,IdTipoUsuario")] Usuario usuario)
        {
            var T = Convert.ToInt32(Session["usuarioTipo"]);

            if (T != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTipoUsuario = new SelectList(db.TipoUsuarios, "Id", "Descricao", usuario.IdTipoUsuario);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            var T = Convert.ToInt32(Session["usuarioTipo"]);

            if (T != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTipoUsuario = new SelectList(db.TipoUsuarios, "Id", "Descricao", usuario.IdTipoUsuario);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Senha,Nome,IdTipoUsuario")] Usuario usuario)
        {
            var T = Convert.ToInt32(Session["usuarioTipo"]);

            if (T != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTipoUsuario = new SelectList(db.TipoUsuarios, "Id", "Descricao", usuario.IdTipoUsuario);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            var T = Convert.ToInt32(Session["usuarioTipo"]);

            if (T != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var T = Convert.ToInt32(Session["usuarioTipo"]);

            if (T != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            var idsAluno = db.Alunos.Where(x => x.IdUsuario == id).Select(x => x.Id).ToList();

            foreach (var itemaluno in idsAluno)
            {
                Aluno aluno = db.Alunos.Find(itemaluno);
                aluno.IdUsuario = null;
                db.Entry(aluno).State = EntityState.Modified;
                db.SaveChanges();
            }

            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
