using CadeviTCC.Models.Context.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Entities
{
    [Table("AlunoXDocumento")]
    public class AlunoxDocumento
    {
        public AlunoxDocumento()
        {
            ArquivoDigitalDocumentos = new List<ArquivoDigitalDocumento>();
        }

        public int Id { get; set; }

        public virtual Aluno Aluno { get; set; }

        public int IdAluno { get; set; }

        public virtual Documento Documento { get; set; }

        public int IdDocumento { get; set; }

        public virtual ICollection<ArquivoDigitalDocumento> ArquivoDigitalDocumentos { get; set; }
    }
}