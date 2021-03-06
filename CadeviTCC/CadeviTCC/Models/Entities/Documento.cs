﻿using CadeviTCC.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            //arquivoDigitalDocumentos = new List<ArquivoDigitalDocumento>();
            AlunoxDocumento = new List<AlunoxDocumento>();
        }
    
        public int Id { get; set; }

        [Display(Name = "Descrição documento")]
        public string Descricao { get; set; }

        public DateTime HoraRegistro { get; set; }

        //public virtual ICollection<ArquivoDigitalDocumento> arquivoDigitalDocumentos { get; set; }

        public virtual ICollection<AlunoxDocumento> AlunoxDocumento { get; set; }

        //public int? IdAluno { get; set; }

        
    }
}