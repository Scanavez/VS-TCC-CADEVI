using CadeviTCC.Models.Context.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Entities
{
    [Table("arqdigitaldocumento")]
    public class ArquivoDigitalDocumento
    {

        public int Id { get; set; }

        public string NomeArquivo { get; set; }

        public byte[] Arquivo { get; set; }

        //public int IdDocumento { get; set; }

        //public virtual Documento documento { get; set; }

        public virtual AlunoxDocumento alunoxDocumento { get; set; }

        public int IdAlunoXDocumento { get; set; }

    }
}