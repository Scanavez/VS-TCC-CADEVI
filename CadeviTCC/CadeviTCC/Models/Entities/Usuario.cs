using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {
            Alunos = new List<Aluno>();
        }


        public int Id { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        [Display(Name = "Funcionário")]
        public string Nome { get; set; }

        public virtual TipoUsuario tipoUsuario { get; set; }

        public virtual ICollection<Aluno> Alunos { get; set; }

        public int IdTipoUsuario { get; set; }

    }
}