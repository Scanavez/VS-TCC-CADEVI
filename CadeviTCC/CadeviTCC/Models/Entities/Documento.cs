using CadeviTCC.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Context.Entities
{
    [Table("Documento")]
    public class Documento
    {
        public Documento()
        {
            arquivoDigitalDocumentos = new List<ArquivoDigitalDocumento>();
        }

        public int Id { get; set; }

        public string Descricao { get; set; }

        public DateTime HoraRegistro { get; set; }

        public virtual ICollection<ArquivoDigitalDocumento> arquivoDigitalDocumentos { get; set; }


    }
}