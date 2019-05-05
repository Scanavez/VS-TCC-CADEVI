using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Entities
{
    [Table("Aluno")]
    public class Aluno
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int IdUsuario { get; set; }

        public virtual Usuario usuario { get; set; }
    }
}