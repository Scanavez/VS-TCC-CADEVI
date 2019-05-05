using CadeviTCC.Models.Context;
using CadeviTCC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadeviTCC.Repository
{
    public class HomeRepository
    {
        ContextBanco banco = new ContextBanco();

        public bool realizarLogin(Usuario u)
        {

            var registros = banco.Usuarios.Where(
                x => x.Login.Equals(u.Login) 
                && x.Senha.Equals(u.Senha)).Count() > 0;

            return registros;

        }

    }
}