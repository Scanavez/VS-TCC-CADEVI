using CadeviTCC.Models.Context;
using CadeviTCC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeviTCC.Repository
{
    public class HomeRepository//: Controller
    {
        ContextBanco banco = new ContextBanco();

        public Usuario realizarLogin(Usuario u)
        {

            var registros = banco.Usuarios.Where(
                x => x.Login.Equals(u.Login) 
                && x.Senha.Equals(u.Senha)).FirstOrDefault();

            return registros;
        }

        //public int getTipoUsuarioLogado()
        //{
        //    var Id = Convert.ToInt32(Session["usuarioTipo"]);
        //    return Id;
        //}

    }
}