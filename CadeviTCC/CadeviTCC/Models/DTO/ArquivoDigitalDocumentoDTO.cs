﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.DTO
{
    public class ArquivoDigitalDocumentoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string NomeArquivo { get; set; }

        public byte[] Arquivo { get; set; }

        public int IdDocumento { get; set; }

        public int? IdAluno { get; set; }
    }
}