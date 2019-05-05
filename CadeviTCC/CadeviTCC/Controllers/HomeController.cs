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


            if (repository.realizarLogin(u))
            {
                Session["usuarioLogadoID"] = u.Id.ToString();
                Session["usuarioLogadoNome"] = u.Nome;

                return RedirectToActionPermanent("Index");
            }

            return View(u).Mensagem("Digite seu Login", "Erro");

        }
    }
}