using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Entities
{
    [Table("TipoUsuario")]
    public class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuarios = new List<Usuario>();
        }

        public int Id { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}