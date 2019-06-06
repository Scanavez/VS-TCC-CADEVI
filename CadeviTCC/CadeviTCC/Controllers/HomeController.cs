using CadeviTCC.Models.Context;
using CadeviTCC.Models.Entities;
using CadeviTCC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeviTCC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(Usuario u)
        {

            HomeRepository repository = new HomeRepository();

            var usuario = repository.realizarLogin(u);
            if (usuario != null)
            {
                Session.Remove("usuarioTipo");
                Session["usuarioLogadoID"] = usuario.Id.ToString();
                Session["usuarioLogadoNome"] = usuario.Nome;
                Session["usuarioTipo"] = usuario.IdTipoUsuario;

                int Id = Convert.ToInt32(Session["usuarioLogadoID"]);

                return RedirectToActionPermanent("Index", "Aluno");
            }

            return View(u).Mensagem("Digite seu Login", "Erro");

        }
    }
}